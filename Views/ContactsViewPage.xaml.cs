
using PhoneBook.ViewModels;

namespace PhoneBook.Views;

public partial class ContactsViewPage : ContentPage
{
    public ContactsViewPage()
    {
        InitializeComponent();
        BindingContext = new ContactsPageViewModel();

    }
}