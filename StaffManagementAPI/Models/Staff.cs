using System.ComponentModel.DataAnnotations;

namespace StaffManagementAPI.Models
{
    public class Staff : BaseObject
    {
        [StringLength(8)]
        public string StaffID { get; set; }
        [StringLength(100)]
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public GenderType Gender { get; set; }
    }

    public enum GenderType
    {
        Male = 1,
        Female = 2
    }
}