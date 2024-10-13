using VseTShirts.Models;

namespace VseTShirts
{
    public class AccountInMemoryManager : IAccountManager
    {
        List<UserAccount> accounts = new();

        public List<UserAccount> GetAll() => accounts;
        public void Add(UserAccount account) => accounts.Add(account);
        public void Remove(UserAccount account) => accounts.Remove(account);
        public UserAccount GetByEmail(string email) => accounts.FirstOrDefault(a => a.Email == email);
        public UserAccount GetById(Guid id) => accounts.FirstOrDefault(a => a.Id == id);
        public UserAccount GetByUserName(string userName) => accounts.FirstOrDefault(a => a.UserName == userName);

        public void Register(RegisterModel register)
        {
            var account = new UserAccount()
            {
                Email = register.Email,
                UserName = register.UserName,
                Password = register.Password,
                Id = Guid.NewGuid()
            };
            accounts.Add(account);
        }

        public bool VerifyPassword(UserAccount user, string password)
        {
            return user.Password == password;
        }

        public void Login(UserAccount user)
        {
            throw new NotImplementedException();
        }
    }
}
