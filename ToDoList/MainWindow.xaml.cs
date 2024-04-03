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
    }
}