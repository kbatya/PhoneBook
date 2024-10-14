using PhoneBook.Models;
using PhoneBook.ViewModels;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
namespace PhoneBook.Views;

public partial class AddContactsPage : ContentPage
{
	public AddContactsPage()
	{
		InitializeComponent();
        BindingContext = new AddContactsPageViewModel();
    }
}