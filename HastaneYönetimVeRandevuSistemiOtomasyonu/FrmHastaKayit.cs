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
    public partial class FrmHastaKayit : Form
    {

        public FrmHastaKayit()
        {
            InitializeComponent();
        }

        private void FrmHastaKayit_Load(object sender, EventArgs e)
        {

        }

        sqlConnection connection = new sqlConnection();

        private void BtnKayit_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("insert into PatientTable (HastaAd,HastaSoyad,HastaTC,HastaTel,HastaSifre,HastaCinsiyet) values (@p1,@p2,@p3,@p4,@p5,@p6)", connection.connect());
            command.Parameters.AddWithValue("@p1", TxtAd.Text);
            command.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            command.Parameters.AddWithValue("@p3", MskTC.Text);
            command.Parameters.AddWithValue("@p4", MskTel.Text);
            command.Parameters.AddWithValue("@p5", TxtSifre.Text);
            command.Parameters.AddWithValue("@p6", CmbCinsiyet.Text);

            command.ExecuteNonQuery();


            connection.connect().Close();  //connection.connect() Bir SqlConnection nesnesi döner

            MessageBox.Show("Kayıt Başarılı! \n Şifreniz : " + TxtSifre.Text, "Kayıt Onay", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Hide();
        }
    }
}
