using Newtonsoft.Json;

namespace FoundersPC.SharedKernel.Models
{
    public class Error
    {
        public Error() { }

        public Error(string message, string description)
        {
            Message = message;
            Description = description;
        }

        [JsonProperty("error")]
        public string Message { get; set; } = default!;

        [JsonProperty("error_description")]
        public string Description { get; set; } = default!;

        public override string ToString() => $"Message {Message}, Description {Description}";
    }
}