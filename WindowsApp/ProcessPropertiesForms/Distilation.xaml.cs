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
            int startpower = Convert.ToInt32(data[5].Substring(5, data[5].Length - 5));
            int mixerMode = Convert.ToInt32(data[6].Substring(data[6].Length-1));
            int powerStep = Convert.ToInt32(data[7].Substring(9, data[7].Length-9));
            int finTemp = Convert.ToInt32(data[8].Substring(8, data[8].Length - 8))/10;
            int closeTemp = Convert.ToInt32(data[9])/10;
            int lowPowerTemp = Convert.ToInt32(data[10])/10;
            int workPower = Convert.ToInt32(data[12].Substring(6, data[12].Length-6))/10;

            finTemperatureBox.Text = finTemp.ToString();
            closeTemperatureBox.Text = closeTemp.ToString();
            lowPowerTemperatureBox.Text = lowPowerTemp.ToString();
            workPowerSlider.Value = workPower;
            workPowerSlider.StepFrequency = powerStep;
            startPowerSlider.Value = startpower;
            startPowerSlider.StepFrequency = powerStep;
            mixerModeBox.SelectedIndex = mixerMode;


        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            int finTemp = Convert.ToInt32(finTemperatureBox.Text);
           int closeTemp = Convert.ToInt32(closeTemperatureBox.Text);
            int lowPower = Convert.ToInt32(lowPowerTemperatureBox.Text);
            int offTemp = Convert.ToInt32(offTempBox.Text);
            int workPower = (int)workPowerSlider.Value;
            int startPower = (int)startPowerSlider.Value;
            int mixMode = mixerModeBox.SelectedIndex;         
            string data = "t1:" + finTemp + ";" + closeTemp + ";" + lowPower + ";" + 
                workPower + ";" + 0 + ";" + 0 + ";" + startPower + ";" + mixMode + ";" + 0 + ";" + 0 + ";" + 0 + ";";
            con.SendData(data);
            Frame.GoBack();
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
