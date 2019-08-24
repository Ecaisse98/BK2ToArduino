using System;
using System.IO.Ports;
using System.Threading;


namespace Model
{
    class SerialWriter
    {
        // Time to wait between each write cycle
        public const int FRAME = (1 / 30) * 1000;

        public SerialPort SerialPort { get; set; }
        public SerialWriter(SerialPort sp){
            this.SerialPort = sp;
        }
        
        /**
         * Send one 16 bits of data every frame
        */
        public void sendData(short[] data) { 
        
            for (int i = 0; i < data.Length; i++)
            {
                byte[] temp = BitConverter.GetBytes(data[i]);
                SerialPort.Write(temp, 0, 2);
                Thread.Sleep(FRAME);
            }
            SerialPort.Close();
        }
    }
}
