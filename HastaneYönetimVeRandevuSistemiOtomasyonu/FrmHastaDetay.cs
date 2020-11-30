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

    public partial class FrmHastaDetay : Form
    {
        public FrmHastaDetay()
        {
            InitializeComponent();
        }

        public string TC;
        sqlConnection connection = new sqlConnection();


        private void FrmHastaDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = TC;

            //Hastanın AD-Soyad Bilgisini Veritabanından Çekme
            SqlCommand komut = new SqlCommand("Select HastaAd,HastaSoyad From PatientTable Where HastaTC=@p1", connection.connect());
            komut.Parameters.AddWithValue("@p1", TC);

            SqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] + " " + dr[1];
            }

            connection.connect().Close();

            //Giriş yapan hastaya ait Randevu Listesini oluştur

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From AppointmentTable Where HastaTC=" + TC, connection.connect());
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            //Mevcut olan Tüm Bransları Gösterme

            SqlCommand komut2 = new SqlCommand("Select BransAd From BranchTable", connection.connect());
            SqlDataReader dr2 = komut2.ExecuteReader();

            while (dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0].ToString());
            }

            connection.connect().Close();
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Branş Seçildiği An Bu Branştaki Doktorların CmbDoktor Listesine Ekle

            CmbDoktor.Items.Clear();

            SqlCommand komut3 = new SqlCommand("Select DoktorAd,DoktorSoyad From DoctorTable Where DoktorBrans=@p1", connection.connect());
            komut3.Parameters.AddWithValue("@p1", CmbBrans.Text);

            SqlDataReader dr3 = komut3.ExecuteReader();

            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0] + " " + dr3[1]);
            }

            connection.connect().Close();
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Doktor ve Brans Seçildikten Sonra -> Bu şartlara uygun olan
            // Mevcut Aktif Randevular -> Aktif Randevular DataGridView içinde listelenir.

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From AppointmentTable Where RandevuBrans='"+CmbBrans.Text+"' AND RandevuDoktor='"+CmbDoktor.Text+"' AND RandevuDurum=0", connection.connect());

            da.Fill(dt);
            dataGridView2.DataSource = dt;
           
        }

    
        private void LnkBilgiDuzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmHastaBilgDuzenle frmHastaBilgDuzenle = new FrmHastaBilgDuzenle();
            frmHastaBilgDuzenle.hastaTc = TC;
            frmHastaBilgDuzenle.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dataGridView2.SelectedCells[0].RowIndex;

            TxtId.Text = dataGridView2.Rows[row].Cells[0].Value.ToString();
            /*
            CmbBrans.Text = dataGridView2.Rows[row].Cells[3].Value.ToString();
            CmbDoktor.Text = dataGridView2.Rows[row].Cells[4].Value.ToString();
            */

        }

        private void BtnRandevuAl_Click(object sender, EventArgs e)
        {
            SqlCommand randevuAl = new SqlCommand("Update AppointmentTable Set RandevuDurum=1,HastaTC=@p1,HastaSikayet=@p2 Where RandevuId=@p3", connection.connect());
            randevuAl.Parameters.AddWithValue("@p1", LblTC.Text);
            randevuAl.Parameters.AddWithValue("@p2", RchSikayet.Text);
            randevuAl.Parameters.AddWithValue("@p3", TxtId.Text);

            randevuAl.ExecuteNonQuery();
            connection.connect().Close();

            MessageBox.Show("Randevu Oluşturuldu!","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
