using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoundersPC.Web.Models.ViewModels
{
    public class UserNotificationsViewModel
    {
        [Display(Name = "Notify by email on entrance")]
        public bool SendNotificationOnEntrance { get; set; }

        [Display(Name = "Notify by email on using API token")]
        public bool SendNotificationOnUsingAPI { get; set; }
    }
}
