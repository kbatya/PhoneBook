using PhoneBook.ViewModels;
namespace PhoneBook.Views;

public partial class ContactDetailsPage : ContentPage
{
   
        public ContactDetailsPage()
        {
            InitializeComponent();
            BindingContext = new ContactDetailsPageViewModel();
        }

}