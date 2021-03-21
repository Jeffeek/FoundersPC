namespace FoundersPC.Identity.Domain.Common.Interfaces
{
    public interface IUser
    {
        public string Email { get; set; }

        public string HashedPassword { get; set; }
    }
}