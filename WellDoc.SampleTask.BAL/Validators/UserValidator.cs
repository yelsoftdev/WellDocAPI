using System;
using System.Collections.Generic;
using System.Text;
using WellDoc.SampleTask.BAL.Ports;
using WellDoc.SampleTask.Model;

namespace WellDoc.SampleTask.BAL.Validators
{
    public class UserValidator: IUserValidator
    {        
        public string GetTokenValidator(string email, string password)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(email))
                errors.Add("Username is required");
            if (string.IsNullOrEmpty(password))
                errors.Add("Password is required");
            if (errors.Count > 0)
                return string.Join(",", errors);
            else
                return null;
        }
        public string GetUsersValidator(int id)
        {
            List<string> errors = new List<string>();
            if (id <= 0) 
                errors.Add("User id is required");
            if (errors.Count > 0)
                return string.Join(",", errors);
            else
                return null;
        }

        public string SaveUserValidator(UsersModel model)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(model.email))
                errors.Add("Email is required");
            if (string.IsNullOrEmpty(model.firstName))
                errors.Add("First name is required");
            if (string.IsNullOrEmpty(model.password))
                errors.Add("Password is required");            
            if (errors.Count > 0)
                return string.Join(",", errors);
            else
                return null;
        }

        public string UpdateUserValidator(UsersModel model)
        {
            List<string> errors = new List<string>();
            if (model.id <= 0)
                errors.Add("User id is required");
            if (string.IsNullOrEmpty(model.email))
                errors.Add("Email is required");
            if (string.IsNullOrEmpty(model.firstName))
                errors.Add("First name is required");
            if (string.IsNullOrEmpty(model.password))
                errors.Add("Password is required");            
            if (errors.Count > 0)
                return string.Join(",", errors);
            else
                return null;
        }
        public string DeleteUserValidator(int id)
        {
            List<string> errors = new List<string>();
            if (id <= 0)
                errors.Add("User id is required");
            if (errors.Count > 0)
                return string.Join(",", errors);
            else
                return null;
        }

    }
}
