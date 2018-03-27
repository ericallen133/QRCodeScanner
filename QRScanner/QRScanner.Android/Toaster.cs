using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using QRScanner.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(Toaster))]
namespace QRScanner.Droid
{
    class Toaster : iToaster
    {
        public void DisplayToast(string text)
        {
            Toast.MakeText(Application.Context, text, ToastLength.Short).Show();
        }
    }
}