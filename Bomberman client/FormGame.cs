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

        public FormGame()
        {
            InitializeComponent();
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            Graphics graphicsControl = MainField.CreateGraphics();
            gameCore = new GameClasses.GameCore
                (
                    MainField.Width, MainField.Height, graphicsControl, "azaz", client, client.id
                );
            this.KeyUp += gameCore.KeyUpEvent;
            this.KeyPress += gameCore.KeyPressEvent;
            client.gameCore = gameCore;
            gameCore.startCore();
            ThreadPool.QueueUserWorkItem(client.StartRecieving);
        }
    }
}
