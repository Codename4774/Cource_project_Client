using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Bomberman_client
{
    public class Client
    {
        public readonly IPHostEntry ipHost;
        public readonly IPAddress ipAdress;
        public readonly int portControl;
        public readonly int portData;
        private int bufferSize;
        public readonly IPEndPoint ipEndPointControl;
        public  IPEndPoint ipEndPointData;
        public readonly Socket socketControl;
        public readonly UdpClient socketData;
        private byte[] receivedData;
        public GameClasses.GameCore gameCore;
        private BinaryFormatter serializer;
        public readonly int id;


        public Client(string host, int portControl, int portData)
        {
            this.ipHost = Dns.GetHostEntry(host);

            this.portControl = portControl;
            this.ipAdress = ipHost.AddressList[0];
            this.bufferSize = 1024 * 1024;

            this.ipEndPointControl = new IPEndPoint(ipAdress, portControl);
            this.ipEndPointData = new IPEndPoint(IPAddress.Any, portData);

            this.socketControl = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            //this.socketData = new UdpClient(portData);
            this.serializer = new BinaryFormatter();
            receivedData = new byte[bufferSize];

            try
            {
                socketControl.Connect(ipEndPointControl);
                int countReceivedData = socketControl.Receive(receivedData);
                id = BitConverter.ToInt32(receivedData, 0);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private void GetBufferFromServer(byte[] data)
        {
            MemoryStream stream = new MemoryStream(data);
            Bitmap buffer = (Bitmap)serializer.Deserialize(stream);
            lock (gameCore.currBuffer)
            {
                gameCore.currBuffer.Graphics.DrawImage(buffer, new Point(0, 0));
            }
        }

        public void SendMessageToServer(params int[] data)
        {
            List<byte> message = new List<byte>();
            for (int i = 0; i < data.Length; i++ )
            {
                message.AddRange(BitConverter.GetBytes(data[i]));
            }
            SocketAsyncEventArgs sendArgs = new SocketAsyncEventArgs();
            sendArgs.SetBuffer(message.ToArray(), 0, message.Count);
            socketControl.SendAsync(sendArgs);
        }

        public void StartRecieving(object state)
        {
            while (true)
            {
                socketControl.Receive(receivedData);

                GetBufferFromServer(receivedData);
            }
        }
    }
}
