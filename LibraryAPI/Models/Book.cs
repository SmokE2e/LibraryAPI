using System;
using System.Text.Json.Serialization;

namespace LibraryAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string ISBN { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int AuthorId { get; set; }

        [JsonIgnore]
        public Author Author { get; set; }

        public DateTime? TakenAt { get; set; }

        public DateTime? ReturnBy { get; set; }

        public string ImagePath { get; set; } = string.Empty;
    }
}
