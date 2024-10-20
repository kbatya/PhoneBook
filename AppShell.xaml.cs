using PhoneBook.Views;
using System.Windows.Input;
using PhoneBook.Models;

namespace PhoneBook
{
    public partial class AppShell : Shell
    {
        public static User? user;
        public ICommand LogoutCommand =>
            new Command(() => {
                App.user = null;
                
                Shell.Current.DisplayAlert("Log Out", "Good buy!", "Ok");

                Shell.Current.GoToAsync("Login");
                Shell.Current.FlyoutIsPresented = false;

            });
        public AppShell()
        {
            InitializeComponent();
            BindingContext = this;
            #region 
            Routing.RegisterRoute("/Details", typeof(ContactDetailsPage));
            Routing.RegisterRoute("Registration", typeof(RegistrationPage));
            Routing.RegisterRoute("Login", typeof(LoginPage));
            #endregion
        }

    }
}