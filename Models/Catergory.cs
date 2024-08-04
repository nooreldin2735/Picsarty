using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Catergory
    {
        [Key]
        public String Catergory_id { get; set; }
        public string Catergory_name { get; set; }
        public List<Post> Posts { get; set; }
        public List<AppUser> AppUsers { get; set; }

    }
}
