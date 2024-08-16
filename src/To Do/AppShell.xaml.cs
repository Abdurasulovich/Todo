using To_Do.Page;

namespace To_Do
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(Routes.TodoDetailsPage, typeof(TodoDetailsPage));
            Routing.RegisterRoute(Routes.MainPage, typeof(MainPage));
        }
    }
}
