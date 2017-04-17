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
using WindowsApp.ProcessPropertiesVeiw;

// Шаблон элемента пустой страницы задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsApp
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class SettingsTemplate : Page
    {
        public Connection con;
        public string inputMessage;

        public SettingsTemplate()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (SettingsTemplate)e.Parameter;

            con = parameters.con;
            inputMessage = parameters.inputMessage;

            Debug.WriteLine(inputMessage);
            string[] list_elements = inputMessage.Split(';');
            int size = list_elements.Length;
            for (int i = 4; i < size - 1; i++)
            {
                recipesList.Items.Add(list_elements[i]);
            }


        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

            Frame.GoBack();
        }     

        private async void recipesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int selected_item = -1;
            selected_item = recipesList.SelectedIndex;


            if (selected_item != -1)
            {
                char c = Convert.ToChar(selected_item);
                string tmp = "step:" + c + ";";
                char[] mes = tmp.ToCharArray();
                con.SendChar(mes);
                con.SendData("step:" + selected_item + ";");
                string response = System.Text.Encoding.UTF8.GetString(con.ReadBytes());

                string selected = recipesList.SelectedItem as string;
                Debug.WriteLine(selected);
                switch (selected)
                {
                    case "Ручной режим":
                        var dialog = new MessageDialog("Данный процесс не содержит изменяемых параметров");                      
                        dialog.Commands.Add(new UICommand { Label = "Продолжить", Id = 0 });
                        var res = await dialog.ShowAsync();
                        break;
                    case "Дистилляция":
                        var distillation_parameters = new Distilation();
                        distillation_parameters.con = con;
                        distillation_parameters.inputMessage = response;
                        Frame.Navigate(typeof(Distilation), distillation_parameters);
                        break;                  
                }
            }
        }
    }
}
