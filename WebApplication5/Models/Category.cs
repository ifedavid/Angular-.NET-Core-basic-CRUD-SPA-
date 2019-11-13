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

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
        [Required]
        public DailySpendings DailySpendings { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        


    }
}
