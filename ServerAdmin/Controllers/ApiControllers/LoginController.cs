using System;
using System.Threading.Tasks;
using System.Web.Http;
using ServerAdmin.DataAccess;
using ServerAdmin.DataAccess.Models;
using ServerAdmin.Models;

namespace ServerAdmin.Controllers.ApiControllers
{
    public class LoginController : ApiController
    {
        private readonly DataAccessHandler dataAccess;
        private readonly UserAccountHandler userAccountHandler;

        public LoginController()
        {
            this.dataAccess = new DataAccessHandler();
            this.userAccountHandler = new UserAccountHandler(this.dataAccess);
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
