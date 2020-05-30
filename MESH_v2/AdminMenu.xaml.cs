using DataAccessLib;
using Microsoft.Toolkit.Extensions;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
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

namespace MESH_v2
{



    public sealed partial class AdminMenu : Page
    {
        ObservableCollection<User> users = new ObservableCollection<User>();
        ObservableCollection<string> roles = new ObservableCollection<string>{"Admin","Teacher","Student"};
        public ObservableCollection<Discipline> disciplines = new ObservableCollection<Discipline>();
        public ObservableCollection<Discipline> selectedDisciplines = new ObservableCollection<Discipline>();

        
        public AdminMenu()
        {



            DataAccessClass.AddDiscipline("asfdssfffssds", 1, false);
            DataAccessClass.AddDiscipline("qffs", 1, false);
            DataAccessClass.AddDiscipline("gsdc", 1, false);
            DataAccessClass.AddDiscipline("qqwf", 1, false);
            DataAccessClass.AddDiscipline("bgsd", 1, false);
            DataAccessClass.AddDiscipline("ooms", 1, false);
            DataAccessClass.AddDiscipline("bxxa", 1, false);
            DataAccessClass.AddDiscipline("qwrm", 1, false);

            users = DataAccessClass.GetUsers();
            disciplines = DataAccessClass.GetDisciplines();
            
            this.InitializeComponent();
        }

        private void DeleteSelectedUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedIndex != -1)
            {
                DataAccessClass.DeleteUser(users[UsersGrid.SelectedIndex].Id);
                users = DataAccessClass.GetUsers();
                
                
            }
        }

        private void AddNewUserBtn_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void AddUserConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            string role = "";
            string group = "";
            if ($"{AddNewUserRoleCBox.SelectedItem as string}" == "Student")
            {
                group = $"{AddNewUserGroupCBox.SelectedItem as string}";
            }
            role = $"{AddNewUserRoleCBox.SelectedItem as string}";
            if (AddNewUserRoleCBox.SelectedIndex != -1&& AddUserLoginBox.Text != ""&& AddUSerPasswordBox.Password != "")
            {
                DataAccessClass.AddUser(AddUserLoginBox.Text, AddUSerPasswordBox.Password, role, group);
                users = DataAccessClass.GetUsers();
                UsersGrid.ItemsSource = users;

                AddUserLoginBox.Text = "";
                AddUSerPasswordBox.Password = "";
                AddNewUserRoleCBox.SelectedIndex = -1;
                AddNewUserGroupCBox.SelectedIndex = -1;
                AddNewUserFlyout.Hide();
            }
        }
        
        

        private void AddNewUserRoleCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (AddNewUserRoleCBox.SelectedIndex == 2)
            {
                AddNewUserGroupCBox.IsEnabled = true;
            }
            else
            {
                AddNewUserGroupCBox.IsEnabled = false;
            }
        }

        private void UsersGrid_BeginningEdit(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridBeginningEditEventArgs e)
        {

        }

        private void UsersGrid_CellEditEnded(object sender, Microsoft.Toolkit.Uwp.UI.Controls.DataGridCellEditEndedEventArgs e)
        {
            if (users[UsersGrid.SelectedIndex].Role != "Student")
            {
                users[UsersGrid.SelectedIndex].Group = "";
                
            }
            DataAccessClass.ChangeUserData(users[UsersGrid.SelectedIndex].Id, users[UsersGrid.SelectedIndex].Login, users[UsersGrid.SelectedIndex].Password, users[UsersGrid.SelectedIndex].Role, users[UsersGrid.SelectedIndex].Group);
            
            

        }


        private void AddNewGroupBtn_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }


        public List<Discipline> searchDisciplines(string query)
        {
            
            return disciplines.Where(
            d => d.Title.IndexOf(query, StringComparison.OrdinalIgnoreCase) != -1).
            OrderByDescending(i => i.Title.StartsWith(query, StringComparison.CurrentCultureIgnoreCase)).ThenBy(i => i.Id).ToList();
        }

        private void DisciplineSelectionASBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if(args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                sender.ItemsSource = searchDisciplines(DisciplineSelectionASBox.Text).Take(10);
            }
            else if (args.Reason == AutoSuggestionBoxTextChangeReason.SuggestionChosen)
            {
                
            }
            
        }

        private void DisciplineSelectionASBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            
            
            
        }

        private void DisciplineSelectionASBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                if (!selectedDisciplines.Contains(args.ChosenSuggestion as Discipline))
                {
                    sender.Text = "";
                    selectedDisciplines.Add(args.ChosenSuggestion as Discipline);
                }
            }
            else if (!String.IsNullOrEmpty(args.QueryText))
            {
                if (disciplines.Select(d => d.Title).Contains(args.QueryText))
                {
                    if (!selectedDisciplines.Contains(disciplines.Where(d => d.Title == args.QueryText).First()))
                    {
                        sender.Text = "";
                        selectedDisciplines.Add(disciplines.Where(d => d.Title == args.QueryText).First());
                    }
                }
            }
        }

        private void AddNewDisciplineBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SelectedDisciplinesGridView_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            selectedDisciplines.Remove((sender as GridView).SelectedItem as Discipline);
            
        }

        private void GroupAddConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (!String.IsNullOrWhiteSpace(AddGroupTitleBox.Text) && selectedDisciplines.Count > 0) 
            {
                DataAccessClass.AddGroup(AddGroupTitleBox.Text,selectedDisciplines.Select(d => $"{d.Id}|").ToString());
                selectedDisciplines.Clear();
                AddGroupTitleBox.Text = "";
            }

        }
    }
}
