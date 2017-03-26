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


        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
