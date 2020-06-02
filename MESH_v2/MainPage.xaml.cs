using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DataAccessLib;
using Windows.Storage;
// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace MESH_v2
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {

            DataAccessClass.InitializeDatabase();
            this.InitializeComponent();
            gg.Text = ApplicationData.Current.LocalFolder.Path;
        }

        private void goAdmin_Click(object sender, RoutedEventArgs e)
        {

            this.Frame.Navigate(typeof(AdminMenu));
        }

        private void LoginMenuConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LoginMenuLoginBox.Text != "" && LoginMenuPasswordBox.Password != "")
            {
                switch( DataAccessClass.ValidateUser(LoginMenuLoginBox.Text, LoginMenuPasswordBox.Password))
                {
                    case 0:
                        this.Frame.Navigate(typeof(AdminMenu));
                        break;
                    case 1:
                        this.Frame.Navigate(typeof(TeacherMenu));
                        break;
                    case 2:
                        break;
                    default:
                        break;
                }
            }
        }

        private void goTecher_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(TeacherMenu));
            
        }

        private void REInitializeDB_Click(object sender, RoutedEventArgs e)
        {
            DataAccessClass.REInitializeDatabase();
        }
    }
}
