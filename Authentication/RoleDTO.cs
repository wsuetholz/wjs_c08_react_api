using System.ComponentModel.DataAnnotations;

namespace wjs_c08_react_api.Authentication
{
    public class RoleDTO
    {
      [Required]
      public string Id { get; set; }
      [Required]
      public string Name { get; set; }
    }
}