using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string Token { get; set; }

        private void Button1_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", "info@codehaks.com"),
                new KeyValuePair<string, string>("password", "123@Abc")
            });


            Task.Run(async () =>
            {
                var result = await client.PostAsync("http://localhost:5000/api/token", content);
                Token = await result.Content.ReadAsStringAsync();
                label1.Text = Token;
            });


        }

        private void Button2_Click(object sender, EventArgs e)
        {
            var url = "http://localhost:5000/api/check";
            var client = new HttpClient();

            Task.Run(async () =>
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);

                var content = new FormUrlEncodedContent(
                    new[]
                    {
                new KeyValuePair<string, string>("", "")
            });

                var result = await client.PostAsync(url,content);
                label2.Text = await result.Content.ReadAsStringAsync();
            });


        }
    }
}
