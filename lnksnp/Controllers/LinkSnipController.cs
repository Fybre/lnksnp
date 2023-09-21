using lnksnp.Context;
using lnksnp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net;

namespace lnksnp.Controllers
{
    //[Route("/")]
    [ApiController]
    public class LinkSnipController : ControllerBase
    {
        private readonly LinkSnipContext _context;

        public LinkSnipController(LinkSnipContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Create a new link entry
        /// </summary>
        /// <param name="linkreq"></param>
        /// <returns></returns>
        [Route("api/[controller]")]
        [HttpPost]
        public async Task<ActionResult<LinkResponse>> CreateLink([FromBody]LinkRequest linkreq)
        {
            //lets just force HTTPS
            //var reqHost = Request.IsHttps ? "https://" : "http://" + Request.Host.ToString();
            var reqHost = "https://" + Request.Host.ToString();
            if (linkreq?.LongLink != null)
            {
                UriBuilder builder = new UriBuilder(linkreq.LongLink);
                if (builder.Uri.IsAbsoluteUri)
                {
                    var newLink = new LinkSnip { LongLink = builder.Uri.ToString() };
                    _context.Add(newLink);
                    await _context.SaveChangesAsync();
                    newLink.ShortLink = Base64UrlEncoder.Encode(newLink.id.ToString());
                    await _context.SaveChangesAsync();
                    
                    var shortLink = reqHost + "/" + newLink.ShortLink;
                    builder = new UriBuilder(shortLink);

                    LinkResponse linkResponse = new LinkResponse { 
                                        LongLink = newLink.LongLink, 
                                        ShortLink = builder.Uri.ToString(),
                                        Created = newLink.Created};

                    return Ok(linkResponse);
                }
            }
            return BadRequest("Valid Link required");
        }

        /// <summary>
        /// Gets the longLink for a short link value
        /// </summary>
        /// <param name="shortLink"></param>
        /// <returns></returns>
        [HttpGet("api/[controller]/{shortLink}")]
        public async Task<IActionResult> GetLongLink(string shortLink)
        {
            if (string.IsNullOrEmpty(shortLink)) { return NotFound(); }
            
            var linkSnip = await _context.LinkSnips.FirstOrDefaultAsync(x => x.ShortLink == shortLink);
            if (linkSnip != null)
            {
                var newClick = new LinkClick
                {
                    linkId = linkSnip.id,
                    IP = Request.HttpContext?.Connection?.RemoteIpAddress?.ToString() ?? "Unknown",
                    referer = Request.Headers.Referer,
                    userAgent = Request.Headers.UserAgent,
                    acceptLanguage = Request.Headers.AcceptLanguage
                };
                _context.LinkClicks.Add(newClick);
                await _context.SaveChangesAsync();
                return new OkObjectResult(linkSnip) ;
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get all link details
        /// </summary>
        /// <returns></returns>
        [HttpGet("api/[controller]/getLinks")]
        public async Task<List<dynamic>> GetLinksWithClickCounts()
        {
            var linksWithClickCounts = await (
                from link in _context.LinkSnips
                join detail in _context.LinkClicks on link.id equals detail.linkId into groupedDetails
                select new
                {
                    linkID = link.id,
                    shortLink = link.ShortLink,
                    longLink = link.LongLink,
                    ClickCount = groupedDetails.Count()  // Count the number of records for each linkId
                }
            ).ToListAsync();

            return linksWithClickCounts.Select(link => (dynamic)link).ToList();
        }

        /// <summary>
        /// End point for redirecting a short link to the long link value
        /// </summary>
        /// <param name="shortLink"></param>
        /// <returns></returns>
        [HttpGet("{shortLink}")]
        public async Task<IActionResult> RedirectLink(string shortLink)
        {
            var res = await GetLongLink(shortLink);
            OkObjectResult? lnk = res as OkObjectResult;    
            if (lnk != null && lnk.Value != null)
            {
                return RedirectPermanent((lnk.Value as LinkSnip).LongLink);
            }
            else
            {
                return File("~/index.html", "text/html");
            }
        }
    }
}
