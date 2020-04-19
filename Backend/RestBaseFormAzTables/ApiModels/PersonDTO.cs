using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace RestBaseFormAzTables.ApiModels
{
    public class PersonDTO
    {
        [Required]
        [MinLength(3), MaxLength(160)]
        public string Name { get; set; }
        
        [Required]
        [MinLength(8), MaxLength(160)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public IFormFile Picture { get; set; }
    }
}