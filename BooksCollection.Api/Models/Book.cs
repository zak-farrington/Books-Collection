using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace BookCollectionApi.Models
{
    public class Book
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; } 

        public string Uid { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public string AuthorUid { get; set; }
        public DateTime PublishedDate { get; set; }
        /// <summary>
        /// MSRP is in USD.
        /// </summary>
        public decimal Msrp { get; set; } 
        public BookCategory Category { get; set; }
        public string OtherCategoryName { get; set; }
        public string ThumbnailUrl { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BookCategory
    {
        Technology,
        Coding,
        Spirituality,
        Meditation,
        Music,
        SelfHelp,
        Other
    }
}
