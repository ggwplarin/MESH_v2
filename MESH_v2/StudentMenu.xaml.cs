using DataAccessLib;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace MESH_v2
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class StudentMenu : Page
    {
        private ObservableCollection<StudentMark> marks = new ObservableCollection<StudentMark>();
        private ObservableCollection<Discipline> disciplines = new ObservableCollection<Discipline>();
        private ObservableCollection<StudentMark> selectedDisciplineMarks = new ObservableCollection<StudentMark>();
        private User thatStudent;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                thatStudent = e.Parameter as User; ;
                marks = new ObservableCollection<StudentMark>(DataAccessClass.GetStudentsMarks().Where(m => m.stId == thatStudent.Id));
                disciplines = new ObservableCollection<Discipline>(DataAccessClass.GetDisciplines().Where(d =>
                (DataAccessClass.GetGroups().Where(g => g.Title == (thatStudent.Group)).FirstOrDefault()).DisciplinesIds.Split('|').Select(t => Convert.ToInt32(t)).Contains(d.Id)));
            }
        }

        public StudentMenu()
        {
            this.InitializeComponent();
            DisplayBadCredentialsDialog();
        }

        private void DisciplineSelectionCBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)
            {
                selectedDisciplineMarks = new ObservableCollection<StudentMark>(marks.Where(m => m.DisciplineId == ((sender as ComboBox).SelectedItem as Discipline).Id));
                selectedDisciplineMarks.Add(new StudentMark(1, DateTimeOffset.Now, 1, "5", ""));
                MarksGridView.ItemsSource = selectedDisciplineMarks;
            }
        }
        private async void DisplayBadCredentialsDialog()
        {
            ContentDialog noWifiDialog = new ContentDialog()
            {
                Title = "Ошибка!",
                Content = "Проверьте правильность введенных данных.",
                CloseButtonText = "Продолжить"
            };

            await noWifiDialog.ShowAsync();
        }
    }
}