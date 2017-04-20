using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
        public string inputMessage;

        private Stopwatch stopwatch;
        private DispatcherTimer timer;

        public PauseTemplate()
        {
            this.InitializeComponent();


        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (PauseTemplate)e.Parameter;
            con = parameters.con;
            inputMessage = parameters.inputMessage;

            stopwatch = new Stopwatch();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
           
            
            stopwatch.Start();         

        }

        private void Timer_Tick(object sender, object e)
        {

            string[] tmp = inputMessage.Split(';');
            double elapsed_time = Convert.ToDouble(tmp[2].Substring(4, tmp[2].Length - 4));
            TimeSpan ts = TimeSpan.FromSeconds(stopwatch.Elapsed.Seconds + elapsed_time);
            
            timerBlock.Text = String.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:1;");
            Frame.GoBack();
        }

        private async void StopButton_Click(object sender, RoutedEventArgs e)
        {
            
            con.SendData("setKey:2;");
            

            Debug.WriteLine(con.ReadData());
            con.ReadData(); //del
            var dialog = new MessageDialog("Завершить рецепт?");
            dialog.Title = "Завершение процесса";
            dialog.Commands.Add(new UICommand { Label = "Нет", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "Да", Id = 1 });
            var res = await dialog.ShowAsync();
            if ((int)res.Id == 1)
            {
                con.SendData("setKey:1;");
                con.ReadData();
                dialog = new MessageDialog("Рецепт завершён");
                dialog.Title = "Завершение процесса";
                dialog.Commands.Add(new UICommand { Label = "Продолжить", Id = 1 });
                res = await dialog.ShowAsync();
                con.SendData("setKey:1;");
                string response = System.Text.Encoding.UTF8.GetString(con.ReadBytes());
                var First_Frame = Frame.BackStack.First();
                Frame.BackStack.Clear();
                Frame.BackStack.Add(First_Frame);
                var parameters = new RecipesPage();
                parameters.con = con;
                parameters.inputMessage = response;
                Frame.Navigate(typeof(RecipesPage), parameters);
            }
            else
            {
                con.SendData("setKey:2;");
                con.ReadData();
                return;
            }

            
        }

        private async void SkipButton_Click(object sender, RoutedEventArgs e)
        {
            con.SendData("setKey:3;");

            Debug.WriteLine(con.ReadData());
            con.ReadData(); //del
            var dialog = new MessageDialog("Пропустить этап?");
            dialog.Title = "Пропуск этапа";
            dialog.Commands.Add(new UICommand { Label = "Нет", Id = 0 });
            dialog.Commands.Add(new UICommand { Label = "Да", Id = 1 });
            var res = await dialog.ShowAsync();
            if ((int)res.Id == 1)
            {
                con.SendData("setKey:1;");
                con.ReadData();
                dialog = new MessageDialog("Рецепт завершён");
                dialog.Title = "Завершение процесса";
                dialog.Commands.Add(new UICommand { Label = "Продолжить", Id = 1 });
                res = await dialog.ShowAsync();
                con.SendData("setKey:1;");
                string response = System.Text.Encoding.UTF8.GetString(con.ReadBytes());
                var First_Frame = Frame.BackStack.First();
                Frame.BackStack.Clear();
                Frame.BackStack.Add(First_Frame);
                var parameters = new RecipesPage();
                parameters.con = con;
                parameters.inputMessage = response;
                Frame.Navigate(typeof(RecipesPage), parameters);
            }
            else
            {
                con.SendData("setKey:2;");
                con.ReadData();
                return;
            }
        }
    }
}
