using To_Do.ViewModels;

namespace To_Do.Page;
//[QueryProperty(nameof(TodoId), nameof(TodoId))]
public partial class TodoDetailsPage : ContentPage
{
    private TodoDetailsPageViewModel _viewModel;
    public TodoDetailsPage(TodoDetailsPageViewModel view)
    {
        InitializeComponent();
        this.BindingContext = _viewModel = view;
        Shell.SetBackButtonBehavior(this, new BackButtonBehavior
        {
            IsVisible = false
        });
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var viewModel = BindingContext as TodoDetailsPageViewModel;
    }
}