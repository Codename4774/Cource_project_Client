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
    public partial class FormConnect : Form
    {
        public FormConnect()
        {
            InitializeComponent();
        }

        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            FormGame formGame = new FormGame();
            try
            {
                formGame.client = new Client(TextBoxServerName.Text, 11000, 11001);
                formGame.Show();
                ThreadPool.QueueUserWorkItem(formGame.client.StartRecieving);
            }
            catch (Exception exeption)
            {
                MessageBox.Show("Cannot connect to the server. Please, try again.", "Bomberman", MessageBoxButtons.OK);
            }
        }
    }
}
