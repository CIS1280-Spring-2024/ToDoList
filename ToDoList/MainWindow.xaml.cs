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

        public MainWindow()
        {
            InitializeComponent();
            _repo = new ToDoRepo();
            _toDos = _repo.GetToDos();
            lbToDos.ItemsSource = _toDos;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Create a new ToDo object
            ToDo toDo = new ToDo();
            toDo.Title = txbTitle.Text;
            toDo.Description = txbDesciption.Text;
            toDo.Created = dtCreateDate.SelectedDate.Value;
            toDo.Completed = cbCompleted.IsChecked.Value;

            // Add the new ToDo object to database
            toDo.Id = _repo.AddToDo(toDo);

            // Add the new ToDo object to the list
            _toDos.Add(toDo);

            // Refresh the list
            lbToDos.Items.Refresh();
        }
    }
}