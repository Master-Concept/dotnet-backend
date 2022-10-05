using System.ComponentModel.DataAnnotations.Schema;
namespace TodoApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Website { get; set; }
        [Column("CreatedAt", TypeName="DateTime2")]
        public DateTime CreatedAt { get; set; }
        [Column("UpdatedAt", TypeName="DateTime2")]
        public DateTime UpdatedAt { get; set; }
    }
}