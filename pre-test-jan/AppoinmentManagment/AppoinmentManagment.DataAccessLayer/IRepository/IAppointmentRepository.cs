using AppoinmentManagment.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppoinmentManagment.DataAccessLayer.IRepository
{
    public interface IAppointmentRepository
    {
        bool AppointmentAlreadyExists(AppoinmentBO abo, int id);
        int Add(AppoinmentBO abo, int id, string name, string appointId);

        List<AppoinmentBO> GetAllAppoinmentByDrId(string DrId);

        int DeclineAppoinment(string id, string name);
        int ApproveAppoinment(string id, string name);

        int CountPendingAppointment(string id);

        List<AppoinmentBO> GetApprovedAppointmentPatientId(int id);

        string GetAppointedDoctorId(string id);

        int UpdateAppointmentPayment(string id, string name);
    }
}
