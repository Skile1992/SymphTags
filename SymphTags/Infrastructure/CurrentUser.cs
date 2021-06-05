using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SymphTagsApp.Application.Interfaces;

namespace SymphTags.Infrastructure
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _accessor;

        public CurrentUser(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public int Id
        {
            get
            {
                if (_accessor.HttpContext != null)
                    return Int32.Parse(_accessor.HttpContext.User.FindFirst("Id").Value);
                return 0;
            }
        }

        public string DisplayName
        {
            get
            {
                if (_accessor.HttpContext != null)
                    return _accessor.HttpContext.User.FindFirst("DisplayName").Value;
                return null;
            }
        }

        public string Email
        {
            get
            {
                if (_accessor.HttpContext != null)
                    return _accessor.HttpContext.User.FindFirst("Email").Value;
                return null;
            }
        }

    }
}
