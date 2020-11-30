using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HastaneYönetimVeRandevuSistemiOtomasyonu
{
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }

        sqlConnection connection = new sqlConnection();

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            //Giris Onay
            SqlCommand komut = new SqlCommand("Select * From SecretaryTable Where SekTC=@p1 AND SekSifre=@p2", connection.connect());
            komut.Parameters.AddWithValue("@p1", MskTcNo.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);

            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                FrmSekreterDetay frmSekreterDetay = new FrmSekreterDetay();
                frmSekreterDetay.sekTC = MskTcNo.Text;
                frmSekreterDetay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Giris!","Hata",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }

          
        }
    }
}
