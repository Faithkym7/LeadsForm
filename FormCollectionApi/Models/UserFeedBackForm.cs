using System.ComponentModel.DataAnnotations;

namespace FormCollectionApi.Models
{
    public class UserFeedBackForm
    {

        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }

        public bool ContactPreference { get; set; }

        public ModeofContact ModeofContact { get; set; }

        public double Score 
        { 
            get
            {
                return ContactPreference ? 5.0 : 3.0;
            }
         }

        public string[] Product { get; set; } = Array.Empty<string>();

    }

    public enum ModeofContact { Call, Email}
}
