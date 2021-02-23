using System;
using System.Collections.Generic;
using System.Text;

namespace FoundersPC.Web.Application.DTO.Response
{
    public class UserRegisterResponse
    {
        public bool IsRegistrationSuccessful { get; set; } = false;

        public string ResponseException { get; set; } = null;

        public string Email { get; set; } = null;
    }
}
