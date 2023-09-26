using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace lnksnp.Models
{
    public class LinkSnip
    {
        public int id { get; set; }
        public string ShortLink { get; set; } = "";
        public string LongLink { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
