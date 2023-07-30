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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        MySqlConnection baglanti = new MySqlConnection("Server = localhost; Database = maasyonetimiveritabani; Uid = root; Pwd = '';");
        MySqlCommand komut = new MySqlCommand();


        private void Form2_Load(object sender, EventArgs e)
        {
            MySqlDataReader oku;
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText = "select * from unvanlar";
            oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku[1].ToString());
            }
            baglanti.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int gunlukMesai = 9;
            int haftalikMesai = 45;
            int aylikMesai = 180;
            int cocukBasinaDusenPara = 200;

            int textbox2 = Convert.ToInt32(textBox2.Text);
            int textbox3 = Convert.ToInt32(textBox3.Text);
            int textbox4 = Convert.ToInt32(textBox4.Text);
            int textbox5 = Convert.ToInt32(textBox5.Text);
            int textbox6 = Convert.ToInt32(textBox6.Text);
            int textbox7 = Convert.ToInt32(textBox7.Text);
            int textbox8 = Convert.ToInt32(textBox8.Text);

            int kacSaatCalisildi = textbox2 * aylikMesai;
            int toplamKacSaatCalisildi = textbox3 + kacSaatCalisildi;


            int raporluGunSaati = textbox7 * 9;

            int enSonCalisilanSaat = toplamKacSaatCalisildi - raporluGunSaati;

            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("SELECT saatlik_maas FROM unvanlar WHERE unvan ='" + comboBox1.Text + "'", baglanti);
            MySqlDataReader oku = komut.ExecuteReader();
            oku.Read();
            string saatlik_maas = oku["saatlik_maas"].ToString();
            int unvanSaatlikMaasDegeri = Convert.ToInt32(saatlik_maas);
            int maas = enSonCalisilanSaat * unvanSaatlikMaasDegeri;


            int maasaEtkiEdecekCocukParası = maas + (cocukBasinaDusenPara * Convert.ToInt32(textBox4.Text));


            int maasCocukParasiCikarimi = maasaEtkiEdecekCocukParası - ((maasaEtkiEdecekCocukParası * Convert.ToInt32(textBox5.Text)) / 100);


            int maasinSonHali = maasCocukParasiCikarimi - ((maasCocukParasiCikarimi * Convert.ToInt32(textBox8.Text)) / 100);


            MessageBox.Show("\t\t\t" + "MAAŞ BİLGİLERİ" + "\n\n" + "Maaş: " + maas.ToString() + "\n"+ 
                           "Maaşa Eklenen Çocuk Yardımından Sonra Maaş: " + maasaEtkiEdecekCocukParası.ToString() + "\n" +
                           "Vergiler Düşüldükten Sonra Maaş: " + maasCocukParasiCikarimi.ToString() + "\n" +
                           "Oto. Kat. Bes. Üyeliği Düşüldükten Sonra Maaşınızın Son Hali: " + maasinSonHali.ToString() + "\n" +
                           "Alacağınız Maaş Tutarı: " + maasinSonHali.ToString(), "MAAŞ BİLGİLERİ");
            baglanti.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.OpenForms["Form1"].Show();
            this.Hide();
        }
    }
}
