using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BooksCollection.Api.Models
{
    public class Book
    {
        [Key, JsonIgnore]
        public int Id { get; set; }
        public string? Uid { get; set; }
        /// <summary>
        /// When the book was added to the collection.
        /// </summary>
        public DateTime CreationDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(100, ErrorMessage = "Title cannot be greater than 100 characters.")]
        public string Title { get; set; }
        public string? Description { get; set; }
        public string AuthorName { get; set; }
        /// <summary>
        /// When the book was published.
        /// </summary>
        public DateTime? PublishedDate { get; set; }
        /// <summary>
        /// MSRP is in USD.
        /// </summary>
        public decimal? Msrp { get; set; }
        public MsrpUnit? MsrpUnit { get; set; }
        public string? Isbn { get; set; }
        public BookCategory? Category { get; set; }
        public string? OtherCategoryName { get; set; }
        public string? ImageUrl { get; set; }
        //public double? Rating { get; set; }
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

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum MsrpUnit
    {
        Usd,
    }

    
}
