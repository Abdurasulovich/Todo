using CommunityToolkit.Mvvm.ComponentModel;

namespace To_Do.ViewModels.Models;

public partial class TodoViewModel : ObservableObject
{
    [ObservableProperty]
    private long id;
    [ObservableProperty]
    private string name;
    [ObservableProperty]
    private string addNote;
    [ObservableProperty]
    private bool isImportant;
    [ObservableProperty]
    private bool isDone;
    [ObservableProperty]
    private DateTime? dueDate;
    [ObservableProperty]
    private DateTime createdDate;
    [ObservableProperty]
    private DateTime updatedDate;
}
