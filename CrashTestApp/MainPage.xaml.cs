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
            Debug.WriteLine(listBox.SelectedIndex);
            listBox.SelectedIndex = -1;
        }

        private void slider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            string tmp = "screen:1;NameРектификация;Mode3;Duration0;Power100;Heat1;ShowTemp5;Temp0;-6796;7679;3338;1000;TargTemp1000;";
            Debug.WriteLine(tmp.IndexOf("ShowTemp"));
            
        }
    }
}
