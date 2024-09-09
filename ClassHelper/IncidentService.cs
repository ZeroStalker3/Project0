using Project0.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project0.ClassHelper
{
    public class IncidentService
    {
        private readonly LogService _logService = new LogService();

        public void AddIncident(IncidentRecord incident, int userId, int employeeId)
        {
            // Добавляем инцидент в базу через OdbConnecHelper
            OdbConnecHelper.entObj.IncidentRecord.Add(incident);
            OdbConnecHelper.entObj.SaveChanges();

            // Логируем добавление инцидента
            _logService.LogAction($"Пользователь {userId} добавил инцидент: {incident.Description}", userId, incident.Id, employeeId);
        }

        public void UpdateIncident(IncidentRecord incident, int userId, int employeeId)
        {
            // Изменяем состояние сущности на Modified
            OdbConnecHelper.entObj.Entry(incident).State = EntityState.Modified;

            // Сохраняем изменения в базе данных
            OdbConnecHelper.entObj.SaveChanges();

            // Логируем обновление инцидента
            _logService.LogAction($"Пользователь {userId} обновил инцидент: {incident.Description}", userId, incident.Id, employeeId);
        }

        public void DeleteIncident(int incidentId, int userId, int employeeId)
        {
            var incident = OdbConnecHelper.entObj.IncidentRecord.Find(incidentId);
            if (incident != null)
            {
                OdbConnecHelper.entObj.IncidentRecord.Remove(incident);
                OdbConnecHelper.entObj.SaveChanges();

                // Логируем удаление инцидента
                _logService.LogAction($"Пользователь {userId} удалил инцидент: {incident.Description}", userId, incidentId, employeeId);
            }
        }
    }

}
