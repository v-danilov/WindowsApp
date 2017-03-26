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
    public sealed partial class Manual : Page
    {
        public Connection con;
        private bool heater = false;
        private bool mixer = false;
        private bool pump = false;

        public Manual()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (Manual)e.Parameter;

            con = parameters.con;

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

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:1;");
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
            con.SendData("setKey:3;");
        }

        private void powerButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:2;");
            //Navigate
        }

        private void notifyButton_Click(object sender, RoutedEventArgs e)
        {
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
            
            con.SendData("setMKey:3;");
        }
    }
}
