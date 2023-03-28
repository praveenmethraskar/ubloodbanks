using BloodManagement.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;

namespace BloodManagement.Repository
{
    public class UserRL : IUserRL
    {

        string dbpath = "Data Source=DESKTOP-6SI061U;Initial Catalog=Bloodbank;Integrated Security=True";

        SqlConnection sqlConnection;
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment webHostEnvironment;
        public List<UserRegistrationModel> userlist = new List<UserRegistrationModel>();
        public List<AddMoreDetails> userprofile = new List<AddMoreDetails>();
        public List<HospitalModel> hosp = new List<HospitalModel>();
        public List<Donations> donate = new List<Donations>();
        public UserRL(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        public UserRegistrationModel Registration(UserRegistrationModel employee)
        {
            try
            {
                sqlConnection = new SqlConnection(dbpath);
                SqlCommand command = new SqlCommand("SP_Register", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                sqlConnection.Open();

                // command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                command.Parameters.AddWithValue("@fullname", employee.fullname);
                command.Parameters.AddWithValue("@mail", employee.mail);
                command.Parameters.AddWithValue("@password", employee.Password);
                command.Parameters.AddWithValue("@confirmpassword", employee.ConfirmPassword);

                command.ExecuteNonQuery();
                return employee;
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

        public UserRegistrationModel UserLogin(UserLoginModel loginModel)
        {
            sqlConnection = new SqlConnection(dbpath);
            using (sqlConnection)
            {
                try
                {
                    SqlCommand command = new SqlCommand("EmployeeLogin", sqlConnection);
                    command.CommandType = System.Data.CommandType.StoredProcedure;


                    sqlConnection.Open();
                    command.Parameters.AddWithValue("@mail", loginModel.Email);
                    command.Parameters.AddWithValue("@password", loginModel.Password);

                    var result = command.ExecuteScalar();
                    if (result != null)
                    {
                        string query = "SELECT * FROM users  WHERE mail = '" + result + "'";
                        SqlCommand cmd = new SqlCommand(query, sqlConnection);
                        SqlDataReader reader = cmd.ExecuteReader();
                        //var EmployeeId = cmd.ExecuteScalar();
                        UserRegistrationModel objregistration = new UserRegistrationModel();
                        while (reader.Read())
                        {
                            objregistration.users_id = Convert.ToInt32(reader["users_id"]);
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

        //private string GenerateSecurityToken(string Email, string users_id)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //    var claims = new[] {
        //        new Claim(ClaimTypes.Role,"Employee"),
        //        new Claim(ClaimTypes.Email,Email),
        //        new Claim("users_id",users_id.ToString())
        //    };
        //    var token = new JwtSecurityToken(Configuration["JWT:key"],
        //      Configuration["JWT:key"],
        //      claims,
        //      expires: DateTime.Now.AddMinutes(60),
        //      signingCredentials: credentials);
        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}




        public IEnumerable<UserRegistrationModel> Getusers(int users_id)
        {

            sqlConnection = new SqlConnection(dbpath);
            try
            {
                SqlCommand command = new SqlCommand("Getusers", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                sqlConnection.Open();
                command.Parameters.AddWithValue("users_id", users_id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        userlist.Add(new UserRegistrationModel
                        {
                            users_id = Convert.ToInt32(reader["users_id"]),
                            fullname = reader["fullname"].ToString(),
                            mail = reader["mail"].ToString(),

                        });
                    }
                    return userlist;
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




        public AddMoreDetails Profile(AddMoreDetails employee,int users_id)
        {
            try
            {
                //string uniqueFileName = UploadedFile(employee);

                sqlConnection = new SqlConnection(dbpath);
                SqlCommand command = new SqlCommand("SP_Profile", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                sqlConnection.Open();

                // command.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
                command.Parameters.AddWithValue("@users_id", users_id);
                command.Parameters.AddWithValue("@dob", employee.DoB);
                command.Parameters.AddWithValue("@Bloodgroup", employee.Bloodgroup);
                command.Parameters.AddWithValue("@mobilenumber", employee.mobile);
                command.Parameters.AddWithValue("@address1", employee.address1);
                command.Parameters.AddWithValue("@district", employee.district);
                command.Parameters.AddWithValue("@states", employee.states);
                command.Parameters.AddWithValue("@pincode", employee.pincode);
               // command.Parameters.AddWithValue("@imagepath", uniqueFileName);
                
                command.ExecuteNonQuery();
                return employee;
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
       
        public IEnumerable<AddMoreDetails> Getprofile(int users_id)
        {

            sqlConnection = new SqlConnection(dbpath);
            try
            {
                SqlCommand command = new SqlCommand("Getprofile", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                sqlConnection.Open();
                command.Parameters.AddWithValue("users_id", users_id);

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
        //private string UploadedFile(UserRegistrationModel user)
        //{
        //    string uniqueFileName = null;

        //    if (user.image_path != null)
        //    {
        //        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");
        //        uniqueFileName = Guid.NewGuid().ToString() + "_" + user.image_path.FileName;
        //        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //            user.image_path.CopyTo(fileStream);
        //        }
        //    }
        //    return uniqueFileName;
        //}
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
                            HId = Convert.ToInt32(reader["Hid"]),
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


        public Donations EnterDonations(Donations donations, int users_id)
        {
            try
            {
                sqlConnection = new SqlConnection(dbpath);
                SqlCommand command = new SqlCommand("SP_enterdonations", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                sqlConnection.Open();

                command.Parameters.AddWithValue("@users_id", users_id);
                command.Parameters.AddWithValue("@L_date", donations.date);
                command.Parameters.AddWithValue("@H_id", donations.hospital);
                //command.Parameters.AddWithValue("@branchid", enterMetersReadings.BranchName);
                command.ExecuteNonQuery();
                return donations;
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


        public IEnumerable<Donations> Listdonations(int users_id)
        {

            sqlConnection = new SqlConnection(dbpath);
            try
            {
                SqlCommand command = new SqlCommand("ListDonated", sqlConnection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                sqlConnection.Open();
                command.Parameters.AddWithValue("users_id", users_id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        donate.Add(new Donations
                        {
                            //users_id = Convert.ToInt32(reader["users_id"]),
                            //HId = Convert.ToInt32(reader["H_id"]),
                            //Id = Convert.ToInt32(reader["d_id"]),
                            fullname = reader["fullname"].ToString(),
                            date = reader["L_date"].ToString(),
                            hospital = reader["HospitalName"].ToString(),


                        });
                    }
                    return donate;
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
