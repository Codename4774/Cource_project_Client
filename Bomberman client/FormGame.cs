using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Bomberman_client
{
    public partial class FormGame : Form
    {
        GameClasses.GameCore gameCore;
        public Client client;

        public FormGame(Client client)
        {
            InitializeComponent();
            this.client = client;
            Graphics graphicsControl = MainField.CreateGraphics();
            gameCore = new GameClasses.GameCore
                (
                    MainField.Width, MainField.Height, graphicsControl, client, client.id, (Environment.CurrentDirectory + "\\Resources\\")
                );
            this.KeyUp += gameCore.KeyUpEvent;
            this.KeyPress += gameCore.KeyPressEvent;
            client.gameCore = gameCore;
        }

        private void FormGame_Load(object sender, EventArgs e)
        {

            //ThreadPool.QueueUserWorkItem(client.StartRecieving);
            gameCore.startCore();
        }

        private void FormGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            gameCore.Dispose();
            client.Dispose();
        }
    }
}
