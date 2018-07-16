using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnguyenResume.Bussiness.Interface;
using tnguyenResume.Bussiness.Model;

namespace tnguyenResume.Bussiness.DAL
{
    public class AccountDAL : IAccountDAL
    {
        private readonly ItnguyenResumeDbContext _itnguyenResumeDbContext;

        public AccountDAL(ItnguyenResumeDbContext itnguyenResumeDbContext)
        {
            _itnguyenResumeDbContext = itnguyenResumeDbContext;
        }

        public Account checkCookie(string userName, string password, string bRemember)
        {
            Account account = null;
           
            if (!String.IsNullOrEmpty(userName) && !String.IsNullOrEmpty(password))
                account = new Account { UserName = userName, Password = password, Role = bRemember };
            return account;
        }

        public Account TryLogin(string UserName, string Password)
        {
            var result = _itnguyenResumeDbContext.Accounts.Where(x => x.UserName == UserName && x.Password == Password).FirstOrDefault();
            return result;
        }

    }
}
