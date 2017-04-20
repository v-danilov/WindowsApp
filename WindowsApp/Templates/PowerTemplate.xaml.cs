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

namespace WindowsApp.Templates
{

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PowerTemplate : Page
    {

        public Connection con;
        public string inputMessage;

        public PowerTemplate()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (PowerTemplate)e.Parameter;

            con = parameters.con;
            inputMessage = parameters.inputMessage;

            string[] data = inputMessage.Split(';');
            ProcessName.Text = data[1].Substring(4);
            powerSlider.Value = Convert.ToDouble(data[2].Substring(5));
            powerSlider.StepFrequency = Convert.ToDouble(data[3].Substring(9));


        }


        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int p = (int) powerSlider.Value;
            char c = Convert.ToChar(p);
            string tmp = "power:" + c + ";";
            char[] mes = tmp.ToCharArray();
            con.SendChar(mes);
            con.ReadBytes();
            Frame.GoBack();
        }
    }
}
