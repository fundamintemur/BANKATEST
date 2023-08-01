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

namespace BankaTest
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        public string adsoyad;
        public string hesapno;
        public string telno;
        public string tcno;


        private void Form2_Load(object sender, EventArgs e)
        {

            lblAdSoyad.Text = adsoyad;
            lblhesapno.Text = hesapno;
            lbltelefon.Text = telno;
            lbltcno.Text = tcno;
        }
        SqlConnection bgl = new SqlConnection(@"Data Source=LENOVO\SQLEXPRESS;Initial Catalog=DbBankaTest;Integrated Security=True");
        private void BtnGonder_Click(object sender, EventArgs e)
        {
            //gönderilen hesabın para artışı
            bgl.Open();
            SqlCommand komut = new SqlCommand("update TBLHESAPNO set BAKİYE=BAKİYE+@p1 where HESAPNO=@p2", bgl);
            komut.Parameters.AddWithValue("@p1",decimal.Parse(TxtTutar.Text));
            komut.Parameters.AddWithValue("@p2",maskedTextBox1.Text);
            komut.ExecuteNonQuery();
            bgl.Close();
           // MessageBox.Show("Sisteme Kayıt Edildi");

            //gönderilen hesabın para azalışı
            bgl.Open();
            SqlCommand komut1 = new SqlCommand("update TBLHESAPNO set BAKİYE=BAKİYE-@k1 where HESAPNO=@k2", bgl);
            komut1.Parameters.AddWithValue("@k1", decimal.Parse(TxtTutar.Text));
            komut1.Parameters.AddWithValue("@k2", hesapno);
            komut1.ExecuteNonQuery();
            bgl.Close();
            MessageBox.Show("Sisteme Kayıt Edildi");

            bgl.Open();
            SqlCommand komut2 = new SqlCommand("insert into TBLHAREKET(GONDEREN,ALICI,TUTAR)values(@m1,@m2,@m3)", bgl);
            komut2.Parameters.AddWithValue("@m1", lblhesapno.Text);
            komut2.Parameters.AddWithValue("@m2", maskedTextBox1.Text);
            komut2.Parameters.AddWithValue("@m3", TxtTutar.Text);
            komut2.ExecuteNonQuery();
            bgl.Close();



        }

        private void btnhesaphareket_Click(object sender, EventArgs e)
        {
           // SqlCommand komutlistele = new SqlCommand("select*from TBLHAREKET", bgl);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select*from TBLHAREKET", bgl);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
