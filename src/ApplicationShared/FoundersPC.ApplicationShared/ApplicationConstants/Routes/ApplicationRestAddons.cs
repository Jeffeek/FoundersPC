#region Using namespaces

using System.Text.RegularExpressions;

#endregion

namespace FoundersPC.ApplicationShared.ApplicationConstants.Routes
{
    public static class ApplicationRestAddons
    {
        public const string All = "All";

        public const string GetById = "{id:int:min(1)}";

        public const string Update = "{id:int:min(1)}";

        public const string Delete = "{id:int:min(1)}";

        public const string Create = "";

        public static string BuildPageQuery(int pageNumber, int pageSize) => $"?Page={pageNumber}&Size={pageSize}";

        #region Docs

        /// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">
        ///     A time-out occurred. For more information
        ///     about time-outs, see the Remarks section.
        /// </exception>
        /// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="input"/>, <paramref name="pattern"/>, or
        ///     <paramref name="replacement"/> is <see langword="null"/>.
        /// </exception>

        #endregion

        public static string BuildRouteById(string route, int id) => Regex.Replace(route, "{id:int:min\\(1\\)}", id.ToString());
    }
}