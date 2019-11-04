using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class DailySpendings
    {
        [Key]
        public DateTime Date { get; set; }

        public List<Category> Categories { get; set; }

        public UserData User { get; set; }
    }
}
