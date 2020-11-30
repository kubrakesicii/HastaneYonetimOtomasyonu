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
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }

        sqlConnection connection = new sqlConnection();

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {

            //Mevcut Doktorların Listesi DataGridView'a Aktarılır
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From DoctorTable", connection.connect());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Mevcut Tüm Branslar Combobox içine Listelenir

            SqlCommand bransListele = new SqlCommand("Select BransAd From BranchTable", connection.connect());
            SqlDataReader dr = bransListele.ExecuteReader();
            while (dr.Read())
            {
                CmbBrans.Items.Add(dr[0]);
            }
            connection.connect().Close();

        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("Insert into DoctorTable (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTC,DoktorSifre) values (@p1,@p2,@p3,@p4,@p5)", connection.connect());
            komut1.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut1.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut1.Parameters.AddWithValue("@p3", CmbBrans.Text);
            komut1.Parameters.AddWithValue("@p4", MskTC.Text);
            komut1.Parameters.AddWithValue("@p5", TxtSifre.Text);

            komut1.ExecuteNonQuery();
            connection.connect().Close();

            MessageBox.Show("Doktor Eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowNo = dataGridView1.SelectedCells[0].RowIndex;

            TxtAd.Text = dataGridView1.Rows[rowNo].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[rowNo].Cells[2].Value.ToString();
            CmbBrans.Text = dataGridView1.Rows[rowNo].Cells[3].Value.ToString();
            MskTC.Text = dataGridView1.Rows[rowNo].Cells[4].Value.ToString();
            TxtSifre.Text = dataGridView1.Rows[rowNo].Cells[5].Value.ToString();

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("Delete From DoctorTable Where DoktorTC = @p1", connection.connect());
            komut2.Parameters.AddWithValue("@p1", MskTC.Text);
            komut2.ExecuteNonQuery();

            connection.connect().Close();

            MessageBox.Show("Doktor Silme İşlemi Gerçekleşti", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("Update DoctorTable Set DoktorAd=@p1,DoktorSoyad=@p2,DoktorBrans=@p3,DoktorSifre=@p4 Where DoktorTC=@p5", connection.connect());
            komut3.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut3.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut3.Parameters.AddWithValue("@p3", CmbBrans.Text);
            komut3.Parameters.AddWithValue("@p4", TxtSifre.Text);
            komut3.Parameters.AddWithValue("@p5", MskTC.Text);

            komut3.ExecuteNonQuery();
            connection.connect().Close();
            MessageBox.Show("Doktor Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
