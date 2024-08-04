using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class AppUser:IdentityUser
    {
        
        public int?Age{ get; set; }
        public List<Catergory>? Catergories { get; set; }
        public string ?BDate { get; set; }
        public int CalcAge(string bdate)
        {
            List<string> values=new List<string>(bdate.Split('-'));
            return DateTime.Now.Year -int.Parse( values[0]);


        }
        public WatchHistory ?watchHistory { get; set; }
        public List<Post> ?posts { get; set; }  
        
    }
}
