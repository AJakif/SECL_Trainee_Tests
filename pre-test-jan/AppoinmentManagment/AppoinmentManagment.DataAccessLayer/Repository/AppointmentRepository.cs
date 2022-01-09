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

        public int Add(AppoinmentBO abo, int id, string name, string appointId)
        {
            string Query = $"INSERT INTO [dbo].[Appoinment]([AppointId],[PatientId],[DoctorId],[AppointmentDate],[AppointmentTime],[AppointmentStatus],[Symptom],[Medication],[Created_at],[Created_by])" +
                            $"VALUES('{appointId}','{id}','{abo.DoctorId}','{abo.AppointmentDate}','{abo.AppointmentTime}','Pending','{abo.Symptom}','{abo.Medication}', GetDate(),'{name}')";

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

        public bool AppointmentAlreadyExists(AppoinmentBO abo, int id)
        {
            string query = $"SELECT  [AppointId] FROM [Hospital].[dbo].[Appoinment] WHERE [PatientId] = '{id}' AND [DoctorId] = '{abo.DoctorId}'AND [AppointmentDate] = '{abo.AppointmentDate}'AND [AppointmentStatus] = 'Pending' OR [AppointmentStatus] = 'Active'";
            _logger.LogInformation("Entered in AppointmentAlreadyExists..");
            bool flag = false;
            string connectionString = _config["ConnectionStrings:DefaultConnection"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                
                try
                {
                    connection.Open();
                    string sql = query;
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader dr = command.ExecuteReader();
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

        public AppoinmentBO GetAllAppoinmentByDrId(string DrId)
        {
            AppoinmentBO abo = new AppoinmentBO();
            string Query = $"SELECT [AppointId],[PatientId],[DoctorId],[AppointmentDate],[AppointmentTime],[AppointmentStatus],[Symptom],[Medication] FROM [Hospital].[dbo].[Appoinment]  where [DoctorId] = '{DrId}'";
            try
            {
                string connectionString = _config["ConnectionStrings:DefaultConnection"];
                using SqlConnection connection = new SqlConnection(connectionString);

                try
                {
                    connection.Open();
                    string sql = Query;
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader dr = command.ExecuteReader();
                    using SqlDataReader dataReader = command.ExecuteReader();
                    try
                    {
                        while (dataReader.Read()) //make it single user
                        {

                        }
                    }
                    catch (NullReferenceException e)
                    {
                        _logger.LogWarning($"'{e}' Exception");
                    }
                }
                catch (Exception e)
                {
                    _logger.LogWarning($"'{e}' Exception");
                }

                connection.Close();
            }
            catch(Exception )
            {

            }
            return abo;
        }
    }
}
