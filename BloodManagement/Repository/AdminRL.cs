using BloodManagement.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Reflection;


namespace BloodManagement.Repository
{
    public class AdminRL : IAdminRL
    {
        string dbpath = "Data Source=DESKTOP-6SI061U;Initial Catalog=Bloodbank;Integrated Security=True";
        SqlConnection sqlConnection;
        public IConfiguration Configuration { get; }
        public List<AddMoreDetails> userprofile = new List<AddMoreDetails>();
        public List<HospitalModel> hosp = new List<HospitalModel>();
        public AdminRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public AdminRegisterModel Register(AdminRegisterModel admin)
        {
            try
            {
                sqlConnection = new SqlConnection(dbpath);
                SqlCommand command = new SqlCommand("SP_Admin1", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                sqlConnection.Open();

                // command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                command.Parameters.AddWithValue("@fullname", admin.fullname);
                command.Parameters.AddWithValue("@mail", admin.mail);
                command.Parameters.AddWithValue("@password", admin.Password);
                command.Parameters.AddWithValue("@confirmpassword", admin.ConfirmPassword);

                command.ExecuteNonQuery();
                return admin;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public AdminRegisterModel adminLogin(AdminLoginModel loginModel)
        {
            sqlConnection = new SqlConnection(dbpath);
            using (sqlConnection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("AdminLogin", sqlConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;


                    sqlConnection.Open();
                    command.Parameters.AddWithValue("@mail", loginModel.Email);
                    command.Parameters.AddWithValue("@password", loginModel.Password);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string query = "SELECT * FROM adminuser  WHERE mail = '" + result + "'";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection);
                        SqlDataReader reader = cmd.ExecuteReader();
                        //var EmployeeId = cmd.ExecuteScalar();
                        AdminRegisterModel objregistration = new AdminRegisterModel();
                        while (reader.Read())
                        {
                            objregistration.admin_id = Convert.ToInt32(reader["admin_id"]);
                            objregistration.fullname = reader["fullname"].ToString();
                            objregistration.mail = reader["mail"].ToString();
                            objregistration.Password = reader["password"].ToString();
                        }
                        //var token = GenerateSecurityToken(loginModel.Email, EmployeeId.ToString());
                        //return token;
                        return objregistration;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    sqlConnection.Close();
                }

            }
        }



        public IEnumerable<AddMoreDetails> Getdetails()
        {

            sqlConnection = new SqlConnection(dbpath);
            try
            {
                SqlCommand command = new SqlCommand("GetDetails", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                sqlConnection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userprofile.Add(new AddMoreDetails
                        {
                            //users_id = Convert.ToInt32(reader["users_id"]),
                            fullname = reader["fullname"].ToString(),
                            mail = reader["mail"].ToString(),
                            DoB = reader["dob"].ToString(),
                            Bloodgroup = reader["Bloodgroup"].ToString(),
                            mobile = Convert.ToInt64(reader["mobilenumber"]),
                            address1 = reader["address1"].ToString(),
                            district = reader["district"].ToString(),
                            states = reader["states"].ToString(),
                            pincode = Convert.ToInt32(reader["pincode"])

                        });
                    }
                    return userprofile;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }


        public HospitalModel Addhospitals(HospitalModel hospital)
        {
            try
            {
                //string uniqueFileName = UploadedFile(employee);

                sqlConnection = new SqlConnection(dbpath);
                SqlCommand command = new SqlCommand("SP_Hospitals", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                sqlConnection.Open();

                // command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                //command.Parameters.AddWithValue("@users_id", users_id);
                command.Parameters.AddWithValue("@HospitalName", hospital.HospitalName);
                // command.Parameters.AddWithValue("@imagepath", uniqueFileName);

                command.ExecuteNonQuery();
                return hospital;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }
        public IEnumerable<HospitalModel> GetHospitals()
        {

            sqlConnection = new SqlConnection(dbpath);
            try
            {
                SqlCommand command = new SqlCommand("SP_ListHospitals", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                sqlConnection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        hosp.Add(new HospitalModel
                        {
                            //users_id = Convert.ToInt32(reader["users_id"]),
                            HospitalName = reader["HospitalName"].ToString()
                        });
                    }
                    return hosp;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

    }
}
