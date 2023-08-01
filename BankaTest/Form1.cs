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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection bgl = new SqlConnection(@"Data Source=LENOVO\SQLEXPRESS;Initial Catalog=DbBankaTest;Integrated Security=True");
        private void lnkKayitOl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form3 frm3 = new Form3();
            frm3.Show();
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            bgl.Open();
            SqlCommand komut = new SqlCommand("select AD,SOYAD,HESAPNO,TELEFON,TC from TBLKISILER where HESAPNO=@p1 and SIFRE=@p2", bgl);
            komut.Parameters.AddWithValue("@p1", mskhesapno.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Form2 frm2 = new Form2();
                frm2.adsoyad = dr[0] + " " + dr[1];
                frm2.hesapno = dr[2].ToString();
                frm2.telno= dr[3].ToString();
                frm2.tcno = dr[4].ToString();

                frm2.Show();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş");
            }
            bgl.Close();

        }
    }
}
