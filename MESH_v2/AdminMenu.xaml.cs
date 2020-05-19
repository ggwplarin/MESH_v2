using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Collections.ObjectModel;
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
// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace MESH_v2
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>

    
    public sealed partial class AdminMenu : Page
    {
        ObservableCollection<User> users = new ObservableCollection<User>();
        public AdminMenu()
        {
            
            DataAccessClass.InitializeDatabase();
            DataAccessClass.AddUser("ggff", "dsgd", "gegggg");
            DataAccessClass.AddUser("grwf", "dsge", "gegggg");
            DataAccessClass.AddUser("gghgsf", "dsgd", "gegggg");
            DataAccessClass.AddUser("rrff", "dsed", "gegggg");
            DataAccessClass.AddUser("jgsff", "ddegd", "gegggg");
            DataAccessClass.AddUser("ggdff", "dsgd", "gegggg");
            DataAccessClass.AddUser("grgwf", "dsge", "gegggg");
            DataAccessClass.AddUser("gghdgsf", "dsgd", "gegggg");
            DataAccessClass.AddUser("rrfff", "dsed", "gegggg");
            DataAccessClass.AddUser("jgfff", "ddegd", "gegggg");
            users = DataAccessClass.GetUsers();
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));
            users.Add(new User(1, "jgfff", "ddegd", "gegggg"));

            this.InitializeComponent();
        }

        private void DeleteSelectedUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddNewUserBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
