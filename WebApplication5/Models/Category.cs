using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class Category
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public int Amount { get; set; }
        [Required]
        public DateTime TimeStamp { get; set; }

        public DailySpendings Spendings { get; set; }


    }
}
