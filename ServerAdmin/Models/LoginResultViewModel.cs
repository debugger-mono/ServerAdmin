
namespace ServerAdmin.Models
{
    public class LoginResultViewModel
    {
        public bool IsSuccess { get; set; }

        public string Key { get; set; }

        public string FailureReason { get; set; }
    }
}