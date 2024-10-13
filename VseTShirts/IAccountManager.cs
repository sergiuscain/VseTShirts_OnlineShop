using VseTShirts.Models;

namespace VseTShirts
{
    public interface IAccountManager
    {
        void Add(UserAccount account);
        List<UserAccount> GetAll();
        UserAccount GetByEmail(string email);
        UserAccount GetById(Guid id);
        UserAccount GetByUserName(string userName);
        void Login(UserAccount user);
        void Register(RegisterModel register);
        void Remove(UserAccount account);
        bool VerifyPassword(UserAccount user, string password);
    }
}  