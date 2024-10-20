using PhoneBook.Views;
using PhoneBook.Models;
namespace PhoneBook
{
    public partial class App : Application
    {
        public static User? user;
        public App(LoginPage page)
        {
            InitializeComponent();
            
            MainPage = page;
        }
    }
}