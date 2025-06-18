using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mission.Entities.Models
{
    public class LoginUserRequestModel
    {
        public string EmailAddress { get; set; }

        public string Password { get; set; }
    }
}
