namespace FoundersPC.Web.Domain.Common
{
    public class ErrorViewModel
    {
        public ErrorViewModel(string content) =>
            Content = content;

        public ErrorViewModel(int statusCode, string content)
        {
            StatusCode = statusCode;
            Content = content;
        }

        public int? StatusCode { get; }

        public string Content { get; }
    }
}