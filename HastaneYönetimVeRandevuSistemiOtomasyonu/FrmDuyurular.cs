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
    public partial class FrmDuyurular : Form
    {
        sqlConnection connection = new sqlConnection();

        public FrmDuyurular()
        {
            InitializeComponent();
        }

        private void FrmDuyurular_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From AnnouncementTable", connection.connect());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

        }
    }
}
