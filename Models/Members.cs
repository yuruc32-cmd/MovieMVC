using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _1121754.Models
{
    public class Members
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookingId { get; set; }  // 新增主鍵
        
        public int Id { get; set; }
        public string Name { get; set; }
        public string ?Movie { get; set; }
        public string ?Time { get; set; }
        public int ?Ticket { get; set; }
        public string Password { get; set; }
    }
}
