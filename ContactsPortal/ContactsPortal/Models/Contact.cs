using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactsPortal.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(50)")]
        public required string Name { get; set; }

        [Column(TypeName = "varchar(15)")]
        public required string PhoneNumber { get; set; }

        [Column(TypeName = "varchar(50)")]
        public required string ContactType { get; set; }

        public required int ContactGroupId { get; set; }

        [ForeignKey("ContactGroupId")]
        public virtual ContactGroup? ContactGroup { get; set; }

        [NotMapped]
        public string ContactGroupName
        {
            get
            {
                return ContactGroup == null ? "" : ContactGroup.ContactGroupName;
            }
        }
    }
}
