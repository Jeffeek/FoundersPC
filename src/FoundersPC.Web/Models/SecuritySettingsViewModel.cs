#region Using namespaces

using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.Web.Models
{
    public class SecuritySettingsViewModel
    {
        [Display(Name = "New login", Prompt = "New login")]
        [StringLength(30,
                      MinimumLength = 5,
                      ErrorMessage = "Min 5, max 30")]
        [Required]
        public string NewLogin { get; set; }
    }
}