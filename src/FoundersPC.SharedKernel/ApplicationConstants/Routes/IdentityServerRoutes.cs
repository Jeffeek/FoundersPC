#region Using namespaces

using System;
using System.Text.RegularExpressions;

#endregion

namespace FoundersPC.SharedKernel.ApplicationConstants.Routes;

public static class IdentityServerRoutes
{
    public const string BaseEndpoint = "FoundersPCIdentity";

    private const string Email = "{email}";

    private const string Id = "{id:int:min(1)}";

    private const string Token = "{token:length(64)}";

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

    public static string BuildRouteByEmail(string route, string email) => Regex.Replace(route, "{email}", email);

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

    public static string BuildRouteForToken(string route, string token) => Regex.Replace(route, "{token:length\\(64\\)}", token);

    public static string BuildRouteForBetween(DateTime start, DateTime finish) => $"?Start={start:s}&Finish={finish:s}";

    public static class Authentication
    {
        public const string AuthenticationEndpoint = BaseEndpoint + "/" + "Authentication";

        public const string ForgotPassword = "ForgotPassword";

        public const string SignIn = "SignIn";

        public const string SignUp = "SignUp";

        public const string SignUpManager = "NewManager";
    }

    public static class Tokens
    {
        public const string TokensEndpoint = BaseEndpoint + "/" + "Tokens";

        public const string ReserveNewToken = "Reserve";

        public const string CheckToken = "Check" + "/" + Token;

        public const string ByUserId = "ByUserId" + "/" + Id;

        public const string ByUserEmail = "ByUserEmail" + "/" + Email;

        public const string BlockByTokenString = "BlockByTokenString" + "/" + Token;

        public const string BlockByTokenId = "BlockByTokenId" + "/" + Id;
    }

    public static class Users
    {
        public const string UsersEndpoint = BaseEndpoint + "/" + "Users";

        public const string ByUserId = Id;

        public const string ByUserEmail = Email;

        public static class StatusChange
        {
            public const string StatusEndpoint = UsersEndpoint + "/" + "StatusChange";

            public static class Block
            {
                public const string BlockByUserId = "Block" + "/" + "ByUserId";

                public const string BlockByUserEmail = "Block" + "/" + "ByUserEmail";
            }

            public static class Unblock
            {
                public const string UnblockByUserId = "Unblock" + "/" + "ByUserId";

                public const string UnblockByUserEmail = "Unblock" + "/" + "ByUserEmail";
            }

            public static class MakeInactive
            {
                public const string MakeInactiveByUserId = "MakeInactive" + "/" + "ByUserId";

                public const string MakeInactiveByUserEmail = "MakeInactive" + "/" + "ByUserEmail";
            }
        }

        public static class SettingsChange
        {
            public const string SettingsChangeEndpoint = UsersEndpoint + "/" + "Settings";

            public const string PasswordChange = "Password";

            public const string LoginChange = "Login";

            public const string NotificationsChange = "Notifications";
        }
    }

    public static class Logs
    {
        public const string LogsEndpoint = BaseEndpoint + "/" + "Logs";

        public static class UsersEntrances
        {
            public const string UsersEntrancesEndpoint = LogsEndpoint + "/" + "UsersEntrances";

            public const string ByUserId = "ByUserId" + "/" + Id;

            public const string ByUserEmail = "ByUserEmail" + "/" + Email;

            public const string Between = "Between";
        }

        public static class TokenUsages
        {
            public const string TokenUsagesEndpoint = LogsEndpoint + "/" + "TokenUsages";

            public const string ByUserId = "ByUserId" + "/" + Id;

            public const string ByUserEmail = "ByUserEmail" + "/" + Email;

            public const string ByTokenId = "ByTokenId" + "/" + Id;

            public const string ByTokenString = "ByTokenId" + "/" + Token;

            public const string Between = "Between";
        }
    }
}