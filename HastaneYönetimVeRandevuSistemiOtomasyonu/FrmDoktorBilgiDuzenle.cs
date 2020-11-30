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
    public partial class FrmDoktorBilgiDuzenle : Form
    {

        sqlConnection connection = new sqlConnection();

        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }

        public string doktorTC;

        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            MskTC.Text = doktorTC;

            //Doktor Bilgilerini Getirme

            SqlCommand komut = new SqlCommand("Select * From DoctorTable Where DoktorTC=@p1", connection.connect());
            komut.Parameters.AddWithValue("@p1", MskTC.Text);

            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtAd.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                CmbBrans.Text = dr[3].ToString();
                TxtSifre.Text = dr[5].ToString();
            }

            connection.connect().Close();
        }

        private void BtnKayit_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("Update DoctorTable Set DoktorAd=@p1,DoktorSoyad=@p2,DoktorBrans=@p3,DoktorSifre=@p4 Where DoktorTC=@p5", connection.connect());
            komut2.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut2.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3", CmbBrans.Text);
            komut2.Parameters.AddWithValue("@p4", TxtSifre.Text);
            komut2.Parameters.AddWithValue("@p5", MskTC.Text);

            komut2.ExecuteNonQuery();

            connection.connect().Close();
            MessageBox.Show("Doktor Bilgileri Güncellendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Hide();
        }
    }
}
