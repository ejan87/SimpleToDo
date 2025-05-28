using SimpleToDo.Data;
using SimpleToDo.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace SimpleToDo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<TaskItem> tasks;
        public MainWindow()
        {
            InitializeComponent();
            TaskDataGrid.Items.Clear();
            tasks = TaskStorage.LoadTasks();
            TaskDataGrid.ItemsSource = tasks;
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text.Trim();
            TextRange textRange = new(DescriptionBox.Document.ContentStart, DescriptionBox.Document.ContentEnd);
            string description = textRange.Text.Trim();
            DateTime? dueDate = DueDatePicker.SelectedDate;

            if(string.IsNullOrEmpty(title) || dueDate == null)
            {
                MessageBox.Show("vyplň datum a název úkolu.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var newTask = new TaskItem 
            { 
                Title = title,
                Description = description,
                DueDate = dueDate.Value,
                IsCompleted = false
            };


            tasks.Add(newTask);
            TaskStorage.SaveTasks(tasks);
            
            TitleTextBox.Clear();
            DescriptionBox.Document.Blocks.Clear();
            DueDatePicker.SelectedDate = null;
            TitleTextBox.Focus();
        }

        private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if(TaskDataGrid.SelectedItem is TaskItem selectedTask)
            {
                if (MessageBox.Show("Opravdu chcete tento úkol smazat?", "Potvrzení", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    tasks.Remove(selectedTask);
                    TaskStorage.SaveTasks(tasks);
                }
            }
            else
            {
                MessageBox.Show("Vyberte úkol, který chcete smazat.", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void TaskDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(TaskDataGrid.SelectedItem is TaskItem selectedTask)
            {
                TitleTextBox.Text = selectedTask.Title;
                DescriptionBox.Document.Blocks.Clear();
                DescriptionBox.Document.Blocks.Add(new Paragraph(new Run(selectedTask.Description)));
                DueDatePicker.SelectedDate = selectedTask.DueDate;
            }
        }

        private void EditTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (TaskDataGrid.SelectedItem is TaskItem selectedTask)
            {
                string newTitle = TitleTextBox.Text.Trim();
                TextRange textRange = new(DescriptionBox.Document.ContentStart, DescriptionBox.Document.ContentEnd);
                string newDescription = textRange.Text.Trim();
                DateTime? newDueDate = DueDatePicker.SelectedDate;

                if (string.IsNullOrEmpty(newTitle) || newDueDate == null)
                {
                    MessageBox.Show("Vyplň datum a název úkolu.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                selectedTask.Title = newTitle;
                selectedTask.Description = newDescription;
                selectedTask.DueDate = newDueDate.Value;

                TaskDataGrid.Items.Refresh();
                TaskStorage.SaveTasks(tasks);
                MessageBox.Show("Úkol upraven", "Hotovo", MessageBoxButton.OK, MessageBoxImage.Information);

                TitleTextBox.Clear();
                DescriptionBox.Document.Blocks.Clear();
                DueDatePicker.SelectedDate = null;
                TitleTextBox.Focus();

            }
            else
            {
                MessageBox.Show("Vyberte úkol k úpravě.", "Upozornění", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}