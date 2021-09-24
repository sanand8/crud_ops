using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class Player
    {

        [Key]
        public int Number { get; set; }
        public String Name { get; set; }
        public String Role { get; set; }
        public String Country { get; set; }
    }
}
