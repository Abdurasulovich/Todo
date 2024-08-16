using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using To_Do.Page;
using To_Do.Services.Interfaces;
using To_Do.ViewModels.Models;

namespace To_Do.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private ITodoService _todoService;
    private IServiceProvider _serviceProvider;
    public MainViewModel(ITodoService todoService, IServiceProvider serviceProvider)
    {
        try
        {

            MessagingCenter.Subscribe<TodoDetailsPageViewModel>(this, "RefreshTodoList", (sender) =>
            {
                Refresh(); // Method to reload or refresh the list from the database
            });
        }
        catch
        {

        }
        finally
        {
            _todoService = todoService;
            _serviceProvider = serviceProvider;
            ListDoneTodo = new ObservableCollection<TodoViewModel>();
            ListTodo = new ObservableCollection<TodoViewModel>();
            AddButtonVisible = true;
            IsDoneDown = false;
        }
    }
    [ObservableProperty]
    ObservableCollection<TodoViewModel> listDoneTodo;

    [ObservableProperty]
    ObservableCollection<TodoViewModel> listTodo;

    [ObservableProperty]
    bool addButtonVisible;

    [ObservableProperty]
    bool isComplatedButtonVisible;

    [ObservableProperty]
    bool isDoneDown;
    [ObservableProperty]
    bool isRefreshing;

    [RelayCommand]
    async void Important(TodoViewModel viewModel)
    {
        try
        {

            var existTodo = ListTodo.FirstOrDefault(x=>x.Id== viewModel.Id);
            if(existTodo is not null)
            {
                existTodo.IsImportant = !existTodo.IsImportant;
                var result = await _todoService.UpdateAsync(existTodo);
                if(result > 0)
                {
                    //var index = ListTodo.IndexOf(existTodo);
                    //ListTodo[index].IsImportant = !viewModel.IsImportant;
                    await SortedTodos();
                }
            }
        }
        catch
        {
            return;
        }
    }

    [RelayCommand]
    async Task Done(TodoViewModel viewModel)
    {
        try
        {
            var existingTaskItem = ListTodo.FirstOrDefault(x => x.Id == viewModel.Id);
            if (existingTaskItem is not null)
            {
                var result = await _todoService.ChangeTodoCompletedOrIncompleted(viewModel.Id, viewModel.IsDone);
                if (result is true)
                {
                    var index = ListTodo.IndexOf(existingTaskItem);
                    ListTodo[index].IsDone = !viewModel.IsDone;
                    await SortedTodos();
                    string value = viewModel.IsDone is true ?
                            "Task complated successfully!"
                            : "Task incomplated!";
                    
                    Toast.Make(value, ToastDuration.Short, 12).Show();
                }
            }
        }
        catch
        {
            return;
        }
    }

    private async Task SortedTodos()
    {
        var sortedItem = ListTodo
                        .OrderBy(x => x.IsDone)
                        .ThenBy(x => !x.IsImportant)
                        .ThenByDescending(x => x.CreatedDate)
                        .ToList();
        ListTodo.Clear();
        foreach (var item in sortedItem)
        {
            ListTodo.Add(item);
        }
    }


    [RelayCommand]
    async Task DropDown()
    {
        IsDoneDown = !IsDoneDown;
    }

    [ICommand]
    async void AddButton()
    {
        AddButtonVisible = false;
        AddPartViewModel viewModel = new(_todoService);
        AddPartPage addPart = new(viewModel);

        await Application.Current.MainPage.ShowPopupAsync(addPart);

        AddButtonVisible = true;
        await LoadTodosAsync();
    }

    [RelayCommand]
    async Task Refresh()
    {
        try
        {
            IsRefreshing = true;
            await LoadTodosAsync();

        }
        finally
        {
            IsRefreshing = false;
        }
    }

    public async Task LoadTodosAsync()
    {
        var todos = (await _todoService.GetAll()).OrderBy(x => x.IsDone)
                        .ThenBy(x => !x.IsImportant)
                        .ThenByDescending(x => x.CreatedDate)
                        .ToList();

        ListTodo.Clear();
        foreach (var item in todos)
        {
            ListTodo.Add(item);
        }
    }


    [RelayCommand]
    async void Delete(TodoViewModel viewmodel)
    {
        try
        {
            var answer = await Application.Current.MainPage.DisplayAlert("Warning!", $"Are you sure you want to delete this task? - {viewmodel.Name}", "Yes", "Cancel");

            if (answer)
            {
                ListTodo.Remove(viewmodel);
            await _todoService.DeleteAsync(viewmodel);
            //await SortedTodos();
            }
        }
        catch
        {

        }
    }

    [RelayCommand]
    async void SelectedTab(TodoViewModel viewModel)
    {
        try
        {
            await Shell.Current.GoToAsync($"{Routes.TodoDetailsPage}", new Dictionary<string, object>
            {
                ["TodoViewModel"] = viewModel
            });
        }
        catch
        {
            return;
        }
    }
}
