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
using WindowsApp.Templates;
using WindowsApp.LaunchProcessForms;

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
                
                string[] split_array = response.Split(';');
                string[] screen = split_array[0].Split(':');
                string screen_id = screen[1];
            
                switch (screen_id)
                {
                    case ("1"):
                        int position = response.IndexOf("ShowTemp");
                        string process_id = response.Substring(position+ 8, 1);
                        switch (process_id)
                        {
                            case ("1"):
                                var manual_parameters = new Manual();
                                manual_parameters.con = connection;
                                manual_parameters.inputMessage = response;
                                Frame.Navigate(typeof(Manual), manual_parameters);
                                break;
                            case ("2"):
                                var distillation_parameters = new TemperatureTemplate();
                                distillation_parameters.con = connection;
                                distillation_parameters.inputMessage = response;
                                Frame.Navigate(typeof(TemperatureTemplate), distillation_parameters);
                                break;
                            case ("4"):
                                var beerwort_parameters = new BeerWort();
                                beerwort_parameters.con = connection;
                                beerwort_parameters.inputMessage = response;
                                Frame.Navigate(typeof(BeerWort), beerwort_parameters);
                                break;
                            case ("5"):
                                var rectification_parameters = new TemperatureTemplate();
                                rectification_parameters.con = connection;
                                rectification_parameters.inputMessage = response;
                                Frame.Navigate(typeof(TemperatureTemplate), rectification_parameters);
                                break;
                            case ("6"):
                                var fermentation_parameters = new Fermentation();
                                fermentation_parameters.con = connection;
                                fermentation_parameters.inputMessage = response;
                                Frame.Navigate(typeof(Fermentation), fermentation_parameters);
                                break;
                            case ("8"):
                                var boiling_parameters = new Boiling();
                                boiling_parameters.con = connection;
                                boiling_parameters.inputMessage = response;
                                Frame.Navigate(typeof(Boiling), boiling_parameters);
                                break;

                        }
                        break;
                    case ("2"):
                        var pause_parameters = new PauseTemplate();
                        pause_parameters.con = connection;
                        pause_parameters.inputMessage = response;
                        Frame.Navigate(typeof(PauseTemplate), pause_parameters);
                        break;
                    case ("3"):
                        var power_parameters = new PowerTemplate();
                        power_parameters.con = connection;
                        power_parameters.inputMessage = response;
                        Frame.Navigate(typeof(RecipesPage), power_parameters);
                        break;
                    case ("4"):
                        var recipe_parameters = new RecipesPage();
                        recipe_parameters.con = connection;
                        recipe_parameters.inputMessage = response;
                        Frame.Navigate(typeof(RecipesPage), recipe_parameters);
                        break;
                    case ("5"):
                        var preprocess_parameters = new TemperatureTemplate();
                        preprocess_parameters.con = connection;
                        preprocess_parameters.inputMessage = response;
                        Frame.Navigate(typeof(TemperatureTemplate), preprocess_parameters);
                        break;
                    case ("6"):
                             
                        break;
                }
                           
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
                
                string[] tmp = textBoxRequest.Text.Split(':');
                string s = tmp[1].Substring(0, tmp.Length - 1);
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
