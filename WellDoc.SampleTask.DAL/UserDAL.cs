using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using WellDoc.SampleTask.BAL.Ports;
using WellDoc.SampleTask.DAL.Translators;
using WellDoc.SampleTask.DAL.Utility;
using WellDoc.SampleTask.Model;

namespace WellDoc.SampleTask.DAL
{
    public class UserDAL : IUser
    {
        private readonly IConfiguration _configuration;
        private string connectionString = string.Empty;
        public UserDAL(IConfiguration configuration)
        {
            _configuration = configuration;
            connectionString = _configuration.GetConnectionString("WellDocConn");
        }
        public Task<List<UsersModel>> GetUsers(int id)
        {
            SqlParameter[] paramData = { new SqlParameter("@Id",id) };
            List<UsersModel> users = SqlHelper.ExtecuteProcedureReturnData(connectionString, "SPGetUsers", r => r.TranslateAsUsersList(), paramData);
            return Task.FromResult(users);
        }

        public Task<string> SaveOrUpdateUser(UsersModel model)
        {
            string result = string.Empty;
            try
            {
                var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
                {
                    Direction = ParameterDirection.Output
                };
                SqlParameter[] paramData = {
                new SqlParameter("@Id",model.id),
                new SqlParameter("@FirstName",model.firstName),
                new SqlParameter("@Email",model.email),
                new SqlParameter("@Mobile",model.mobile),
                new SqlParameter("@Address",model.address),
                new SqlParameter("@LastName",model.lastName),
                new SqlParameter("@Password",model.password),
                new SqlParameter("@IsActive",model.isActive),
                outParam
                };
                SqlHelper.ExecuteProcedureReturnString(connectionString, "SPSaveOrUpdateUser", paramData);
                result = (string)outParam.Value;
            }
            catch (Exception ex)
            {
                result = null;
            }
            return Task.FromResult(result);
        }

        public Task<string> DeleteUser(int id)
        {
            string result = string.Empty;
            try
            {
                var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
                {
                    Direction = ParameterDirection.Output
                };
                SqlParameter[] paramData = {
                new SqlParameter("@Id",id),
                outParam
                };
                SqlHelper.ExecuteProcedureReturnString(connectionString, "SPDeleteUser", paramData);
                result = (string)outParam.Value;
            }
            catch (Exception ex)
            {
                result = null;
            }
            return Task.FromResult(result);
        }

        public Task<string> ValidateUserCredentials(string email, string password)
        {
            string result = string.Empty;
            try
            {
                var outParam = new SqlParameter("@ReturnCode", SqlDbType.NVarChar, 20)
                {
                    Direction = ParameterDirection.Output
                };
                SqlParameter[] paramData = {
                new SqlParameter("@Email",email),
                new SqlParameter("@Password",password),
                outParam
                };
                SqlHelper.ExecuteProcedureReturnString(connectionString, "SPValidateUser", paramData);
                result = (string)outParam.Value;
            }
            catch (Exception ex)
            {
                result = null;
            }
            return Task.FromResult(result);
        }
    }
}
