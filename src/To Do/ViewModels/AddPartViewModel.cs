
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maui.NullableDateTimePicker;
using Microsoft.Toolkit.Mvvm.Input;
using To_Do.Services.Interfaces;
using To_Do.ViewModels.Models;

namespace To_Do.ViewModels;

public delegate Task CloseHandlerAddPopup();
public partial class AddPartViewModel : ObservableObject
{
    private ITodoService _todoService;
    public Func<Task> OnReFocusEditor { get; set; }

    public AddPartViewModel(ITodoService todoService)
    {
        TodoViewModel = new TodoViewModel();
        _todoService = todoService;
        IsChecked = false;
    }

    [ObservableProperty]
    TodoViewModel todoViewModel;
    [ObservableProperty]
    DateTime? selectedDate = null;
    [ObservableProperty]
    string task;
    [ObservableProperty]
    bool isChecked;

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


    //[RelayCommand]
    //async void OnDateSelectedAsync()
    //{
    //    //var selectedDateStyle = Application.Current.Resources["SelectedDayStyle"] as Style;

    //    INullableDateTimePickerOptions nullableDateTimePickerOptions = new NullableDateTimePickerOptions
    //    {
    //        NullableDateTime = SelectedDate,
    //        Mode = PickerModes.Date,
    //        ShowWeekNumbers = true,
    //        HeaderBackgroundColor = Color.Parse("#347980"),
    //        ActivityIndicatorColor = Color.Parse("#347980"),
    //        ForeColor = Color.Parse("#347980"),
    //    };
    //    var result = await NullableDateTimePicker.OpenCalendarAsync(nullableDateTimePickerOptions);
    //    if (result is PopupResult popupResult && popupResult.ButtonResult != PopupButtons.Cancel)
    //    {
    //        SelectedDate = popupResult.NullableDateTime;
    //    }

    //    if (OnReFocusEditor != null)
    //    {
    //        await OnReFocusEditor.Invoke();
    //    }
    //}
    [RelayCommand]
    void Check()
    {
        IsChecked = !IsChecked;
    }

    [RelayCommand]
    async Task AddTodo(Popup pupup)
    {
        try
        {
            if (!string.IsNullOrEmpty(Task))
            {

                var todoViewModel = new TodoViewModel
                {
                    Name = Task,
                    IsDone = IsChecked,
                    IsImportant = IsChecked,
                    DueDate = SelectedDate,
                };
                await _todoService.SaveAsync(todoViewModel);
                Task = "";
                pupup?.Close();
                Toast.Make("New task successfully added!", ToastDuration.Short, 12).Show();
                SelectedDate = null;
            }
        }
        catch
        {

        }
        
    }
}
