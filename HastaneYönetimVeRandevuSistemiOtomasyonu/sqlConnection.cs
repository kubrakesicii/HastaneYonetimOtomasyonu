using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HastaneYönetimVeRandevuSistemiOtomasyonu
{
    class sqlConnection
    {

        public SqlConnection connect()
        {
            SqlConnection connection = new SqlConnection("Data Source=DESKTOP-HL207FG\\SQLEXPRESS;Initial Catalog=HospitalAutomation;Integrated Security=True");
            connection.Open();
            return connection;
        }

    }
}
