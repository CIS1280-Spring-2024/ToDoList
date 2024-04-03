namespace ToDoList.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime Created { get; set; } = DateTime.Now;
        public bool Completed { get; set; } = false;

        public override string ToString()
        {
            return Title;
        }
    }
}
