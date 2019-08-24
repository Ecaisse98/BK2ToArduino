using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View
{
    public interface IViewController
    {
        void ConnectToCommPort(String commPort);
        void SendDataToComm(String filePath);
        string[] GetCommPortList();
    }
}
