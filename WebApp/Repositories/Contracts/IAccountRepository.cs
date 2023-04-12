using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Repositories.Contracts;

public interface IAccountRepository : IRepository<string, Account>
{
    int Register(RegisterVM registerVM);
    bool Login(LoginVM loginVM);
    bool CheckEmailPhone(string email, string phone);
}
