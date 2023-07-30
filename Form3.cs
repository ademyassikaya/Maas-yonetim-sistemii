using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Veritabanı için eklenen kütüphaneler
using MySql.Data;
using MySql.Data.MySqlClient;

namespace MaasYonetimi
{
    public partial class Form3 : Form
    {
        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataReader dr;
        public Form3()
        {
            InitializeComponent();

            con = new MySqlConnection("Server = localhost; Database = maasyonetimiveritabani; Uid = root; Pwd = '';");
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();
            string sorgu = "İNSERT İNTO calisanlar (isim,soyisim,e_mail,tel_no,departman,sifre,unvan_id) VALUES ('" + textBox1.Text + "', '" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text +
                "','" + textBox5.Text + "','" + textBox6.Text + "','" + textBox7.Text + "')";
            cmd = new MySqlCommand(sorgu, con);

            cmd.ExecuteNonQuery();

            MessageBox.Show("Kullanıcı Eklendi.");
            con.Close();

            foreach (Control item in this.Controls)
            {
                if (item.GetType().ToString() == "System.Windows.Forms.TextBox") item.Text = "";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();
            string sorgu = "SELECT * FROM calisanlar WHERE isim ='" + textBox8.Text + "'";
            cmd = new MySqlCommand(sorgu, con);
            MySqlDataReader oku = cmd.ExecuteReader();


            while (oku.Read())
            {
                
                textBox1.Text = oku["isim"].ToString();
                textBox2.Text = oku["soyisim"].ToString();
                textBox3.Text = oku["e_mail"].ToString();
                textBox4.Text = oku["tel_no"].ToString();
                textBox5.Text = oku["departman"].ToString();
                textBox6.Text = oku["sifre"].ToString();
                textBox7.Text = oku["unvan_id"].ToString();

            }
            
            con.Close();

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();
            string sorgu = "DELETE FROM calisanlar WHERE isim ='" + textBox8.Text + "' ";
            cmd = new MySqlCommand(sorgu, con);
            cmd.ExecuteNonQuery();
            MessageBox.Show("Kullanıcı Silindi.");
            con.Close();

            foreach (Control item in this.Controls)
            {
                if (item.GetType().ToString() == "System.Windows.Forms.TextBox") item.Text = "";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Application.OpenForms["Form1"].Show();
            this.Hide();
        }
    }
}
