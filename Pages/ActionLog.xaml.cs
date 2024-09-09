using Project0.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project0.Pages
{
    /// <summary>
    /// Логика взаимодействия для ActionLog.xaml
    /// </summary>
    public partial class ActionLog : Page
    {
        public ActionLog()
        {
            InitializeComponent();

            LoadActionLogs();
        }

        private void LoadActionLogs()
        {
            var actionLogs = OdbConnecHelper.entObj.ActionLog
                .OrderByDescending(log => log.ActionDate)  // Сортировка по дате действия
                .ToList();  // Приводим к списку

            // Отображаем данные в DataGrid
            ActionLogDataGrid.ItemsSource = actionLogs;
        }
    }
}
