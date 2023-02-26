namespace ToDoListApi.Models
{
    public class ToDoListModel
    {
        public int Id { get; set; }
        public string TodoMessage { get; set; } = null!;
        public bool Completed { get; set; }
        //date
        //priority
    }
}
