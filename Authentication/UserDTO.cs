using System.ComponentModel.DataAnnotations;

namespace wjs_c08_react_api.Authentication
{
    public class UserDTO
    {
      [Required]
      public string Id { get; set; }
      [Required, EmailAddress]
      public string Email { get; set; }
      [Required]
      public string UserName { get; set; }
    }
}