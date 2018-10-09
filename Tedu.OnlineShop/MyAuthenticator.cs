using CKSource.CKFinder.Connector.Core;
using CKSource.CKFinder.Connector.Core.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Tedu.OnlineShop
{
    public class MyAuthenticator : IAuthenticator
    {
        /*
         * Although this method is asynchronous, it will be called for every request
         * and it is not recommended to make time-consuming calls within it.
         */
        public Task<IUser> AuthenticateAsync(ICommandRequest commandRequest, CancellationToken cancellationToken)
        {
            /*
             * It should be safe to assume the IPrincipal is a ClaimsPrincipal.
             */
            var claimsPrincipal = commandRequest.Principal as ClaimsPrincipal;

            /*
             * Extract role names from claimsPrincipal.
             */
            var roles = claimsPrincipal?.Claims?.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToArray();

            /*
             * It is strongly suggested to change this in a way to allow only certain users access to CKFinder.
             * For example you may check commandRequest.RemoteIPAddress to limit access only to your local network.
             */
            var isAuthenticated = true;

            /*
             * Create and return the user.
             */
            var user = new User(isAuthenticated, roles);
            return Task.FromResult((IUser)user);
        }
    }
}