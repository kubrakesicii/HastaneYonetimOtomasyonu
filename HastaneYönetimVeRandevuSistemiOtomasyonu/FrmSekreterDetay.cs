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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        
        public string sekTC;
        sqlConnection connection = new sqlConnection();

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = sekTC;

            //Sekreter AdSoyad Çekme

            SqlCommand komut = new SqlCommand("Select SekAdSoyad From SecretaryTable Where SekTC=@p1", connection.connect());
            komut.Parameters.AddWithValue("@p1", LblTC.Text);

            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0].ToString();
            }

            connection.connect().Close();


            //Brans Tablosunu Çekme (DataGridView'a aktarma)

            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("Select * From BranchTable", connection.connect());
            da1.Fill(dt1);
            dataGridView1.DataSource = dt1;

            //Doktor Tablosunu Çekme  (DataGridView'a aktarma)

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAd + ' ' + DoktorSoyad) as 'Doktorlar',DoktorBrans From DoctorTable", connection.connect());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            // Bransları Randevu Combobox'ında listeleme

            SqlCommand komut2 = new SqlCommand("Select BransAd From BranchTable", connection.connect());
            SqlDataReader dr2 = komut2.ExecuteReader();

            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }

            connection.connect().Close();

            


        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand randOlustur = new SqlCommand("Insert into AppointmentTable (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor,RandevuDurum) values (@r1,@r2,@r3,@r4,'false')", connection.connect());
            randOlustur.Parameters.AddWithValue("@r1", MskTarih.Text);
            randOlustur.Parameters.AddWithValue("@r2", MskSaat.Text);
            randOlustur.Parameters.AddWithValue("@r3", CmbBrans.Text);
            randOlustur.Parameters.AddWithValue("@r4", CmbDoktor.Text);

            randOlustur.ExecuteNonQuery();
            connection.connect().Close();

            MessageBox.Show("Randevu Oluşturuldu!");

            /*
            MskTarih.Text = "";
            MskSaat.Text = "";
            CmbBrans.Text = "";
            CmbDoktor.Text = "";
            ChkDurum.Checked = false;
            */

        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Seçilen Bransta bulunan doktorları CmbBoxa Listeleme

            CmbDoktor.Items.Clear();

            SqlCommand komut3 = new SqlCommand("Select (DoktorAd+' '+DoktorSoyad) From DoctorTable Where DoktorBrans=@p1", connection.connect());
            komut3.Parameters.AddWithValue("@p1", CmbBrans.Text);
            SqlDataReader dr3 = komut3.ExecuteReader();

            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0]);
            }

            connection.connect().Close();

        }

        private void ChkDurum_CheckedChanged(object sender, EventArgs e)
        {
            if(ChkDurum.Checked == true)
            {
                label9.Text = "True";
            }
            else
            {
                label9.Text = "False";
            }
        }

        private void BtnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut5 = new SqlCommand("Insert into AnnouncementTable (Duyuru) values (@d1)", connection.connect());
            komut5.Parameters.AddWithValue("@d1", RchDuyuru.Text);
            komut5.ExecuteNonQuery();

            connection.connect().Close();
            MessageBox.Show("Duyuru Oluşturuldu!");

            RchDuyuru.Text = "";
        }

        private void BtnDoktorPaneli_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli frmDoktorPaneli = new FrmDoktorPaneli();
            frmDoktorPaneli.Show();
        }

        private void BtnBransPaneli_Click(object sender, EventArgs e)
        {
            FrmBransPaneli frmBransPaneli = new FrmBransPaneli();
            frmBransPaneli.Show();
        }

        private void BtnRandevuListe_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frmRandevuListesi = new FrmRandevuListesi();
            frmRandevuListesi.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular frmDuyurular = new FrmDuyurular();
            frmDuyurular.Show();
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
