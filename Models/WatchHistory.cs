using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class WatchHistory
    {
        [Key]
        public int Id { get; set; }
        public string ?Userid { get; set; }
        [ForeignKey("User_id")]
        public AppUser ?user { get; set; }
        public string ?PostId { get; set; }
        [ForeignKey("PostId")]
        public Post ?Post { get; set; }
        public String ?WData { get; set; }
    }
}
