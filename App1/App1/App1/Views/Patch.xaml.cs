using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Forms9Patch;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Patch : ContentPage
    {
        public Patch()
        {
            InitializeComponent();
        }

        string fileName = "";

        protected override void OnAppearing()
        {
            base.OnAppearing();
            fileName = Regex.Replace(Guid.NewGuid().ToString(), "[^0-9a-zA-z]", "").Substring(0, 10);
            webView.Source = "https://github.com/";
        }

        private  async void btnPDF_Clicked(object sender, EventArgs e)
        {
            if (Forms9Patch.ToPdfService.IsAvailable)
            {
                if (await webView.ToPdfAsync(fileName) is ToFileResult result)
                {
                    if (result.IsError)
                        using (Toast.Create("Falha ao tentar exportar", result.Result)) { }
                    else
                    {
                        await Xamarin.Essentials.Share.RequestAsync(new Xamarin.Essentials.ShareFileRequest
                        {
                            Title = "Compartilhe seu arquivo",
                            File = new Xamarin.Essentials.ShareFile(result.Result, "application/pdf"),
                        });
                    }
                }
            }
        }

        private async void btnPNG_Clicked(object sender, EventArgs e)
        {
            if (await webView.ToPngAsync(fileName) is ToFileResult result)
            {
                if (result.IsError)
                    using (Toast.Create("Falha ao tentar exportar", result.Result)) { }
                else
                {
                    await Xamarin.Essentials.Share.RequestAsync(new Xamarin.Essentials.ShareFileRequest
                    {
                        Title = "Compartilhe seu arquivo",
                        File = new Xamarin.Essentials.ShareFile(result.Result, "image/png"),
                    });
                }
            }
        }
    }
}