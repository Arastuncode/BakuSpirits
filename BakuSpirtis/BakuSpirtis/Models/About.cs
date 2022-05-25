using System.ComponentModel.DataAnnotations;


namespace BakuSpirtis.Models
{
    public class About : BaseEntity
    {
        [Required]
        public string Image { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
