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

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsApp.ProcessPropertiesVeiw
{
    
    public sealed partial class Distilation : Page
    {
        public Connection con;
        public string inputMessage;

        public Distilation()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (Distilation)e.Parameter;

            con = parameters.con;
            inputMessage = parameters.inputMessage;
            updateData(inputMessage);

        }

        private void updateData(string mes)
        {

            string[] data = mes.Split(';');
           // powerBlock.Text = data[4];

            // tankBlock.Text = data[]; index 8??
            // columnBlock.Text = data[]; index 11??


            //pressureBlock.Text = (Convert.ToDouble(data[16]) / 100).ToString();

            /*
             * heat/mix/pum - data[?]/data[?]/data[?] indexes?
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

            if (pum.Equals("1"))
            {
                pumpButton.BorderBrush = new SolidColorBrush(Color.FromArgb(60, 10, 141, 16));
                pump = true;
            }
            else
            {
                pumpButton.BorderBrush = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                pump = false;
            }
            */


        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            double finTemp = Convert.ToDouble(finTemperatureBox.Text);
            double closeTemp = Convert.ToDouble(closeTemperatureBox.Text);
            double lowPower = Convert.ToDouble(lowPowerTemperatureBox.Text);
            double offTemp = Convert.ToDouble(offTempBox.Text);
            double workPower = workPowerSlider.Value;
            double startPower = startPowerSlider.Value;
            int mixMode = mixerModeBox.SelectedIndex;
        }

        private void workPowerSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            workPowerBlock.Text = workPowerSlider.Value.ToString();
        }

        private void startPowerSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            startPowerBlock.Text = startPowerSlider.Value.ToString();
        }
    }
}
