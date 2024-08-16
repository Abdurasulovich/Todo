using CommunityToolkit.Maui.Core.Platform;
using CommunityToolkit.Maui.Views;
using To_Do.ViewModels;

namespace To_Do.Page;

public partial class AddPartPage : Popup
{
	private AddPartViewModel _viewModel;
	public AddPartPage(AddPartViewModel view)
	{
		InitializeComponent();
		this.BindingContext = _viewModel =view;
    }

    private async void TaskEditor_Focused(object sender, FocusEventArgs e)
    {
		if(!KeyboardExtensions.IsSoftKeyboardShowing(TaskEditor))
			await KeyboardExtensions.ShowKeyboardAsync(TaskEditor, default);
    }
}