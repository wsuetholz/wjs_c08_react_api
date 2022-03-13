using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace wjs_c08_react_api.Authentication
{
    public class RoleUsersDTO
    {
      [Required]
      public string Id { get; set; }
      [Required]
      public string Name { get; set; }
      public List<string> Users { get; set; }
    }
}