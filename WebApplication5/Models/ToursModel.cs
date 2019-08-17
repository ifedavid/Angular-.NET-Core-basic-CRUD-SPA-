using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace WebApplication5.Models
{
    public class ToursModel
    {
        [Key]
        public int TourId { get; set; }


        [Required]
        public string TourName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        
        public DateTime EndDate { get; set; }

        [Required]
        public int Duration { get; set; }

        [Required]
        public int OptinPrice { get; set; }

        [Required]
        public TourPlannersModel TourPlanner { get; set; }
    }
}
