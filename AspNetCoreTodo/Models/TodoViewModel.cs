namespace AspNetCoreTodo.Models
{

    //array of todos passed to the views
    public class TodoViewModel
    {
        public TodoItem[] Items { get; set; }
    }
}