using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Post
    {
        [Key]
        public string ?Post_id { get; set; }

        public string ?Post_title { get; set; }
        public string ?Picture_id {  get; set; }
        [ForeignKey("Picture_id")]
        public Picture ?picture { get; set; }
        public List<Catergory> catergories { get; set; }=new List<Catergory>();
        public string ?Description { get;set; }
        public string ?User_id { get; set; }
        [ForeignKey("User_id")]
        public AppUser ?appUser { get; set; }
        public string ?Link { get; set; }

    }
}
