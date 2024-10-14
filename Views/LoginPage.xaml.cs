using PhoneBook.ViewModels;
namespace PhoneBook.Views
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
			BindingContext = new LoginViewModel();
		}
	}
}