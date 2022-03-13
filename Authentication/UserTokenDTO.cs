using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace wjs_c08_react_api.Authentication
{
    public class UserTokenDTO
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string Expiration { get; set; }
    }
}
