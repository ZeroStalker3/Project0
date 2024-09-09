using Project0.DataBase;
using System;

namespace Project0.ClassHelper
{
    class LogService
    {
        public void LogAction(string actionDescription, int userId, int incidentId, int employeeId)
        {
            var log = new ActionLog
            {
                ActionDescription = actionDescription,
                ActionDate = DateTime.Now,
                Id_User = userId,
                Id_Incident = incidentId,
                Id_employee = employeeId
            };

            // Добавляем лог в базу через OdbConnecHelper
            OdbConnecHelper.entObj.ActionLog.Add(log);
            OdbConnecHelper.entObj.SaveChanges();  // Сохраняем изменения в базе данных
        }
        public void LogUserLogin(int userId)
        {
            var log = new ActionLog
            {
                ActionDate = DateTime.Now,
                ActionDescription = "Вход в систему",
                UserId = userId
            };

            OdbConnecHelper.entObj.ActionLog.Add(log);
            OdbConnecHelper.entObj.SaveChanges();
        }
        public void LogUserLogout(int userId)
        {
            var log = new ActionLog
            {
                ActionDate = DateTime.Now,
                ActionDescription = "Выход из системы",
                UserId = userId
            };

            OdbConnecHelper.entObj.ActionLog.Add(log);
            OdbConnecHelper.entObj.SaveChanges();
        }

    }

}
