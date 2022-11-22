using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WellDoc.SampleTask.BAL.Ports;
using WellDoc.SampleTask.Model;

namespace WellDoc.SampleTask.BAL.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUser _user;
        public UserService(IUser user)
        {
            _user = user;
        }

        public async Task<List<UsersModel>> GetUsers(int id)
        {
            return await _user.GetUsers(id);
        }

        public async Task<ReturnObject<string>> SaveUser(UsersModel model)
        {
            ReturnObject<string> returnObject = new ReturnObject<string>();
            returnObject.code = "C205";
            returnObject.message = "User action is failed";
            var result = await _user.SaveOrUpdateUser(model);
            if (result != null)
            {
                returnObject.code = result;
                if (result == "C200")
                {                    
                    returnObject.message = "User saved successfully";
                    returnObject.isStatus = true;
                }
                else if (result == "C201")
                {
                    returnObject.message = "User email already exists";
                    returnObject.isStatus = false;
                }
            }
            return returnObject;
        }

        public async Task<ReturnObject<string>> UpdateUser(UsersModel model)
        {
            ReturnObject<string> returnObject = new ReturnObject<string>();
            returnObject.code = "C205";
            returnObject.message = "User action is failed";
            var result = await _user.SaveOrUpdateUser(model);
            if (result != null)
            {
                returnObject.code = result;
                if (result == "C200")
                {
                    returnObject.message = "User updated successfully";
                    returnObject.isStatus = true;
                }
                else if (result == "C201")
                {
                    returnObject.message = "User email already exists";
                    returnObject.isStatus = false;
                }
                else if (result == "C202")
                {
                    returnObject.message = "User id not exists";
                    returnObject.isStatus = false;
                }
            }
            return returnObject;
        }

        public async Task<ReturnObject<string>> DeleteUser(int id)
        {
            if (id == 0)
            {
                return new ReturnObject<string>()
                {
                    code = Convert.ToString(201),
                    isStatus = false,
                    message = "User id is required"
                };
            }
            else
            {
                ReturnObject<string> returnObject = new ReturnObject<string>();
                returnObject.code = "C205";
                returnObject.message = "User action is failed";
                var result = await _user.DeleteUser(id);
                if (result != null)
                {
                    returnObject.code = result;
                    if (result == "C200")
                    {
                        returnObject.message = "User deleted successfully";
                        returnObject.isStatus = true;
                    }
                    else if (result == "C203")
                    {
                        returnObject.message = "User detail not found";
                        returnObject.isStatus = false;
                    }
                }
                return returnObject;
            }
        }

        public async Task<ReturnObject<string>> ValidateUserCredentials(string email, string password)
        {
            ReturnObject<string> returnObject = new ReturnObject<string>();
            returnObject.code = "C205";
            returnObject.message = "User action is failed";
            var result = await _user.ValidateUserCredentials(email, password);
            if (result != null)
            {
                returnObject.code = result;
                if (result == "C200")
                {                    
                    returnObject.message = "Username and password are valid.";
                    returnObject.isStatus = true;
                }
                else if (result == "C203")
                {
                    returnObject.message = "Username or password are not valid.";
                    returnObject.isStatus = false;
                }
            }
            return returnObject;
        }
    }
}
