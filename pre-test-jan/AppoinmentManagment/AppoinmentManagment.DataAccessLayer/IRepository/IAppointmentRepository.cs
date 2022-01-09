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

        AppoinmentBO GetAllAppoinmentByDrId(string DrId);

    }
}
