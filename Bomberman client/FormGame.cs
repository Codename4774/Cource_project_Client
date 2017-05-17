using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bomberman_client
{
    public partial class FormGame : Form
    {
        GameClasses.GameCore gameCore;

        public FormGame()
        {
            InitializeComponent();
        }

        private void FormGame_Load(object sender, EventArgs e)
        {
            Graphics graphicsControl = MainField.CreateGraphics();
            gameCore = new GameClasses.GameCore
                (
                    MainField.Width, MainField.Height, graphicsControl, "azaza",
                    new Size(24, 32), new Size(24, 24),  new Size(24, 24), new Size(24, 24), (Environment.CurrentDirectory + "\\Resources\\")
                );
            this.KeyUp += gameCore.KeyUpEvent;
            this.KeyPress += gameCore.KeyPressEvent;
            gameCore.startCore();
        }
    }
}
