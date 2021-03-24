#region Using namespaces

#endregion

namespace FoundersPC.Web.Models
{
    public class ErrorViewModel
    {
        public ErrorViewModel(int statusCode, string content)
        {
            StatusCode = statusCode;
            Content = content;
        }

        public int StatusCode { get; }

        public string Content { get; }
    }
}