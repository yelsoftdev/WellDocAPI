using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WellDoc.SampleTask.Model;

namespace WellDoc.SampleTask.BAL.Ports
{
    public interface IUserService
    {
        public Task<List<UsersModel>> GetUsers(int id);
        public Task<ReturnObject<string>> SaveUser(UsersModel model);
        public Task<ReturnObject<string>> UpdateUser(UsersModel model);
        public Task<ReturnObject<string>> DeleteUser(int id);
        public Task<ReturnObject<string>> ValidateUserCredentials(string email, string password);
    }
}
