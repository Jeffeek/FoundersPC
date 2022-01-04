#region Using namespaces

#endregion

namespace FoundersPC.SharedKernel.Models;

public class Error
{
    public Error() { }

    public Error(string message, string description)
    {
        Message = message;
        Description = description;
    }

    public string Message { get; set; } = default!;

    public string Description { get; set; } = default!;

    public override string ToString() => $"Message {Message}, Description {Description}";
}