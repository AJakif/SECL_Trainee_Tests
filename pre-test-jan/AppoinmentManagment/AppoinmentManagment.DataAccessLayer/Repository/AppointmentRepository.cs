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
    public class AppointmentRepository : IAppointmentRepository
    {

        private readonly ILogger<AppointmentRepository> _logger;
        private readonly IConfiguration _config;

        public AppointmentRepository(ILogger<AppointmentRepository> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }

        public int Add(AppoinmentBO abo, int id, string name)
        {
            string Query = $"INSERT INTO [dbo].[Appoinment]([PatientId],[DoctorId],[AppointmentDate],[AppointmentTime],[AppointmentStatus],[Symptom],[Medication],[Created_at],[Created_by])" +
                            $"VALUES('{id}','{abo.DoctorId}','{abo.AppointmentDate}','{abo.AppointmentTime}','{abo.AppointmentStatus}','{abo.Symptom}','{abo.Medication}', GetDate(),'{name}')";

            int Result;
            string connectionString = _config["ConnectionStrings:DefaultConnection"];
            using SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            try
            {
                string sql = Query;
                SqlCommand command = new SqlCommand(sql, connection);
                Result = command.ExecuteNonQuery();
                _logger.LogInformation("Data Inserted");
                connection.Close();
                return Result;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"'{e}' Exception..");
                connection.Close();
                return -1;
            }
        }

        public bool appointmentAlreadyExists(AppoinmentBO abo,int id)
        {
            string query = $"SELECT  [OId] FROM [Hospital].[dbo].[Appoinment] WHERE [PatientId] = '{id}' ,[DoctorId] = '{abo.DoctorId}' ,[AppointmentDate] = '{abo.AppointmentDate}' ";
            _logger.LogInformation("Entered in AppointmentAlreadyExists..");
            bool flag = false;
            string connectionString = _config["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string sql = query;
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataReader dr = command.ExecuteReader();
                try
                {
                    if (dr.HasRows)
                    {
                        flag = true;
                        _logger.LogInformation("Appointment Already Exist..");
                    }
                }
                catch (Exception e)
                {
                    _logger.LogWarning($"'{e}' Exception");
                }

                connection.Close();

            }
            return flag;
        }
    }
}
