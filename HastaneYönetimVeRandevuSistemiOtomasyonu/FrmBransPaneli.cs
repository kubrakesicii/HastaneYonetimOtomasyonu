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
    public partial class FrmBransPaneli : Form
    {
        sqlConnection connection = new sqlConnection();

        public FrmBransPaneli()
        {
            InitializeComponent();
        }

        private void FrmBransPaneli_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From BranchTable", connection.connect());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Insert Into BranchTable (BransAd) values (@p1)", connection.connect());
            komut.Parameters.AddWithValue("@p1", TxtBrans.Text);

            komut.ExecuteNonQuery();

            connection.connect().Close();
            MessageBox.Show("Branş Eklendi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = dataGridView1.SelectedCells[0].RowIndex;

            TxtId.Text = dataGridView1.Rows[row].Cells[0].Value.ToString();
            TxtBrans.Text = dataGridView1.Rows[row].Cells[1].Value.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("Delete From BranchTable Where BransId=@p1", connection.connect());
            komut2.Parameters.AddWithValue("@p1", TxtId.Text);
            komut2.ExecuteNonQuery();

            connection.connect().Close();
            MessageBox.Show("Branş Silindi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut3 = new SqlCommand("Update BranchTable Set BransAd=@p1 Where BransId=@p2", connection.connect());
            komut3.Parameters.AddWithValue("@p1", TxtBrans.Text);
            komut3.Parameters.AddWithValue("@p2", TxtId.Text);
     
            komut3.ExecuteNonQuery();
            connection.connect().Close();
            MessageBox.Show("Brans Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
