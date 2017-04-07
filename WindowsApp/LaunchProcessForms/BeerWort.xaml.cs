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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsApp.LaunchProcessForms
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BeerWort : Page
    {
        public Connection con;
        public string inputMessage;

        private bool heater = false;
        private bool mixer = false;
        private bool pump = false;

        public BeerWort()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (BeerWort)e.Parameter;

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
            targetTankBlock.Text = "/" + Math.Round(Convert.ToDouble(data[13].Substring(0)) / 100, 1).ToString();
            //targetColumnBlock.Text = "/" + data[14].Substring(0);

            //Pressure
            pressureBlock.Text = Math.Round((Convert.ToDouble(data[16].Substring(12)) / 100), 2).ToString();

            //HeatButton
            string heat = data[5].Substring(4);

            //MixerButton
            string mix = data[17].Substring(5);

            //PumpButton
            string pmp = data[20].Substring(5);


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

            if (pmp.Equals("1"))
            {
                pumpButton.BorderBrush = new SolidColorBrush(Color.FromArgb(60, 10, 141, 16));
                pump = false;
            }
            else
            {
                pumpButton.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                pump = false;
            }



        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:1");
            var parameters = new PauseTemplate();
            parameters.con = con;
            Frame.Navigate(typeof(PauseTemplate));
        }

        private void heatingButton_Click(object sender, RoutedEventArgs e)
        {
            if (!heater)
            {
                heatingButton.BorderBrush = new SolidColorBrush(Color.FromArgb(60, 10, 141, 16));
                heater = true;
            }
            else
            {
                heatingButton.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                heater = false;
            }
            con.SendData("setMKey:1;");
        }

        private void mixerButton_Click(object sender, RoutedEventArgs e)
        {
            if (!mixer)
            {
                mixerButton.BorderBrush = new SolidColorBrush(Color.FromArgb(60, 10, 141, 16));
                mixer = true;
            }
            else
            {
                mixerButton.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                mixer = false;
            }
            con.SendData("setKey:3");
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:4;");
            //Frame.Navigate(StageSettings);
        }

        private void powerButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:2");
            //Navigate
        }

        private void pumpButton_Click(object sender, RoutedEventArgs e)
        {
            if (!pump)
            {
                pumpButton.BorderBrush = new SolidColorBrush(Color.FromArgb(60, 10, 141, 16));
                pump = true;
            }
            else
            {
                pumpButton.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                pump = false;
            }

            con.SendData("setMKey:3");
        }
    }
}
