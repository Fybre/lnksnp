namespace lnksnp.Models
{
    public class LinkResponse
    {
        public string ShortLink { get; set; } = "";
        public string LongLink { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.Now;
    }

}
