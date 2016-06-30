using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft.Json;
using System.Net.Http;

namespace LojaWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string ip = "http://10.21.0.137";
        

        public MainWindow()
        {
            InitializeComponent();
            SelectFabricantes();
        }

        private async void SelectFabricantes()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);

            var response = await httpClient.GetAsync("/20131011110029/api/fabricante");
            var str = response.Content.ReadAsStringAsync().Result;

            List<Models.Fabricante> obj = JsonConvert.DeserializeObject<List<Models.Fabricante>>(str);
            
            cbox.ItemsSource = obj;
            cbox.DisplayMemberPath = "Descricao";
            cbox.SelectedValuePath = "Id";
        }

        private async void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            Models.Fabricante f = new Models.Fabricante
            {
                Id = int.Parse(txtId.Text),
                Descricao = txtDesc.Text
            };
            List<Models.Fabricante> fl = new List<Models.Fabricante>();
            fl.Add(f);
            string s = "=" + JsonConvert.SerializeObject(fl);
            var content = new StringContent(s, Encoding.UTF8,"application/x-www-form-urlencoded");
            await httpClient.PostAsync("/20131011110029/api/fabricante", content);
        }

        private async void btnList_Click(object sender, RoutedEventArgs e)
        {

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);

            var response = await httpClient.GetAsync("/20131011110029/api/fabricante");
            var str = response.Content.ReadAsStringAsync().Result;

            List<Models.Fabricante> obj = JsonConvert.DeserializeObject<List<Models.Fabricante>>(str);

            ListBoxFabricantes.ItemsSource = obj;
        }

        private async void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            Models.Fabricante f = new Models.Fabricante
            {
                 Id = int.Parse(txtId.Text),

                 Descricao = txtDesc.Text
                            };

                 string s = "=" + JsonConvert.SerializeObject(f);

                 var content = new StringContent(s, Encoding.UTF8,"application/x-www-form-urlencoded");

                 await httpClient.PutAsync("/20131011110029/api/fabricante/" + f.Id,content);
         }

        private async void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();

             httpClient.BaseAddress = new Uri(ip);

             await httpClient.DeleteAsync("/20131011110029/api/fabricante/" +txtId.Text);
 
        }

        private async void buttonInserir_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            Models.Veiculo f = new Models.Veiculo
            {
                Id =1,// int.Parse(txtId.Text),
                Modelo = txtModelo.Text,
                idFabricante = int.Parse(cbox.SelectedValue.ToString())
            };
            List<Models.Veiculo> fl = new List<Models.Veiculo>();
            fl.Add(f);
            string s = "=" + JsonConvert.SerializeObject(fl);
            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");
            await httpClient.PostAsync("/20131011110029/api/veiculo", content);
        }

        private async void buttonUpdate_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);
            Models.Veiculo f = new Models.Veiculo
            {
                Id = int.Parse(txtId.Text),

                Modelo = txtDesc.Text
            };

            string s = "=" + JsonConvert.SerializeObject(f);

            var content = new StringContent(s, Encoding.UTF8, "application/x-www-form-urlencoded");

            await httpClient.PutAsync("/20131011110029/api/veiculo/" + f.Id, content);
        }

        private async void buttonListar_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ip);

            var response = await httpClient.GetAsync("/20131011110029/api/veiculo");
            var str = response.Content.ReadAsStringAsync().Result;

            List<Models.Veiculo> obj = JsonConvert.DeserializeObject<List<Models.Veiculo>>(str);

            ListBoxVeiculos.ItemsSource = obj;
        }

        private async void buttonDelete_Click(object sender, RoutedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();

            httpClient.BaseAddress = new Uri(ip);

            await httpClient.DeleteAsync("/20131011110029/api/veiculo/" + txtId1.Text);
        }
    }
}

