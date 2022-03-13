using System.ComponentModel.DataAnnotations;

namespace wjs_c08_react_api.Authentication
{
    public class CreateRole
    {
      [Required(ErrorMessage = "Role name is required")]
      public string RoleName { get; set; }
    }
}