using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsApp.Templates;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsApp.LaunchProcessForms
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Boiling : Page
    {
        public Connection con;
        public string inputMessage;

        private bool heater = false;
        private bool mixer = false;

        public Boiling()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (Boiling)e.Parameter;

            con = parameters.con;
            inputMessage = parameters.inputMessage;

            updateData(inputMessage);
        }

        private void updateData(string mes)
        {

            string[] data = mes.Split(';');

            //Power
            powerBlock.Text = data[4].Substring(5);

            //Temp
            tankBlock.Text = Math.Round(Convert.ToDouble(data[7].Substring(4)) / 100, 1).ToString();         

            //TargTemp
            //targetTankBlock.Text = "/" + data[13].Substring(0);
            //targetColumnBlock.Text = "/" + data[14].Substring(0);

            //Pressure
            pressureBlock.Text = pressureBlock.Text = Math.Round((Convert.ToDouble(data[16].Substring(12)) / 100), 2).ToString();

            //HeatButton
            string heat = data[5].Substring(4);

            //MixerButton
            string mix = data[17].Substring(5);

            //PumpButton
            string pum = data[27].Substring(4);

            //Highlighting
            if (heat.Equals("1"))
            {
                heatingButton.BorderBrush = new SolidColorBrush(Color.FromArgb(60, 10, 141, 16));
                heater = true;
            }
            else
            {
                heatingButton.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                heater = false;
            }

            if (mix.Equals("1"))
            {
                mixerButton.BorderBrush = new SolidColorBrush(Color.FromArgb(60, 10, 141, 16));
                mixer = true;
            }
            else
            {
                mixerButton.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                mixer = false;
            }             

        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:1;");
            string response = System.Text.Encoding.UTF8.GetString(con.ReadBytes());
            var parameters = new PauseTemplate();
            parameters.con = con;
            Frame.Navigate(typeof(PauseTemplate), parameters);
        }

        private void heatButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setMKey:1;");
            string response = System.Text.Encoding.UTF8.GetString(con.ReadBytes());
            updateData(response);
        }

        private void mixerButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:3;");
            string response = System.Text.Encoding.UTF8.GetString(con.ReadBytes());
            updateData(response);
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void powerButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:2;");
            string response = System.Text.Encoding.UTF8.GetString(con.ReadBytes());
            var power_parameters = new PowerTemplate();
            power_parameters.con = con;
            power_parameters.inputMessage = response;
            Frame.Navigate(typeof(PowerTemplate), power_parameters);
        }

        private void notifyButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
