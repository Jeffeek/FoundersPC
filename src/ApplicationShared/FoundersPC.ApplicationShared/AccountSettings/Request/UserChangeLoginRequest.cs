#region Using namespaces

using System.ComponentModel.DataAnnotations;

#endregion

namespace FoundersPC.ApplicationShared.AccountSettings
{
    public class UserChangeLoginRequest
    {
        [Required]
        public int UserId { get; set; }

        [StringLength(30,
                      MinimumLength = 5,
                      ErrorMessage = "Min 5, max 30")]
        [Required]
        public string NewLogin { get; set; }
    }
}