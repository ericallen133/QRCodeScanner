using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QRScanner
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoomSelector : ContentPage
	{

		public RoomSelector ()
		{
			InitializeComponent ();    
		}


        public async void OnSubmitClicked(object sender, EventArgs e)
        {
            try
            {
                int room = roomPicker.SelectedIndex;
                if (room < 0)
                {
                    DependencyService.Get<iToaster>().DisplayToast("Please select a room");
                }
                else
                {
                    var scanner = DependencyService.Get<IQrScanningService>();
                    scanner.setRoomId(room);

                    var result = scanner.ScanAsync();
                }

            }
            catch(Exception ex)
            {
                DependencyService.Get<iToaster>().DisplayToast("Oops "+ ex.Message);
                Console.WriteLine(ex);
            }
        }
	}
}