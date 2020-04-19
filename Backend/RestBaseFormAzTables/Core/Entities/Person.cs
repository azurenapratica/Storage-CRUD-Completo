using System.ComponentModel.DataAnnotations;
using Microsoft.WindowsAzure.Storage.Table;

namespace RestBaseFormAzTables.Core.Entities
{
    public class Person : TableEntity
    {
        [Required]
        [MinLength(3), MaxLength(160)]
        public string Name { get; set; }
        
        [Required]
        [MinLength(8), MaxLength(160)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Picture { get; set; }

        public Person() { }
    }
}