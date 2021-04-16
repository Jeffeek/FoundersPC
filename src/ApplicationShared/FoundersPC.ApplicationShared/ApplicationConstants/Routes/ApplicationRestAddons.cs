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
    }
}
