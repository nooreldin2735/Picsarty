﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class RecomendCategories
    {
        [Key]
        public int Id { get; set; }
        public string ?UserId { get; set; }
        public string ?Category_name { get; set; }
        public int ?clicks {  get; set; }
         

    }
}
