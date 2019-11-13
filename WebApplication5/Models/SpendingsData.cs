using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Data;

namespace WebApplication5.Models
{
    public class DailySpendings
    {
       
        [Key]
        public Guid DateId { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        public UserData User { get; set; }

        [Required]
        public string CreatedAt { get; set; }

        [Required]
        public string UpdatedAt { get; set; }

        public int TotalAmount { get; set; }

        public int GetTotalAmount(ApplicationDbContext context, Guid dateId)
        {
            int TotalAmount = 0;
            var categories = context.Categories.Where(sp => sp.DailySpendings.DateId == dateId);

            foreach (var category in categories)
            {
                TotalAmount += category.Amount;
            }

            return TotalAmount;

        }

    }
}
