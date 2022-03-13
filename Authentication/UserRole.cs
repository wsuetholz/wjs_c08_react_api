using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace wjs_c08_react_api.Authentication
{
    public class UserRole
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Role name is required")]
        public string RoleName { get; set; }
    }
}
