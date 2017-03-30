using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Sockets;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WindowsApp.LaunchProcessForms;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace WindowsApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RecipesPage : Page
    {
        public Connection con;
        public string inputMessage;
        
        public RecipesPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = (RecipesPage)e.Parameter;

            con = parameters.con;
            inputMessage = parameters.inputMessage;

            //con.SendData("refresh;");
            //string response = System.Text.Encoding.UTF8.GetString(con.ReadBytes());
            //Debug.WriteLine(inputMessage);
            string[] list_elements = inputMessage.Split(';');
            int size = list_elements.Length;
            for(int i = 4; i < size - 1; i++)
            {
                recipesList.Items.Add(list_elements[i]);
            }
            
            
        }

        private void launchButton_Click(object sender, RoutedEventArgs e)
        {

            int selectedRecipe = recipesList.SelectedIndex;
            char c = Convert.ToChar(selectedRecipe);
            string tmp = "rec:" + c + ";";
            char[] mes = tmp.ToCharArray();
            con.SendChar(mes);
            string response = System.Text.Encoding.UTF8.GetString(con.ReadBytes());
            //con.SendData("rec:" + selectedRecipe + ";");

            int screen_id = selectedRecipe;
            switch (screen_id)
            {
                case 0:
                    var manual_parameters = new Manual();
                    manual_parameters.con = con;
                    manual_parameters.inputMessage = response;
                    Frame.Navigate(typeof(Manual), manual_parameters);
                    break;
                case 1:                  
                    var distillation_parameters = new Distillation();
                    distillation_parameters.con = con;
                    distillation_parameters.inputMessage = response;
                    Frame.Navigate(typeof(Distillation), distillation_parameters);
                    break;
                case 2:
                    var rectification_parameters = new Rectification();
                    rectification_parameters.con = con;
                    rectification_parameters.inputMessage = response;
                    Frame.Navigate(typeof(Distillation), rectification_parameters);
                    break;
                case 3:                 
                case 4:          
                case 5:
                case 6:          
                case 8:           
                case 9:                   
                case 10:            
                case 11:       
                case 12:
                    var beerwort_parameters = new BeerWort();
                    beerwort_parameters.con = con;
                    beerwort_parameters.inputMessage = response;
                    Frame.Navigate(typeof(BeerWort), beerwort_parameters);
                    break;
                case 13:
                    var fermentation_parameters = new Fermentation();
                    fermentation_parameters.con = con;
                    fermentation_parameters.inputMessage = response;
                    Frame.Navigate(typeof(Fermentation), fermentation_parameters);
                    break;
                case 14:
                    var boiling_parameters = new Boiling();
                    boiling_parameters.con = con;
                    boiling_parameters.inputMessage = response;
                    Frame.Navigate(typeof(Boiling), boiling_parameters);
                    break;

            }

        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private void settingsButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedRecipe = recipesList.SelectedIndex;
            char c = Convert.ToChar(selectedRecipe);
            string tmp = "chg:" + c + ";";
            char[] mes = tmp.ToCharArray();
            con.SendChar(mes);
            con.SendData("chg:" + selectedRecipe + ";");
            string response = System.Text.Encoding.UTF8.GetString(con.ReadBytes());

            var settingsTemplateParameters = new SettingsTemplate();
            settingsTemplateParameters.con = con;
            settingsTemplateParameters.inputMessage = response;

            Frame.Navigate(typeof(SettingsTemplate), settingsTemplateParameters);
        }

        async private void recipesList_Holding(object sender, HoldingRoutedEventArgs e)
        {
           
                int selectedRecipe = recipesList.SelectedIndex;

                //Не сработает
                string name = recipesList.SelectedItem.ToString();

                RenameDialog dialog = new RenameDialog(selectedRecipe, name);
                var res = await dialog.ShowAsync();

                if (res == ContentDialogResult.Primary)
                {
                string new_name = dialog.message;
                    con.SendData("r(" + selectedRecipe + ")cn(" + new_name + ");");
                }
            
            
        }
    }
}
