using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Networking.Sockets;
using Windows.Networking;
using System.IO;
using System;
using Windows.UI.Popups;
using System.Diagnostics;
using Windows.ApplicationModel.Core;
using System.Text;

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WindowsApp
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public Connection connection;
        public MainPage()
        {
            this.InitializeComponent();
            connection = new Connection();
            connection.Connect();
            Debug.WriteLine("Connection");
        }


        private void NetworkState_Click(object sender, RoutedEventArgs e)
        {
           // Frame.Navigate(typeof(StatusPage));
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            CoreApplication.Exit();
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            //connection.SendData("settings;");
            var parameters = new SettingsPage();
            parameters.con = connection;
            Frame.Navigate(typeof(SettingsPage), parameters);
        }

         private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.SendData("refresh;");
                string response = System.Text.Encoding.UTF8.GetString(connection.ReadBytes());
                var parameters = new RecipesPage();
                parameters.con = connection;
                parameters.inputMessage = response;
                Frame.Navigate(typeof(RecipesPage), parameters);            
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc.StackTrace); //Ctrl+Alt+O
            }
        }

        private void ScrollViewer_ViewChanged(object sender, ScrollViewerViewChangedEventArgs e)
        {

        }

        private void Connection_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxRequest.Text.StartsWith("rec"))
            {
                //byte nul = 0;
                string[] tmp = textBoxRequest.Text.Split(':');
                string s = tmp[1].Substring(0, 1);
                int i = Convert.ToInt32(s);
                char c = Convert.ToChar(i);

                

                string str = tmp[0] + ":" + c + ";";

                
                char[] mes = str.ToCharArray();
                connection.SendChar(mes);
            }
            else
            {
                connection.SendData(textBoxRequest.Text);
            }
            textBoxResponse.Text= System.Text.Encoding.UTF8.GetString(connection.ReadBytes());
        }
    }
}
