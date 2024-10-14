using PhoneBook.Views;
namespace PhoneBook
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AddContactsPage();
        }
    }
}
