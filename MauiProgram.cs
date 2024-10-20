using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.PlatformConfiguration;
using PhoneBook.Services;
using PhoneBook.ViewModels;
using PhoneBook.Views;

namespace PhoneBook
{
    public static class MauiProgram
    {

        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MaterialSymbolsOutlined.ttf", "MaterialSymbols");
                    fonts.AddFont("gadi-almog-regular-aaa.otf", "AlmogRegular");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            #region Dependency Inject the views, viewmodel and builder
            
            builder.RegisterViews().RegisterServices().RegisterViewModels();


            #endregion
            return builder.Build();
        }
        public static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<AddContactsPage>();
            builder.Services.AddSingleton<LoginPage>();
            builder.Services.AddSingleton<ContactDetailsPage>();
            builder.Services.AddSingleton<ContactsViewPage>();
            return builder;
        }
        public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<AddContactsPageViewModel>();
            builder.Services.AddSingleton<LoginPageViewModel>();
            builder.Services.AddSingleton<ContactDetailsPageViewModel>();
            builder.Services.AddSingleton<ContactsPageViewModel>();
            return builder;
        }
        public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<IContactService,ContactService>();
            return builder;
        }
    }
}

