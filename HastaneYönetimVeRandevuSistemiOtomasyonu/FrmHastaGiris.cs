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
    public partial class FrmHastaGiris : Form
    {

        sqlConnection connection = new sqlConnection();

        public FrmHastaGiris()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LnkUyeOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaKayit frmHastaKayit = new FrmHastaKayit();
            frmHastaKayit.Show();
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("Select * From PatientTable Where HastaTC=@p1 AND HastaSifre=@p2", connection.connect());
            command.Parameters.AddWithValue("@p1", MskTcNo.Text);
            command.Parameters.AddWithValue("@p2", TxtSifre.Text);

            SqlDataReader dr = command.ExecuteReader();

            if (dr.Read())
            {
                FrmHastaDetay frmHastaDetay = new FrmHastaDetay();
                frmHastaDetay.TC = MskTcNo.Text;
                frmHastaDetay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı veya Sifre");
            }

            connection.connect().Close();
        }
    }
}
