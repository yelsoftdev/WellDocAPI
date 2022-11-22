using System;
using System.Collections.Generic;
using System.Text;
using WellDoc.SampleTask.Model;

namespace WellDoc.SampleTask.BAL.Ports
{
    public interface IUserValidator
    {
        public string GetTokenValidator(string email, string password);
        public string GetUsersValidator(int id);
        public string SaveUserValidator(UsersModel model);
        public string UpdateUserValidator(UsersModel model);
        public string DeleteUserValidator(int id);
    }
}
