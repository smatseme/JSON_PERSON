using System.ComponentModel.DataAnnotations;

namespace JSON_PERSON.Models
{
    public class PersonModel
    {
        [Required(ErrorMessage = "ID is required")]
        public int Id { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
       
    }
}
