using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QRScanner
{
    public interface IQrScanningService
    {
        Task<string> ScanAsync();
    }
}
