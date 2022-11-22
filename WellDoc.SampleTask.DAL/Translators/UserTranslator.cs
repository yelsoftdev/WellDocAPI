using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using WellDoc.SampleTask.DAL.Utility;
using WellDoc.SampleTask.Model;

namespace WellDoc.SampleTask.DAL.Translators
{
    public static class UserTranslator
    {
        public static UsersModel TranslateAsUser(this SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new UsersModel();
            if (reader.IsColumnExists("Id"))
                item.id = SqlHelper.GetNullableInt32(reader, "Id");

            if (reader.IsColumnExists("FirstName"))
                item.firstName = SqlHelper.GetNullableString(reader, "FirstName");

            if (reader.IsColumnExists("Email"))
                item.email = SqlHelper.GetNullableString(reader, "Email");

            if (reader.IsColumnExists("Mobile"))
                item.mobile = SqlHelper.GetNullableString(reader, "Mobile");

            if (reader.IsColumnExists("Address"))
                item.address = SqlHelper.GetNullableString(reader, "Address");

            if (reader.IsColumnExists("LastName"))
                item.lastName = SqlHelper.GetNullableString(reader, "LastName");

            if (reader.IsColumnExists("Password"))
                item.password = SqlHelper.GetNullableString(reader, "Password");

            if (reader.IsColumnExists("IsActive"))
                item.isActive = SqlHelper.GetBoolean(reader, "IsActive");

            return item;
        }

        public static List<UsersModel> TranslateAsUsersList(this SqlDataReader reader)
        {
            var list = new List<UsersModel>();
            while (reader.Read())
            {
                list.Add(TranslateAsUser(reader, true));
            }
            return list;
        }
    }
}
