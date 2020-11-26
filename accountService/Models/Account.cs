using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace accountService.Models
{
    public class Account
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string Password { get; set; }

        public string UserBio { get; set; }

        public string UserType { get; set; }

        public byte UserImg { get; set; }

        public int UserMobile { get; set; }
    }
}
