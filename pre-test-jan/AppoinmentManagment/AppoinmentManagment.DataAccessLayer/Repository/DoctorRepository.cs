using AppoinmentManagment.BusinessLayer;
using AppoinmentManagment.DataAccessLayer.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace AppoinmentManagment.DataAccessLayer.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly IConfiguration _config;
        private readonly ILogger<DoctorRepository> _logger;
        private readonly IUserRepository _user;

        public DoctorRepository(IConfiguration config, ILogger<DoctorRepository> logger, IUserRepository user)
        {
            _config = config;
            _logger = logger;
            _user = user;
        }

        public List<DoctorBO> GetDoctorBySpecialization(int id)
        {
            string query = $"SELECT * FROM[Hospital].[dbo].[Doctor] WHERE SpecializationId = '{id}'";

            _logger.LogInformation("Entered in Get doctor..");
            List<DoctorBO> dbol = new List<DoctorBO>();

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
                        while (dataReader.Read()) 
                        {
                            //DoctorBO dbo = new DoctorBO()
                            //{
                            //    DrId = Convert.ToInt32(dataReader["DrId"]),
                            //    UserId = Convert.ToInt32(dataReader["OId"]),
                            //    Name = _user.GetUserName(Convert.ToInt32(dataReader["OId"])).ToString(),
                            //    SpecializationId = Convert.ToInt32(dataReader["SpecializationId"])
                            //};
                            dbol.Add(dbo);
                        }
                    }
                    catch (NullReferenceException e)
                    {
                        _logger.LogWarning($"'{e}' Exception");
                    }
                }
                connection.Close();
            }
            return dbol;
        }
    }
}
