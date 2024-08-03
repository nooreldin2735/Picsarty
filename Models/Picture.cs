using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Picture
    {
        [Key]
        public string Id { get; set; }
        public string Name {  get; set; }
        public string Location {  get; set; }
        public int width;
        public int height;
    }
}
