using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsApp.LaunchProcessForms;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsApp.Templates
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class TemperatureTemplate : Page
    {

        public Connection con;
        public string inputMessage;

        public TemperatureTemplate()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (TemperatureTemplate)e.Parameter;

            con = parameters.con;
            inputMessage = parameters.inputMessage;

            string[] data = inputMessage.Split(';');
            Debug.WriteLine(data[1]);
            ProcessName.Text = data[1].Substring(4);
            temperatureBox.Text = data[2].Substring(4);        

        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string temp = temperatureBox.Text;
            //char c = Convert.ToChar(p);
            string tmp = "temp:" + temp + ";";
            //char[] mes = tmp.ToCharArray();
            //con.SendChar(mes);
            con.SendData(tmp);
            string response = System.Text.Encoding.UTF8.GetString(con.ReadBytes());
            if (response.Contains("PUSH"))
            {
                string message = response.Substring(response.IndexOf("Name") + 4, response.Length - 10);
                var dialog = new MessageDialog(message);
                dialog.Commands.Add(new UICommand { Label = "Продолжить", Id = 1 });
                await dialog.ShowAsync();
                con.SendData("setKey:1;");              
                con.ReadBytes();
                Frame.GoBack();
                return;
            }
            else
            {
                int screenNum = Convert.ToInt32(response.Substring(response.IndexOf("ShowTemp") + 8, 1));
                Debug.WriteLine("Redirecting to screen#" + screenNum);

                switch (screenNum)
                {
                    case 2:
                        var parameters = new Distillation();
                        parameters.con = con;
                        parameters.inputMessage = response;
                        Frame.Navigate(typeof(Distillation), parameters);
                        break;
                    case 5:
                        var parameters_r = new Rectification();
                        parameters_r.con = con;
                        parameters_r.inputMessage = response;
                        Frame.Navigate(typeof(Rectification), parameters_r);
                        break;
                }
            }
            
        }
    }
}
