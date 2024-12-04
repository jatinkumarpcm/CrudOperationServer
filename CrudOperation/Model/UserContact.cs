using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace CrudOperation.Model
{
    public class UserContact
    {

        [JsonPropertyName("id")]
        public int? Id { get; set; }

        [JsonPropertyName("firstname")]
        [Required(ErrorMessage = "First Name is required")]
        [StringLength(100, MinimumLength = 3)]
        public string FirstName { get; set; }

        [JsonPropertyName("lastname")]
        [Required(ErrorMessage = "Last Name is required")]
        [StringLength(100, MinimumLength = 3)]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        [Required(ErrorMessage = "Please enter your email address")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        [MaxLength(50)]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Please enter correct email")]
        public string Email { get; set; }

    }
}
