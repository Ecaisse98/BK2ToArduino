using System;
using View;
using Model;
using System.Threading;
using System.IO.Ports;
using System.Windows.Forms;

namespace Controller
{
    class MainController : IViewController
    {
        SerialPort SerialPort { get; set; }
        SerialWriter SerialWriter { get; set; }
        DataReader DataReader { get; set; }
        MainUI MainUI { get; set; }
        public MainController()
        {
            MainUI = new MainUI(this);
        }
        [STAThread]
        static void Main()
        {    
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainController controller = new MainController();
            Application.Run(controller.MainUI);
        }

        public void ConnectToCommPort(string commPort)
        { 
            SerialPort = new SerialPort(commPort);

            SerialPort.BaudRate = 345600;
            SerialPort.Parity = Parity.None;
            SerialPort.StopBits = StopBits.One;
            SerialPort.DataBits = 8;
            SerialPort.Handshake = Handshake.None;

            SerialPort.Open();
        }

        public string[] GetCommPortList()
        {
            return SerialPort.GetPortNames();
        }

        public void SendDataToComm(string filePath)
        {

            DataReader = new DataReader();
            short[] data = DataReader.ReadDataFromFile(filePath);
            SerialWriter = new SerialWriter(SerialPort);
            Thread thread = new Thread(new ThreadStart(()=> SerialWriter.sendData(data)));
            thread.Start();
            
        }
    }
}
