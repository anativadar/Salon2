using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.Threading.Tasks;
using Salon2.Models;


namespace Salon2.Data
{
    public class SalonDB
    {
        readonly SQLiteAsyncConnection _database;
        public SalonDB(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Appointment>().Wait();
            _database.CreateTableAsync<Service>().Wait();
            _database.CreateTableAsync<ListService>().Wait();
        }
        public Task<List<Appointment>> GetAppointmensAsync()
        {
            return _database.Table<Appointment>().ToListAsync();
        }
        public Task<Appointment> GetAppointmensAsync(int id)
        {
            return _database.Table<Appointment>()
            .Where(i => i.ID == id)
           .FirstOrDefaultAsync();
        }
        public Task<int> SaveAppointmentAsync(Appointment app)
        {
            if (app.ID != 0)
            {
                return _database.UpdateAsync(app);
            }
            else
            {
                return _database.InsertAsync(app);
            }
        }
        public Task<int> DeleteAppointmentAsync(Appointment app)
        {
            return _database.DeleteAsync(app);
        }

        public Task<int> SaveServiceAsync(Service service)
        {
            if (service.ID != 0)
            {
                return _database.UpdateAsync(service);
            }
            else
            {
                return _database.InsertAsync(service);
            }
        }
        public Task<int> DeleteServiceAsync(Service service)
        {
            return _database.DeleteAsync(service);
        }
        public Task<List<Service>> GetServicesAsync()
        {
            return _database.Table<Service>().ToListAsync();
        }
        public Task<int> SaveListServiceAsync(ListService lists)
        {
            if (lists.ID != 0)
            {
                return _database.UpdateAsync(lists);
            }
            else
            {
                return _database.InsertAsync(lists);
            }
        }
        public Task<List<Service>> GetListServicesAsync(int appid)
        {
            return _database.QueryAsync<Service>(
            "select S.ID, S.Description from Service S"
            + " inner join ListService LS"
            + " on S.ID = LS.ServiceID where LS.AppointmentID = ?",
            appid);
        }
    }
}

    

