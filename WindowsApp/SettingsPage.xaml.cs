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

namespace WindowsApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public Connection con;
        public string inputMessage;

        public SettingsPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (SettingsPage)e.Parameter;

            con = parameters.con;      

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            string SSID = NetNameBox.Text;
            string pswd = passwordBox.Password;
            con.SendData("getSet:0;ssid:" + SSID + ";pass:" + pswd + ";");
            Frame.GoBack();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void defaultButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
