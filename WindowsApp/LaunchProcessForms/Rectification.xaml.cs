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
    public sealed partial class Rectification : Page
    {
        public Connection con;
        public string inputMessage;

        private bool heater = false;
        private bool mixer = false;
        private bool drill = false;

        public Rectification()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (Rectification)e.Parameter;

            con = parameters.con;
            inputMessage = parameters.inputMessage;

            string[] data = inputMessage.Split(';');

            powerBlock.Text = data[4];

            // tankBlock.Text = data[?]; 
            // columnBlock.Text = data[?]; 
            // setBlock.Text = data[?]; 

            //buttonHighliting(data[?], data[?], data[?], data[?]) 

            //pressureBlock.Text = (Convert.ToDouble(data[?]) / 100).ToString(); 

        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:1");
            var parameters = new PauseTemplate();
            parameters.con = con;
            Frame.Navigate(typeof(PauseTemplate));
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

        private void buttonHighliting(string heat, string mix, string dr, string set)
        {
            if (heat.Equals("1"))
            {
                heatingButton.BorderBrush = new SolidColorBrush(Color.FromArgb(60, 10, 141, 16));
                heater = true;
            }

            if (mix.Equals("1"))
            {
                mixerButton.BorderBrush = new SolidColorBrush(Color.FromArgb(60, 10, 141, 16));
                mixer = true;
            }

            if (dr.Equals("1"))
            {
                drillButton.BorderBrush = new SolidColorBrush(Color.FromArgb(60, 10, 141, 16));
                drill = true;
            }
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

        private void notifyButton_Click(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(StageSettings);
        }

        private void setButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void drillButton_Click(object sender, RoutedEventArgs e)
        {
            //Режим бур. Отображение в форме
        }
    }
}
