using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaffManagementAPI.Models
{
    public abstract class BaseObject
    {
        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Column(TypeName = "bit")]
        public bool IsDeleted { get; set; } = false;

        [Column(TypeName = "nvarchar(250)")]
        public string CreatedBy { get; set; } = "*** SYSTEM ***";

        [Column(TypeName = "datetime")]
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

        [Column(TypeName = "nvarchar(250)")]
        public string ModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedDateTime { get; set; }
    }
}