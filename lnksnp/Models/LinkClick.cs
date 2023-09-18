namespace lnksnp.Models
{
    public class LinkClick
    {
        public int id { get; set; }
        public DateTime clickDate { get; set; } = DateTime.Now;
        public int linkId { get; set; }
        public string? IP { get; set; }
        public string? referer { get; set; }
        public string? userAgent { get; set; }
        public string? acceptLanguage { get; set; }  

    }
}
