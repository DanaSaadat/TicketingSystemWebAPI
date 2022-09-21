using DataAccess.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;


namespace DataAccess
{
    public class UserDAL
    {
        string conString = ConfigurationManager.ConnectionStrings["TicketingSystemConnection"].ToString();

        public List<Login> GetAllEmployee()
        {
            List<Login> UserList = new List<Login>();

            using (SqlConnection Connection = new SqlConnection(conString))

            {
                SqlCommand command = Connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[sp_GetAllEmployee]";
                SqlDataAdapter SqlDa = new SqlDataAdapter(command);

                DataTable dtStudents = new DataTable();
                Connection.Open();
                SqlDa.Fill(dtStudents);
                Connection.Close();
                //Department department = new Department();
                foreach (DataRow dr in dtStudents.Rows)
                {
                    Department dpt = new Department();


                    //dpt.ID = Convert.ToInt32(dr["ID"]);// here found student Id 
                    dpt.Name = Convert.ToString(dr["DeptName"]);

                    UserList.Add(new Login
                    {

                        UserID = Convert.ToInt32(dr["UserID"]),
                        UserName = Convert.ToString(dr["UserName"]),
                        Password = Convert.ToString(dr["Password"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        Email = Convert.ToString(dr["Email"]),
                        Mobile = Convert.ToString(dr["Mobile"]),
                        DepartmentID = Convert.ToInt32(dr["DepartmentID"]),
                        Department = dpt,
                        //dpt.Name = Convert.ToString(dr["DeptName"]),



                    });
                }


            }
            return UserList;
        }



        public bool InsertUser(Login Login, int[] Permission) // void 
        {
            // int id = 0;
            
            using (SqlConnection Connection = new SqlConnection(conString))

            {
                SqlCommand command = new SqlCommand("[dbo].[sp_insertUsers]", Connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserName", Login.UserName);
                command.Parameters.AddWithValue("@Password", Login.Password);
                command.Parameters.AddWithValue("@FirstName", Login.FirstName);
                command.Parameters.AddWithValue("@LastName", Login.LastName);
                command.Parameters.AddWithValue("@Email", Login.Email);
                command.Parameters.AddWithValue("@Mobile", Login.Mobile);
                command.Parameters.AddWithValue("@IsClient", Login.IsClient);
                command.Parameters.AddWithValue("@DepartmentID", Login.DepartmentID);
                command.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                PermissionUser obj1 = new PermissionUser();


                Connection.Open();
                int i = command.ExecuteNonQuery();
                i = 1;


                if (Permission != null)
                {

                    foreach (var xx in Permission)
                    {

                        obj1.PermissionID = xx;

                        obj1.UserID = Convert.ToInt32(command.Parameters["@id"].Value);

                        InsertUserPermisiion(obj1.PermissionID, obj1.UserID);
                    }
                }

                Connection.Close();
                if (i >= 0)
                {

                    return true;

                }
                else
                {

                    return false;
                }


            }
        }



        public bool InsertUserPermisiion(int? PermissionID, int? UserID)
        {
            // int id = 0;

            using (SqlConnection Connection = new SqlConnection(conString))

            {
                SqlCommand command = new SqlCommand("[dbo].[sp_insertUsersPermision]", Connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@PermissionID", PermissionID);
                command.Parameters.AddWithValue("@UserID", UserID); 
                
             
               

                Connection.Open();
                int i = command.ExecuteNonQuery();
                i = 1;

                Connection.Close();
                if (i >= 0)
                {

                    return true;

                }
                else
                {

                    return false;
                }


            }
        }



        public List<Login> GetUserById(int ID) 
        {
            List<Login> UserList = new List<Login>();

            using (SqlConnection Connection = new SqlConnection(conString))

            {
                SqlCommand command = Connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[GetUserBYId]";
                command.Parameters.AddWithValue("@ID", ID);
                SqlDataAdapter SqlDa = new SqlDataAdapter(command);

                DataTable dtStudents = new DataTable();
                Connection.Open();
                SqlDa.Fill(dtStudents);
                Connection.Close();

                foreach (DataRow dr in dtStudents.Rows)
                {
                    //Department dpt = new Department();


                    //dpt.ID = Convert.ToInt32(dr["ID"]);// here found student Id 
                    //dpt.Name = Convert.ToString(dr["DeptName"]);
                    UserList.Add(new Login
                    {

                        UserID = Convert.ToInt32(dr["UserID"]),
                        UserName = Convert.ToString(dr["UserName"]),
                        Password = Convert.ToString(dr["Password"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        Email = Convert.ToString(dr["Email"]),
                        Mobile = Convert.ToString(dr["Mobile"]),
                        DepartmentID = Convert.ToInt32(dr["DepartmentID"]),
                        //Department = dpt,
                        //dpt.Name = Convert.ToString(dr["DeptName"]),



                    });
                }


            }
            return UserList;
        }


        public bool UpdateUser(Login Login, int[] Permission)
        {
            // int id = 0;

            using (SqlConnection Connection = new SqlConnection(conString))

            {
                SqlCommand command = new SqlCommand("[dbo].[sp_UpdateUsers]", Connection);

                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@UserID", Login.UserID);
                command.Parameters.AddWithValue("@UserName", Login.UserName);
                command.Parameters.AddWithValue("@Password", Login.Password);
                command.Parameters.AddWithValue("@FirstName", Login.FirstName);
                command.Parameters.AddWithValue("@LastName", Login.LastName);
                command.Parameters.AddWithValue("@Email", Login.Email);
                command.Parameters.AddWithValue("@Mobile", Login.Mobile);
                command.Parameters.AddWithValue("@IsClient", Login.IsClient);
                command.Parameters.AddWithValue("@DepartmentID", Login.DepartmentID);
                //command.Parameters.Add("@id", SqlDbType.Int).Direction = ParameterDirection.Output;
                PermissionUser obj1 = new PermissionUser();


                Connection.Open();
                int i = command.ExecuteNonQuery();
                i = 1;


                //if (Permission != null)
                //{

                //    foreach (var xx in Permission)
                //    {

                //        obj1.PermissionID = xx;

                //        obj1.UserID = Convert.ToInt32(command.Parameters["@id"].Value);

                //        InsertUserPermisiion(obj1.PermissionID, obj1.UserID);
                //    }
                //}

                Connection.Close();
                if (i >= 0)
                {

                    return true;

                }
                else
                {

                    return false;
                }


            }
        }
        public bool DeleteUser(int ID)
        {
            using (SqlConnection Connection = new SqlConnection(conString))

            {
                SqlCommand command = new SqlCommand("[dbo].[sp_deleteUserById]", Connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@ID", ID);

                Connection.Open();
                int i = command.ExecuteNonQuery();

                i = 1;
                Connection.Close();
                if (i >= 1)
                {
                    return true;
                }
                else
                {

                    return false;
                }


            }
        }

        public List<Login> GetAllEmployeepagedList(int PageNumber,int PageSize)
        {
            List<Login> UserList = new List<Login>();

            using (SqlConnection Connection = new SqlConnection(conString))

            {
                SqlCommand command = Connection.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[PageListUser]";
                command.Parameters.AddWithValue("@PageNumber", PageNumber);
                command.Parameters.AddWithValue("@PageSize", PageSize);
                SqlDataAdapter SqlDa = new SqlDataAdapter(command);

                DataTable dtStudents = new DataTable();
                Connection.Open();
                SqlDa.Fill(dtStudents);
                Connection.Close();
                //Department department = new Department();
                foreach (DataRow dr in dtStudents.Rows)
                {
                    Department dpt = new Department();


                    //dpt.ID = Convert.ToInt32(dr["ID"]);// here found student Id 
                    dpt.Name = Convert.ToString(dr["DeptName"]);

                    UserList.Add(new Login
                    {

                        UserID = Convert.ToInt32(dr["UserID"]),
                        UserName = Convert.ToString(dr["UserName"]),
                        Password = Convert.ToString(dr["Password"]),
                        FirstName = Convert.ToString(dr["FirstName"]),
                        LastName = Convert.ToString(dr["LastName"]),
                        Email = Convert.ToString(dr["Email"]),
                        Mobile = Convert.ToString(dr["Mobile"]),
                        DepartmentID = Convert.ToInt32(dr["DepartmentID"]),
                        Department = dpt,
                        //dpt.Name = Convert.ToString(dr["DeptName"]),



                    });
                }


            }
            return UserList;
        }
    }
}
