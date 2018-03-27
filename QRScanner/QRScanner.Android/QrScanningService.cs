using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using QRScanner.Droid;
using Xamarin.Forms;
using ZXing;
using ZXing.Mobile;

[assembly: Dependency(typeof(QrScanningService))]
namespace QRScanner.Droid
{
    class QrScanningService : IQrScanningService
    {
        private string urlSuffix;
        public int RoomID;

        public bool ScanAsync()
        {

            var optionsDefault = new MobileBarcodeScanningOptions();
            var optionsCustom = new MobileBarcodeScanningOptions();
            
            var scanner = new MobileBarcodeScanner()
            {
                TopText = "Scan the QR Code",
                BottomText = "Please Wait",
            };
            optionsCustom.DelayBetweenContinuousScans = 500;
            scanner.ScanContinuously(optionsCustom, HandleScanResult);
            return true;
            
        }

        private async void HandleScanResult(ZXing.Result result)
        {
            string msg;
            string url;
            if (result != null && !string.IsNullOrEmpty(result.Text))  // Success
            {
                url = result.Text;
                url += urlSuffix;
                if (await registerUser(url))
                {
                    (MainActivity.getInstance()).RunOnUiThread(() => { Toast.MakeText(Android.App.Application.Context, "Registration Successful!", ToastLength.Short).Show(); });
                   
                }
                else
                {
                    (MainActivity.getInstance()).RunOnUiThread(() => { Toast.MakeText(Android.App.Application.Context, $"Oops, Something went wrong, could not register with {url}", ToastLength.Short).Show(); });
                }
            }
        }
        private async Task<bool> registerUser(string url)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var result = await client.GetAsync(url);
                    if (result.IsSuccessStatusCode)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public void setRoomId(int id)
        {
            RoomID = id;
            urlSuffix = $"&roomId={RoomID.ToString()}&token=c116d090-663a-4a43-8c1b-d0a317beccfb";
        }
    }
}