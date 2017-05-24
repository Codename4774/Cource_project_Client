using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;

namespace Bomberman_client
{
    public class Client
    {
        public readonly IPHostEntry ipHost;
        public readonly IPAddress ipAdress;
        public readonly int port;
        private int bufferSize;
        public readonly IPEndPoint ipEndPoint;
        public readonly Socket socket;
        private byte[] receivedData;
        private int maxCountMessages;
        private int countReceivedMessages;


        public Client(string host, int port)
        {
            this.ipHost = Dns.GetHostEntry(host);

            this.port = port;
            this.ipAdress = ipHost.AddressList[0];
            this.bufferSize = 2048;
            this.maxCountMessages = 10;

            this.ipEndPoint = new IPEndPoint(ipAdress, port);
            this.socket = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            receivedData = new byte[bufferSize];

            try
            {
                socket.Connect(ipEndPoint);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private void ProcessingData(object state)
        {
            MessageBox.Show(Encoding.UTF8.GetString(receivedData), "Header", MessageBoxButtons.OK);
        }

        public void StartRecieving(object state)
        {
            while (true)
            {
                countReceivedMessages = socket.Receive(receivedData);
                ThreadPool.QueueUserWorkItem(ProcessingData);
            }
        }
    }
}
