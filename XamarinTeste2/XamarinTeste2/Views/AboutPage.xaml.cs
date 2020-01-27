using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Forms9Patch;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace XamarinTeste2.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        Pedido pedido;

        public AboutPage()
        {
            InitializeComponent();

            Random rnd = new Random();

            pedido = new Pedido()
            {
                DataVenda = DateTime.Now,
                ClienteNome = "José Teste",
                ClienteCPFCNPJ = "123.456.789-00",
                LojaCNPJ = "01.665.968.855-88", //que raios de documento é esse??
                LojaEndereco = "Av Duque de caxias 1541 - CENTRO   Fortaleza-CE",
                LojaRazao = "DJA Distribuidora",
                ID = rnd.Next(1, 999),
                Items = new List<PedidoItem>()
                {
                    new PedidoItem()
                    {
                        ID = 1,
                        EAN = "1234567890123",
                        Desconto = 0,
                        Nome = "Coca Cola Lata 300ml",
                        ValorUn = 3.5m,
                        Quantidade = 1,

                    },
                    new PedidoItem()
                    {
                        ID = 1,
                        EAN = "1234567890123",
                        Desconto = 0,
                        Nome = "Pastel de Frango",
                        ValorUn = 5.5m,
                        Quantidade = 3,

                    },
                }
            };

            webView.Source = new HtmlWebViewSource
            {
                Html = PedidoHTML(pedido)
            };
        }

        /// <summary>
        /// Pra exportar como pdf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ButtonPDF_Clicked(object sender, EventArgs e)
        {
            if (Forms9Patch.ToPdfService.IsAvailable)
            {
                if (await webView.ToPdfAsync("Pedido" + pedido.ID + ".pdf") is ToFileResult result)
                {
                    if (result.IsError)
                        using (Toast.Create("PDF Failure", result.Result)) { }
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
            else
                using (Toast.Create(null, "ToPdfService Export is not available on this device")) { }

        }
        /// <summary>
        /// Pra exportar como PNG/Imagem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (await webView.ToPngAsync("Pedido" + pedido.ID + ".png") is ToFileResult result)
            {
                if (result.IsError)
                    using (Toast.Create("PNG Failure", result.Result)) { }
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

        private string PedidoHTML(Pedido pedido)
        {

            string html = @"
<!DOCTYPE html>
<html style='width: 400px;'>
<head>
</head>
<body style='width: 400px; padding: 6px;'>
<p style='text-align: center;'>" + pedido.LojaRazao + @"</p>
<p>" + pedido.LojaEndereco + @"</p>
<p>Data: " + pedido.DataVenda.ToString("dd/MM/yyyy") + @" 
        CNPJ: " + pedido.LojaCNPJ + @" </p>
<table style='width: 100%;'>
<tr>
    <th>#</th>
    <th>Desc</th>
    <th>Vlr</th>
    <th>Qtd</th>
    <th>Total</th>
</tr>
</table>
<table style='width: 100%;'>
";
            for (int i = 1; i <= pedido.Items.Count; i++)
            {
                var item = pedido.Items[i - 1];
                html += @"
<tr>
    <td colspan='3'>" + i.ToString("000") + @" " + item.Nome + @"</td>
</tr>
<tr style='border-bottom: 1px solid gray;'>
    <td>R$ " + item.ValorUnitario.ToString("#0.00") + @"</td>
    <td>x " + item.Quantidade + @" Un</td>
    <td style='text-align: right;'>R$ " + item.ValorTotal.ToString("#0.00") + @"</td>
</tr>
";
            }

            html +=
            @"
<tr>
    <td colspan='3' style='text-align: right;'>R$ " + pedido.ValorTotal.ToString("#0.00") + @"</td>
</tr>
</table>
<p style='text-align: center;'> OBRIGADO PELA PREFERÊNCIA </p>
</body>
</html>
";

            return html;
        }
    }

    public class Pedido
    {
        public int ID { get; set; }
        public string ClienteNome { get; set; }
        public string ClienteCPFCNPJ { get; set; }

        public string LojaRazao { get; set; }
        public string LojaEndereco { get; set; }
        public string LojaCNPJ { get; set; }

        public DateTime DataVenda { get; set; }
        public decimal ValorTotal
        {
            get
            {
                if (Items != null)
                {
                    return 0;
                }

                return Items.Select(i => i.ValorTotal).Sum();
            }
        }


        public List<PedidoItem> Items { get; set; }
    }

    public class PedidoItem
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public string EAN { get; set; }
        public decimal ValorUn { get; set; }

        /// <summary>
        /// Desconto em %
        /// </summary>
        public decimal Desconto { get; set; }
        public int Quantidade { get; set; }

        /// <summary>
        /// ValorUn - Desconto
        /// </summary>
        public decimal ValorUnitario
        {
            get
            {
                return ValorUn - (ValorUn * Desconto * 100);
            }
        }
        public decimal ValorTotal
        {
            get
            {
                return Quantidade * ValorUnitario;
            }
        }

    }
}