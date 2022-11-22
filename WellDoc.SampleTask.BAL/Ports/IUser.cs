using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WellDoc.SampleTask.Model;

namespace WellDoc.SampleTask.BAL.Ports
{
    public interface IUser
    {
        public Task<List<UsersModel>> GetUsers(int id);
        public Task<string> SaveOrUpdateUser(UsersModel model);
        public Task<string> DeleteUser(int id);
        public Task<string> ValidateUserCredentials(string email, string password);
    }
}
