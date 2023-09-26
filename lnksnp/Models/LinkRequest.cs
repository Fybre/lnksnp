namespace lnksnp.Models
{
    public class LinkRequest
    {
        public string LongLink { get; set; } = "";
        public string? RequestedShortLink { get; set; }
        public string? AccessKey { get; set; }
    }
}
