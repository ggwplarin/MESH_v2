using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace MESH_v2
{
    
    public sealed partial class TeacherMenu : Page
    {
        string selectedDiscipline;
        string selectedGroup;
        int selectedStudentId;
        string selectedMark;


        ObservableCollection<string> groups = new ObservableCollection<string>();
        ObservableCollection<User> students = new ObservableCollection<User>();


        public TeacherMenu()
        {
            groups = DataAccessClass.GetGroupsTitles(false);
           
            this.InitializeComponent();
        }

        private void GroupSelectionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedGroup = (sender as ComboBox).SelectedItem as string;
            students = DataAccessClass.GetUsersFromGroup(selectedGroup);

        }

        private void MarkSelectionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedMark = (sender as ComboBox).SelectedItem as string;
        }

        private void DisciplineSelectionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedDiscipline = (sender as ComboBox).SelectedItem as string;
        }

        private void StudentSelectionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedStudentId = ((sender as ComboBox).SelectedItem as User).Id;

        }

        private void AddMarkBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTimeOffset date = MarkDatePicker.Date ?? DateTimeOffset.Now;
            DataAccessClass.AddMark(selectedStudentId,date, selectedDiscipline, selectedMark);
        }
    }
}
