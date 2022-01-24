namespace FoundersPC.SharedKernel.ApplicationConstants;

public static class ApplicationAuthorizationPolicies
{
    public const string AdministratorPolicy = "AdministratorPolicy";

    public const string ManagerPolicy = "ManagerPolicy";

    public const string EmployeePolicy = "EmployeePolicy";

    public const string DefaultUserPolicy = "DefaultUserPolicy";

    public const string AuthenticatedPolicy = "AuthenticatedPolicy";

    public const string AllowAllPolicy = "AllowAll";
}

public static class CorsPolicies
{
    public const string AllowAllPolicy = "AllowAllPolicy";
}