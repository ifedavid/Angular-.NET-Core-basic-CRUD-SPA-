using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
    public class LoginViewModel
    {

        public string UserId { get; set; }

       
        public string FirstName { get; set; }

       
        public string LastName { get; set; }

       
        public string EmailAddress { get; set; }

       
        public string PictureUrl { get; set; }

       
        public string Provider { get; set; }


    }
}
