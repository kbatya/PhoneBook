
using PhoneBook.ViewModels;

namespace PhoneBook.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage(LoginPageViewModel vm)
    {
        
        InitializeComponent();
        this.BindingContext = vm;

    }
}
