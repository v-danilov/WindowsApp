using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CrashTestApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        private Stopwatch stopwatch;
        private DispatcherTimer timer;
        private bool c = false;

        public MainPage()
        {
            this.InitializeComponent();
            listBox.Items.Add("Fourth");
            slider.Value = 20;
            slider.StepFrequency = 4;
            
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            Debug.WriteLine(slider.Value);
            string g = "Hello";
            g = g.Substring(1);
            Debug.WriteLine(g);
        }
    }
}
