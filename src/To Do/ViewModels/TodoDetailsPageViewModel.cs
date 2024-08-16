using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maui.NullableDateTimePicker;
using To_Do.Services.Interfaces;
using To_Do.ViewModels.Models;

namespace To_Do.ViewModels;
public partial class TodoDetailsPageViewModel : ObservableObject, IQueryAttributable
{
    private ITodoService _todoService;
    public Func<Task> OnReFocusEditor { get; set; }
    public TodoDetailsPageViewModel(ITodoService todoService)
    {
        _todoService = todoService;
    }

    [ObservableProperty]
    TodoViewModel todoViewModel;

    [ObservableProperty]
    bool isClearedDateTimeVisible;

    [ObservableProperty]
    bool isClearRemindTimeVisible;
    [ObservableProperty]
    DateTime? selectedDate;

    async Task LoadData()
    {
        try
        {
            if(TodoViewModel.DueDate != null)
            {
                IsClearedDateTimeVisible = true;
                SelectedDate = TodoViewModel.DueDate;
            }

        }catch
        {
            return;
        }
    }

    [RelayCommand]
    async void Important()
    {
        TodoViewModel.IsImportant = !TodoViewModel.IsImportant;
    }

    [RelayCommand]
    async void SaveData()
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(TodoViewModel.Name))
            {
                var todoViewModel = new TodoViewModel
                {
                    Id = TodoViewModel.Id,
                    Name = TodoViewModel.Name,
                    IsDone = TodoViewModel.IsDone,
                    IsImportant = TodoViewModel.IsImportant,
                    DueDate = SelectedDate,
                    AddNote = TodoViewModel.AddNote,
                    CreatedDate = TodoViewModel.CreatedDate
                };
                var result = await _todoService.UpdateAsync(todoViewModel);
                await Shell.Current.GoToAsync("..");
                MessagingCenter.Send(this, "RefreshTodoList");
            }
            else
            {
                Toast.Make("Task name is required!", ToastDuration.Short, 12).Show();
            }
        }
        catch
        {
            return;
        }
    }

    [RelayCommand]
    async void DateSelected()
    {
        INullableDateTimePickerOptions nullableDateTimePickerOptions = new NullableDateTimePickerOptions
        {
            NullableDateTime = SelectedDate,
            Mode = PickerModes.Date,
            ShowWeekNumbers = true,
            HeaderBackgroundColor = Color.Parse("#347980"),
            ActivityIndicatorColor = Color.Parse("#347980"),
            ForeColor = Color.Parse("#347980"),
        };
        var result = await NullableDateTimePicker.OpenCalendarAsync(nullableDateTimePickerOptions);
        if (result is PopupResult popupResult && popupResult.ButtonResult != PopupButtons.Cancel)
        {
            SelectedDate = popupResult.NullableDateTime;
        }

        if (OnReFocusEditor != null)
        {
            await OnReFocusEditor.Invoke();
        }
    }
    [RelayCommand]
    async void DeleteTodo()
    {
        try
        {
            var ask = await Application.Current.MainPage.DisplayAlert("Warning", $"Are you sure delete {TodoViewModel.Name}", "OK", "Cancel");
            if (ask is false) return;
            var result = await _todoService.DeleteAsync(TodoViewModel);
            MessagingCenter.Send(this, "RefreshTodoList");
            await Shell.Current.GoToAsync($"..");
        }
        catch (Exception ex)
        {

        }
    }

    [RelayCommand]
    async void DateClear()
    {
        try
        {
            SelectedDate = null;
            IsClearedDateTimeVisible = false;
        }
        catch
        {
            return;
        }
    }


    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.ContainsKey("TodoViewModel"))
        {
            TodoViewModel = (TodoViewModel)query.Values.First();
            await LoadData();

        }
    }
}
