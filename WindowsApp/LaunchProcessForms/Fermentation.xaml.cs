using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Fermentation : Page
    {
        public Connection con;
        public string inputMessage;

        public Fermentation()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (Fermentation)e.Parameter;

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

        }

        private void heatButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mixerButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void changeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void powerButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void notifyButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
