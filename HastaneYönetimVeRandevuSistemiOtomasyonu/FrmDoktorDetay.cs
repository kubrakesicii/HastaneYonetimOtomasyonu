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
    public partial class FrmDoktorDetay : Form
    {

        sqlConnection connection = new sqlConnection();

        public FrmDoktorDetay()
        {
            InitializeComponent();
        }

     
        public string doktorTc;

        private void FrmDoktorDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = doktorTc;

            //Doktor Ad Soayd Getirme
            SqlCommand komut = new SqlCommand("Select (DoktorAd+' '+DoktorSoyad) From DoctorTable Where DoktorTC=@p1", connection.connect());
            komut.Parameters.AddWithValue("@p1", LblTC.Text);
            SqlDataReader dr = komut.ExecuteReader();

            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0].ToString();
            }

            connection.connect().Close();

            //Doktora ait Mevcut Randevu Listesini Getirme

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From AppointmentTable Where RandevuDoktor='" + LblAdSoyad.Text+"'", connection.connect());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void BtnBilgiDuzenle_Click(object sender, EventArgs e)
        {
            FrmDoktorBilgiDuzenle frmDoktorBilgiDuzenle = new FrmDoktorBilgiDuzenle();
            frmDoktorBilgiDuzenle.doktorTC = LblTC.Text;
            frmDoktorBilgiDuzenle.Show();

        }

        private void BtnDuyurular_Click(object sender, EventArgs e)
        {
            FrmDuyurular frmDuyurular = new FrmDuyurular();
            frmDuyurular.Show();
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dataGridView1.SelectedCells[0].RowIndex;

            RchSikayet.Text = dataGridView1.Rows[row].Cells[7].Value.ToString();
        }
    }
}

