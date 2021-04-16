using System;
using System.Text.RegularExpressions;

namespace FoundersPC.ApplicationShared.ApplicationConstants.Routes
{
    public static class IdentityServerRoutes
    {
        public const string Base = "FoundersPCIdentity";

        #region Docs

        /// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
        /// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="input" />, <paramref name="pattern" />, or <paramref name="replacement" /> is <see langword="null" />.</exception>

        #endregion

        public static string BuildRouteById(string route, int id) => Regex.Replace(route, "{id:int:min\\(1\\)}", id.ToString());

        #region Docs

        /// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
        /// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="input" />, <paramref name="pattern" />, or <paramref name="replacement" /> is <see langword="null" />.</exception>

        #endregion

        public static string BuildRouteByEmail(string route, string email) => Regex.Replace(route, "{email}", email);

        #region Docs

        /// <exception cref="T:System.Text.RegularExpressions.RegexMatchTimeoutException">A time-out occurred. For more information about time-outs, see the Remarks section.</exception>
        /// <exception cref="T:System.ArgumentException">A regular expression parsing error occurred.</exception>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="input" />, <paramref name="pattern" />, or <paramref name="replacement" /> is <see langword="null" />.</exception>

        #endregion

        public static string BuildRouteForToken(string route, string token) => Regex.Replace(route, "{token:length\\(64\\)}", token);

        public static class Authentication
        {
            public const string Endpoint = Base + "/" + "Authentication";

            public const string ForgotPassword = "ForgotPassword";

            public const string SignIn = "SignIn";

            public const string SignUp = "SignUp";

            public const string SignUpManager = "NewManager";
        }

        public static class Tokens
        {
            public const string Endpoint = Base + "/" + "Tokens";

            public const string ReserveNewToken = "Reserve";

            public const string CheckToken = "Check" + "/" + Token;

            public static class User
            {
                public const string UserEndpoint = "User";

                public const string UserTokensByUserId = UserEndpoint + "/" + Id;

                public const string UserTokensByUserEmail = UserEndpoint + "/" + Email;
            }

            public static class Block
            {
                public const string BlockEndpoint = "Block";

                public const string BlockTokenByTokenId = BlockEndpoint + "/" + "ById" + "/" + Id;

                public const string BlockTokenByToken = BlockEndpoint + "/" + "ByToken" + "/" + Token;
            }

            public static class Logs
            {
                public const string LogsEndpoint = Endpoint + "/" + "Logs";

                public static class LogsByToken
                {
                    public const string LogsByTokenId = "ByTokenId" + "/" + Id;

                    public const string LogsByTokenString = "ByToken" + "/" + Token;
                }

                public static class LogsByUser
                {
                    public const string LogsByUserId = "ByUserId" + "/" + Id;

                    public const string LogsByUserEmail = "ByUserEmail" + "/" + Email;
                }
            }
        }

        public static class Users
        {
            public const string UsersEndpoint = Base + "/" + "Users";

            public static class Get
            {
                public const string ById = "ById" + "/" + Id;

                public const string ByEmail = "ByEmail" + "/" + Email;
            }

            public static class SettingsChange
            {
                public const string SettingsChangeEndpoint = UsersEndpoint + "/" + "SettingsChange";

                public const string Password = "Password";

                public const string Login = "Login";

                public const string Notifications = "Notifications";
            }

            public static class Entrances
            {
                public const string EntrancesEndpoint = UsersEndpoint;

                public static class GetEntrances
                {
                    public const string InnerEntrancesEndpoint = "Entrances";

                    public const string All = InnerEntrancesEndpoint + "/" + "All";

                    public const string ById = InnerEntrancesEndpoint + "/" + Id;

                    public const string Between = InnerEntrancesEndpoint + "/" + "Between";

                    public static string BuildBetweenQuery(DateTime left, DateTime right) => Between + $"?Start={left:s}&Finish={right:s}";
                }

                public static class User
                {
                    public const string ByUserId = "ById" + "/" + Id + "/" + "Entrances";

                    public const string ByUserEmail = "ByEmail" + "/" + Email + "/" + "Entrances";
                }
            }

            public static class Status
            {
                public const string UserChangeStatusEndpoint = UsersEndpoint + "/" + "StatusChange";

                public static class Block
                {
                    public const string BlockEndpoint = "Block";

                    public const string ByUserId = BlockEndpoint + "/" + "ById";

                    public const string ByUserEmail = BlockEndpoint + "/" + "ByEmail";
                }

                public static class Unblock
                {
                    public const string UnblockEndpoint = "UnBlock";

                    public const string ByUserId = UnblockEndpoint + "/" + "ById";

                    public const string ByUserEmail = UnblockEndpoint + "/" + "ByEmail";
                }

                public static class MakeInactive
                {
                    public const string MakeInactiveEndpoint = "MakeInactive";

                    public const string ByUserId = MakeInactiveEndpoint + "/" + "ById";

                    public const string ByUserEmail = MakeInactiveEndpoint + "/" + "ByEmail";
                }
            }
        }

        private const string Email = "{email}";

        private const string Id = "{id:int:min(1)}";

        private const string Token = "{token:length(64)}";
    }
}
