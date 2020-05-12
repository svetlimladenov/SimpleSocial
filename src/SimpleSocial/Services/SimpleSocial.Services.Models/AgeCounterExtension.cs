using System;

namespace SimpleSocial.Services.Models
{
    public static class AgeCounterExtension
    {
        public static int GetAge(this DateTime? dt)
        {
            if (dt == null)
            {
                return -1;
            }
            // Save today's date.
            var today = DateTime.Today;
            // Calculate the age.
            var age = today.Year - dt.Value.Year;
            // Go back to the year the person was born in case of a leap year
            if (dt.Value > today.AddYears(-age)) age--;

            return age;
        }
    }
}
