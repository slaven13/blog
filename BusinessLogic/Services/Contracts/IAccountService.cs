using BusinessLogic.Models.Account;
using System.Threading.Tasks;

namespace BusinessLogic.Services.Contracts
{
    public interface IAccountService
    {
        Task<int> RegisterUser(RegisterModel user);
        Task<int> LoginUser(LoginModel user);
        Task<int> LogOff();
        Task<int> ResetPassword(ResetPasswordModel user);
    }
}
