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
using Bomberman_server;

namespace Bomberman_client
{
    public class Client
    {
        public readonly IPAddress ipAdress;
        public readonly int portControl;
        private int bufferSize;
        public readonly IPEndPoint ipEndPointControl;
        public readonly Socket socketControl;
        private byte[] receivedData;
        public GameClasses.GameCoreClient gameCore;
        private BinaryFormatter serializer;
        public readonly int id;
        private bool isEnded = false;

        public Client(string host, int portControl, string playerName)
        {
            //this.ipHost = Dns.GetHostEntry(host);

            this.portControl = portControl;
            this.ipAdress = IPAddress.Parse(host);
            this.bufferSize = 1024 * 1024;

            this.ipEndPointControl = new IPEndPoint(ipAdress, portControl);

            this.socketControl = new Socket(ipAdress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.serializer = new BinaryFormatter();
            receivedData = new byte[bufferSize];

            try
            {
                socketControl.Connect(ipEndPointControl);
                MemoryStream playerNameBuffer = new MemoryStream();
                serializer.Serialize(playerNameBuffer, playerName);
                socketControl.Send(playerNameBuffer.ToArray());
                int countReceivedData = socketControl.Receive(receivedData);
                id = BitConverter.ToInt32(receivedData, 0);
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        private void GetBufferFromServer(object sender, SocketAsyncEventArgs e)
        {

            MemoryStream stream = new MemoryStream(e.Buffer, 0, e.BytesTransferred);
            if (stream.Capacity != 0)
            {
                try
                {
                    ObjectsLists buffer = (ObjectsLists)serializer.Deserialize(stream);
                    lock (gameCore.bufferObjectsList)
                    {
                        gameCore.bufferObjectsList = buffer;
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.ToString(), "Bomberman", MessageBoxButtons.OK);
                }
                finally
                {
                    if (!isEnded)
                    {
                        SocketAsyncEventArgs eventArgs = new SocketAsyncEventArgs();
                        eventArgs.Completed += GetBufferFromServer;
                        byte[] temp = new byte[1024 * 1024];
                        eventArgs.SetBuffer(temp, 0, 1024 * 1024);

                        (sender as Socket).ReceiveAsync(eventArgs);
                    }
                }
            }
        }

        public void SendMessageToServer(params int[] data)
        {
            List<byte> message = new List<byte>();
            for (int i = 0; i < data.Length; i++ )
            {
                message.AddRange(BitConverter.GetBytes(data[i]));
            }
            if (!isEnded)
            {
                SocketAsyncEventArgs sendArgs = new SocketAsyncEventArgs();
                sendArgs.SetBuffer(message.ToArray(), 0, message.Count);

                socketControl.SendAsync(sendArgs);
            }
        }

        public void Dispose()
        {
            SendMessageToServer(id, (int)KindMessages.KindMessage.Player, (int)KindMessages.KindPlayerMessages.Disconnect);
            socketControl.Shutdown(SocketShutdown.Both);
            socketControl.Disconnect(false);
            socketControl.Close();
            isEnded = true;
        }
        public void StartRecieving(object state)
        {
            SocketAsyncEventArgs eventArgs = new SocketAsyncEventArgs();
            eventArgs.Completed += GetBufferFromServer;
            byte[] temp = new byte[1024 * 1024];
            eventArgs.SetBuffer(temp, 0, 1024 * 1024);
            socketControl.ReceiveAsync(eventArgs);
        }
    }
}
