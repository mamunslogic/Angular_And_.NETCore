using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsPortal.Models
{
    public class ContactGroup
    {
        [Key]
        public int Id { get; set; }
        
        [Column(TypeName = "varchar(50)")]
        public required string ContactGroupName { get; set; }
    }
}
