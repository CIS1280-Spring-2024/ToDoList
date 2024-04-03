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
using ToDoList.Models;
using ToDoList.Repos;

namespace ToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ToDoRepo _repo;
        private List<ToDo> _toDos;
        private ToDo _selectedToDo;

        public MainWindow()
        {
            InitializeComponent();
            _repo = new ToDoRepo();
            _toDos = _repo.GetToDos();
            lbToDos.ItemsSource = _toDos;
            _selectedToDo = new ToDo();
            InitializeControlsFromTodo(_selectedToDo);
        }

        private void InitializeControlsFromTodo(ToDo _toDo)
        {
            txbTitle.Text = _toDo.Title;
            txbDesciption.Text = _toDo.Description;
            dtCreateDate.SelectedDate = _toDo.Created;
            cbCompleted.IsChecked = _toDo.Completed;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Create a new ToDo object
            ToDo toDo = new ToDo();
            GetToDoFromControls(toDo);

            // Add the new ToDo object to database
            toDo.Id = _repo.AddToDo(toDo);

            // Add the new ToDo object to the list
            _toDos.Add(toDo);

            // Refresh the list
            lbToDos.Items.Refresh();
        }

        private void GetToDoFromControls(ToDo toDo)
        {
            toDo.Title = txbTitle.Text;
            toDo.Description = txbDesciption.Text;
            toDo.Created = dtCreateDate.SelectedDate.Value;
            toDo.Completed = cbCompleted.IsChecked.Value;
        }

        private void lbToDos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            _selectedToDo = (ToDo)lbToDos.SelectedItem;
            InitializeControlsFromTodo(_selectedToDo);
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            GetToDoFromControls(_selectedToDo);
            _repo.UpdateToDo(_selectedToDo);
            lbToDos.Items.Refresh();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            _selectedToDo = (ToDo)lbToDos.SelectedItem;
            if (_selectedToDo != null)
            {
                if (MessageBox.Show("Are you sure you want to delete this ToDo?", "Delete ToDo", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    _repo.DeleteToDo(_selectedToDo.Id);
                    _toDos.Remove(_selectedToDo);
                    lbToDos.Items.Refresh();
                }
            }
        }
    }
}