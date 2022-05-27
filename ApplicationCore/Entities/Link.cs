using ShortenerUrl.Api.ApplicationCore.Entities.Common;

namespace ShortenerUrl.Api.ApplicationCore.Entities
{
    public class Link : BaseEntity
    {        
        public string ShortId { get; set; }
        public string Url { get; set; }        
        public int Clicks { get; set; }
        public DateTime? LastAccessDate { get; set; }
        public int? RequestId { get; set; }
    }
}
