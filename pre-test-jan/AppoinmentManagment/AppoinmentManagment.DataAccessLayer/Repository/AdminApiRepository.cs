using AppoinmentManagment.BusinessLayer;
using AppoinmentManagment.DataAccessLayer.IRepository;
using AppoinmentManagment.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AppoinmentManagment.DataAccessLayer.Repository
{
    public class AdminApiRepository : IAdminApiRepository
    {
        private readonly IConfiguration _config;
        private readonly ILogger<AdminApiRepository> _logger;

        public AdminApiRepository(IConfiguration config, ILogger<AdminApiRepository> logger)
        {
            _config = config;
            _logger = logger;
        }

        public int CountPatient()
        {
            string query = $"SELECT COUNT([OId])as patient FROM[Hospital].[dbo].[User] WHERE[TypeId] = 2; ";
            int result = 0;
            _logger.LogInformation("Getting patient count..");

            string connectionString = _config["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = query;
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    try
                    {
                        while (dataReader.Read()) //make it single user
                        {
                            result = Convert.ToInt32(dataReader["patient"]);
                        }
                    }
                    catch (NullReferenceException e)
                    {
                        _logger.LogWarning($"'{e}' Exception");
                    }
                }
                connection.Close();
            }
            return result;
        }

        public int CountDoctor()
        {
            string query = $"SELECT COUNT([OId])as doctor FROM[Hospital].[dbo].[User] WHERE[TypeId] = 3; ";
            int result = 0;
            _logger.LogInformation("Getting Doctor count..");

            string connectionString = _config["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = query;
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    try
                    {
                        while (dataReader.Read()) //make it single user
                        {
                            result = Convert.ToInt32(dataReader["doctor"]);
                        }
                    }
                    catch (NullReferenceException e)
                    {
                        _logger.LogWarning($"'{e}' Exception");
                    }
                }
                connection.Close();
            }
            return result;
        }

        public int CountSpecialization()
        {
            string query = $"SELECT COUNT(*) as special FROM [Hospital].[dbo].[Specialization] ";
            int result = 0;
            _logger.LogInformation("Getting Specialization count..");

            string connectionString = _config["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = query;
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    try
                    {
                        while (dataReader.Read()) //make it single user
                        {
                            result = Convert.ToInt32(dataReader["special"]);
                        }
                    }
                    catch (NullReferenceException e)
                    {
                        _logger.LogWarning($"'{e}' Exception");
                    }
                }
                connection.Close();
            }
            return result;
        }

        public int TotalBalance()
        {
            string query = $"SELECT [Balance] FROM [Hospital].[dbo].[Company] ";
            int result = 0;
            _logger.LogInformation("Getting Company Balance count..");

            string connectionString = _config["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string sql = query;
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    try
                    {
                        while (dataReader.Read()) //make it single user
                        {
                            result = Convert.ToInt32(dataReader["Balance"]);
                        }
                    }
                    catch (NullReferenceException e)
                    {
                        _logger.LogWarning($"'{e}' Exception");
                    }
                }
                connection.Close();
            }
            return result;
        }

        public List<UserBO> GetAllPatientList()
        {
            string query = $"SELECT  [OId],[TypeId],[Name],[Address],[Gender],[DOB],[Phone],[Email],[Password] FROM[Hospital].[dbo].[User] WHERE[TypeId] = 2";
            List<UserBO> ubol = new List<UserBO>();
            string connectionString = _config["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = query;
                SqlCommand command = new SqlCommand(sql, connection);
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read()) //make it single user
                    {
                        try
                        {
                            if (dataReader != null)
                            {
                                UserBO ubo = new UserBO()
                                {
                                    OId = Convert.ToInt32(dataReader["OId"]),
                                    Name = dataReader["Name"].ToString(),
                                    Phone = dataReader["Phone"].ToString(),
                                    Address = dataReader["Address"].ToString(),
                                    DOB = dataReader["DOB"].ToString(),
                                };
                                ubol.Add(ubo);
                            }
                        }
                        catch (NullReferenceException e)
                        {
                            _logger.LogError($"'{e}' Exception");
                        }
                    }
                }
                connection.Close();
            }

            //UserBO obj = new UserBO
            //{
            //    UserList = ubol
            //};

            return ubol;
        }
    }
}
