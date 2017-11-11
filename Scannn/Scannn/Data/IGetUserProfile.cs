using Scannn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scannn.Data
{
    public interface IGetUserProfile
    {
        Task<LoginAPI> PerformLoginAsync(string email, string password);
        Task<string> PerformGetFullNameAsync(string email);
        Task PerformLogoutAsync(string sessionid);
        Task<UserProfileAPI> PerformGetUserProfileAsync(string session, string mode);
    }
}
