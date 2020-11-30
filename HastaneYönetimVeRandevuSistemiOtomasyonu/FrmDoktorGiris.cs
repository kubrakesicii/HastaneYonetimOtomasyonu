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
    public partial class FrmDoktorGiris : Form
    {
        sqlConnection connection = new sqlConnection();

        public FrmDoktorGiris()
        {
            InitializeComponent();
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * From DoctorTable Where DoktorTC=@p1 AND DoktorSifre=@p2", connection.connect());
            komut.Parameters.AddWithValue("@p1", MskTcNo.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();

            if (dr.Read())
            {
                FrmDoktorDetay frmDoktorDetay = new FrmDoktorDetay();
                frmDoktorDetay.doktorTc = MskTcNo.Text;
                frmDoktorDetay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            connection.connect().Close();

        }
    }
}
