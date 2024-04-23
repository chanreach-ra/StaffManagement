using StaffManagementAPI.Extensions;

namespace StaffManagementAPI.Models
{
    public class StaffDTO
    {
        public string StaffID { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public GenderType Gender { get; set; }

        public bool IsValid(out string message)
        {
            message = "";
            var isValid = !StaffID.IsNullOrEmpty() && !FullName.IsNullOrEmpty();
            if (!isValid)
            {
                if (StaffID.IsNullOrEmpty())
                    message = message.Append("Field 'StaffID' is required");

                if (FullName.IsNullOrEmpty())
                    message = message.Append("Field 'FullName' is required");
            }
            else
            {
                isValid = StaffID.IsValidID(8) && FullName.IsValidName(100) && (Gender == GenderType.Male || Gender == GenderType.Female);
                if (!isValid)
                {
                    if (!StaffID.IsValidID(8))
                        message = message.Append("Invalid staff ID. Must be 8 characters long, containing only letters and numbers.");

                    if (!FullName.IsValidName(100))
                        message = message.Append("Invalid full name. Must be less than or equal 100 characters, and containing only letters");

                    if (!(Gender == GenderType.Male || Gender == GenderType.Female))
                        message = message.Append("Invalid gender");
                }

            }

            return isValid;
        }
    }
}