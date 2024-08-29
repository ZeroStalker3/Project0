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
using Project0.ClassHelper;
using Project0.DataBase;
using Project0.Pages;

namespace Project0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            FrameApp.frmObj = MainFrm;
            //NextPageForMain.frmObj = FrmPage;
            OdbConnecHelper.entObj = new DataBaseForProjectFEntities();
            FrameApp.frmObj.Navigate(new Author());
        }

        private void Auth_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
