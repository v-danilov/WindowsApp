﻿using System;
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
using WindowsApp.Templates;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsApp.LaunchProcessForms
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Manual : Page
    {
        public Connection con;
        public string inputMessage;

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
            inputMessage = parameters.inputMessage;
            updateData(inputMessage);

            

        }

        private void updateData(string mes)
        {

            string[] data = mes.Split(';');
            powerBlock.Text = data[4];

            // tankBlock.Text = data[]; index 8??
            // columnBlock.Text = data[]; index 11??
          

            pressureBlock.Text = (Convert.ToDouble(data[16]) / 100).ToString();

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


        private void heatingButton_Click(object sender, RoutedEventArgs e)
        {          
            con.SendData("setMKey:1;");
            string response = System.Text.Encoding.UTF8.GetString(con.ReadBytes());
            updateData(response);
        }

        private void pauseButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:1;");
            string response = System.Text.Encoding.UTF8.GetString(con.ReadBytes());
            var parameters = new PauseTemplate();
            parameters.con = con;
            Frame.Navigate(typeof(PauseTemplate));
        }

        private void mixerButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:3;");
            string response = System.Text.Encoding.UTF8.GetString(con.ReadBytes());
            updateData(response);
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

        private void notifyButton_Click(object sender, RoutedEventArgs e)
        {
            //Frame.Navigate(typeof(NotifyTemplate));
        }

        private void pumpButton_Click(object sender, RoutedEventArgs e)
        {                
            con.SendData("setMKey:3;");
            string response = System.Text.Encoding.UTF8.GetString(con.ReadBytes());
            updateData(response);

        }
    }
}
