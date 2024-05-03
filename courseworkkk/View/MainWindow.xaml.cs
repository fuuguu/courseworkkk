using courseworkkk.View;
using System.Windows;

namespace courseworkkk
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();  
        }

        private void MenuItem_Click_Client(object sender, RoutedEventArgs e)
        {
            ClientWindow clientWindow = new ClientWindow();
            clientWindow.Show();
        }
        private void MenuItem_Click_Executor(object sender, RoutedEventArgs e)
        {
            SalesmanView salesmanView = new SalesmanView();
            salesmanView.Show();
        }
        private void MenuItem_Click_Project(object sender, RoutedEventArgs e)
        {
            CarsView carsView = new CarsView();
            carsView.Show();
        }


        private void Button_ExitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
