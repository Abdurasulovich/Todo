using CommunityToolkit.Maui;
using epj.RouteGenerator;
using Maui.NullableDateTimePicker;
using To_Do.DataAccess;
using To_Do.Page;
using To_Do.Services;
using Microsoft.Extensions.Logging;
using To_Do.Services.Interfaces;
using To_Do.ViewModels;

namespace To_Do
{
    [AutoRoutes("Page")]
    [ExtraRoute("TodoDetailsPage")]
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureNullableDateTimePicker()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddScoped<ITodoService, TodoService>();
            builder.Services.AddTransient<MainPage>();
            builder.Services.AddTransient<TodoDetailsPage>();
            builder.Services.AddScoped<MainViewModel>();
            builder.Services.AddScoped<AddPartViewModel>();
            builder.Services.AddScoped<TodoDetailsPageViewModel>();
            builder.Services.AddSingleton<DataContext>();
            //builder.Services.AddSingleton<TodoStepsContentViewModel>(new TodoStepsService(new DataContext()));

#if DEBUG
            builder.Logging.AddDebug();
#endif


            return builder.Build();
        }
    }
}
