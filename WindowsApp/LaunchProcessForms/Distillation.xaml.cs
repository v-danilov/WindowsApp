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
    public sealed partial class Distillation : Page
    {
        public Connection con;

        private bool heater = false;


        public Distillation()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (Distillation)e.Parameter;

            con = parameters.con;

        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:1;");
            var parameters = new PauseTemplate();
            parameters.con = con;
            Frame.Navigate(typeof(PauseTemplate));
        }

        private void launchButton_Click(object sender, RoutedEventArgs e)
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


        private void powerButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:2;");
            //Frame.Navigate(powerSettings);
        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:4;");
            //Frame.Navigate(StageSettings);
        }

        private void notifyButton_Click(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(StageSettings);
        }
    }
}
