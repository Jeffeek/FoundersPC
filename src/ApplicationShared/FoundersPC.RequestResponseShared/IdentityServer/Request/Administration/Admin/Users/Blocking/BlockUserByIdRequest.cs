﻿namespace FoundersPC.RequestResponseShared.IdentityServer.Request.Administration.Admin.Users.Blocking
{
    public class BlockUserByIdRequest
    {
        public int UserId { get; set; }

        public bool BlockUserTokens { get; set; } = true;

        public bool SendNotificationToUserViaEmail { get; set; } = true;
    }
}