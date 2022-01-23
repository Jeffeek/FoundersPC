#region Using namespaces

using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Models
{
    public class NotificationsSettingsViewModel
    {
        [Display(Name = "Notify by email on entrance")]
        public bool SendNotificationOnEntrance { get; set; }

        [Display(Name = "Notify by email on using API token")]
        public bool SendNotificationOnUsingAPI { get; set; }
    }
}