using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication5.Models
{
    public class TourPlannersModel

    {
        [Key]
        public int TourPlannerId { get; set; }

        [Required]
        public string TourPlannerFullname { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string TourPlannerEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string TourPlannerPassword { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string TourPlannerPhoneNumber { get; set; }

        public string AboutTourPlanner { get; set; }

        [DataType(DataType.Url)]
        public string TourPlannerWebsite { get; set; }


    }
}
