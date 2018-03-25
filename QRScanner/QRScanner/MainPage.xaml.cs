using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QRScanner
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        private async void btnScan_Clicked(object sender, EventArgs e)
        {
            string url;
            try
            {
                var scanner = DependencyService.Get<IQrScanningService>();
                var result = await scanner.ScanAsync();
                if (result != null)
                {
                    url = result;
                    website.Source = url;
                }
              
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }
    }
}
