using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyMemory
{
    public partial class dlgPlayerNameEntry : Form
    {

        public String username1,username2;

        public dlgPlayerNameEntry()
        {
            InitializeComponent();
        }

        private void dlgPlayerNameEntry_Load(object sender, EventArgs e)
        {
            this.txtSpeler1.Text = gameSettings.players[0].name;
            this.txtSpeler2.Text = gameSettings.players[1].name;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            username1 = this.txtSpeler1.Text;
            username2 = this.txtSpeler2.Text;
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            if (this.txtSpeler1.Text != "" && 
                this.txtSpeler1.Text.Length>=3 && 
                this.txtSpeler2.Text != "" && 
                this.txtSpeler2.Text.Length >= 3) this.btnOK.Enabled = true;
        }
    }
}
