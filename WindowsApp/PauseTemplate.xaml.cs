using System;
using System.Collections.Generic;
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

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsApp
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class PauseTemplate : Page
    {
        public Connection con;

        private Stopwatch stopwatch;
        private DispatcherTimer timer;

        public PauseTemplate()
        {
            this.InitializeComponent();


        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            stopwatch = new Stopwatch();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
            stopwatch.Start();

            var parameters = (PauseTemplate)e.Parameter;

            //Null?
            con = parameters.con;

        }

        private void Timer_Tick(object sender, object e)
        {

            timerBlock.Text = String.Format("{0:00}:{1:00}", stopwatch.Elapsed.Minutes, stopwatch.Elapsed.Seconds);

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:1;");
            Frame.GoBack();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:2;");
            var First_Frame = Frame.BackStack.First();
            Frame.BackStack.Clear();
            Frame.BackStack.Add(First_Frame);
            Frame.Navigate(typeof(RecipesPage));
        }

        private void SkipButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:3;");
        }
    }
}
