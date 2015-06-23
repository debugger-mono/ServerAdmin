using System;
using System.Threading.Tasks;
using System.Web.Http;
using ServerAdmin.Models;
using Tbl.ServerAdmin.DataAccess;
using Tbl.ServerAdmin.DataAccess.Core;
using Tbl.ServerAdmin.DataAccess.Handlers;
using Tbl.ServerAdmin.DataAccess.Models;

namespace ServerAdmin.Controllers.ApiControllers
{
    public class LoginController : ApiController
    {
        private readonly IDataAccessHandler<ServerAdminDbContext> dataAccess;
        private readonly IUserAccountHandler userAccountHandler;

        public LoginController(IDataAccessHandler<ServerAdminDbContext> dataAccess, IUserAccountHandler userAccountHandler)
        {
            this.dataAccess = dataAccess;
            this.userAccountHandler = userAccountHandler;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<LoginResultViewModel> ValidateLogin(LoginRequestViewModel loginRequest)
        {

            var success = this.userAccountHandler.Validate(new UserAccount { Username = loginRequest.Username, Password = loginRequest.Password });

            if (success)
            {

            }

            var result = new LoginResultViewModel() { IsSuccess = success, Key = Guid.NewGuid().ToString() };
            return result;
        }
    }
}
