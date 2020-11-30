using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneYönetimVeRandevuSistemiOtomasyonu
{
    public partial class FormGirisler : Form
    {
        public FormGirisler()
        {
            InitializeComponent();
        }

        private void BtnHastaGiris_Click(object sender, EventArgs e)
        {
            FrmHastaGiris frmHstaGiris = new FrmHastaGiris();
            frmHstaGiris.Show();
            this.Hide();
        }

        private void BtnDoktorGiris_Click(object sender, EventArgs e)
        {
            FrmDoktorGiris frmDoktorGiris = new FrmDoktorGiris();
            frmDoktorGiris.Show();
            this.Hide();
        }

        private void BtnSekreterGiris_Click(object sender, EventArgs e)
        {
            FrmSekreterGiris frmSekreterGiris = new FrmSekreterGiris();
            frmSekreterGiris.Show();
            this.Hide();
        }
    }
}
