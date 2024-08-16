namespace To_Do.DataAccess.Models;

public class TodoModel : Entity
{
    public string AddNote { get; set; }
    public bool IsImportant { get; set; }
    public bool IsDone { get; set; }
    public DateTime? DueDate { get; set; }
}
