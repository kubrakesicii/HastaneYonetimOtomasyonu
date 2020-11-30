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
    public partial class FrmHastaBilgDuzenle : Form
    {
        public FrmHastaBilgDuzenle()
        {
            InitializeComponent();
        }

        public string hastaTc;
        sqlConnection connection = new sqlConnection();

        private void FrmHastaBilgDuzenle_Load(object sender, EventArgs e)
        {

            //Bilgileri Düzenle Tıklandığında -> Giriş yapmış olan hastanın
            //mevcut verileri veritabanından çekilir ve gösterilir.
            MskTC.Text = hastaTc;
            SqlCommand komut = new SqlCommand("Select * From PatientTable Where HastaTC=@p1", connection.connect());
            komut.Parameters.AddWithValue("@p1", MskTC.Text);

            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                TxtAd.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                MskTel.Text = dr[4].ToString();
                TxtSifre.Text = dr[5].ToString();
                CmbCinsiyet.Text = dr[6].ToString();
            }

            connection.connect().Close();
        }

        private void BtnKayit_Click(object sender, EventArgs e)
        {
            //İLgili alanlara guncel veriler girildikten sonra veritbanındaki tablodan
            //bu hastaya ait veriler güncellenir

            SqlCommand komut2 = new SqlCommand("Update PatientTable" +
                " Set HastaAd=@p1,HastaSoyad=@p2,HastaTel=@p3,HastaSifre=@p4,HastaCinsiyet=@p5" +
                " Where HastaTC=@p6", connection.connect());
            komut2.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut2.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3", MskTel.Text);
            komut2.Parameters.AddWithValue("@p4", TxtSifre.Text);
            komut2.Parameters.AddWithValue("@p5", CmbCinsiyet.Text);
            komut2.Parameters.AddWithValue("@p6", MskTC.Text);

            komut2.ExecuteNonQuery();

            MessageBox.Show("Bilgiler Güncellendi!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();

            connection.connect().Close();
        }
    }
}
