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
using WindowsApp.ProcessPropertiesVeiw;
using WindowsApp.Templates;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsApp.LaunchProcessForms
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Distillation : Page
    {
        public Connection con;
        public string inputMessage;
        private bool heater = false;
        private bool mixer = false;


        public Distillation()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (Distillation)e.Parameter;

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
            columnBlock.Text = Math.Round(Convert.ToDouble(data[8].Substring(0)) / 100, 1).ToString();

            //TargTemp
            targetTankBlock.Text = "/" + Math.Round(Convert.ToDouble(data[12].Substring(8)) / 10, 1).ToString();
        //targetColumnBlock.Text = "/" + data[14].Substring(0);

       
            //Pressure
            pressureBlock.Text = Math.Round((Convert.ToDouble(data[16].Substring(12)) / 100), 2).ToString();

            //HeatButton
            string heat = data[5].Substring(4);

            //MixerButton
            string mix = data[17].Substring(5);


            //Highlighting
            if (heat.Equals("1"))
            {
                heatingButton.Background = new SolidColorBrush(Color.FromArgb(60, 10, 141, 16));
                heater = true;
            }
            else
            {
                heatingButton.Background = new SolidColorBrush(Color.FromArgb(60, 0, 0, 0));
                heater = false;
            }

            if (mix.Equals("1"))
            {
                mixerButton.Background = new SolidColorBrush(Color.FromArgb(60, 10, 141, 16));
                mixer = true;
            }
            else
            {
                mixerButton.Background = new SolidColorBrush(Color.FromArgb(60, 0, 0, 0));
                mixer = false;
            }

        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:1;");
            string response = System.Text.Encoding.UTF8.GetString(con.ReadBytes());
            var parameters = new PauseTemplate();
            parameters.con = con;
            parameters.inputMessage = response;
            Frame.Navigate(typeof(PauseTemplate), parameters);
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

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:4;");
            string response = System.Text.Encoding.UTF8.GetString(con.ReadBytes());
            var power_parameters = new Distilation();
            power_parameters.con = con;
            power_parameters.inputMessage = response;
            Frame.Navigate(typeof(Distilation), power_parameters);

        }

        private void notifyButton_Click(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(StageSettings);
        }

        private void heatingButton_Click(object sender, RoutedEventArgs e)
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
    }
}
