using System.ComponentModel.DataAnnotations;

namespace _1121754.Models
{
    public class Movie
    {
        [Required]
        [Key]
        [Display(Name = "電影編號")]
        public int Movie_Id { get; set; }
        [Required]
        [Display(Name = "電影名字")]
        public string Movie_Name { get; set; }
       
       
        [Required]
        [Display(Name = "電影介紹")]
        public string Movie_Introd { get; set; }
    }
}
