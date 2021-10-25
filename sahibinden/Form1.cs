using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;


namespace sahibinden
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti;
        OleDbDataAdapter adaptor;
        OleDbCommand komut;
        DataSet verikumesi;

        //Veritabanından alıp GridView nesnesine akataracak
        void VeriDoldur()
        {
            baglanti = new OleDbConnection("Provider=Microsoft.ACE.Oledb.12.0; Data Source=./sahibindenDB.accdb");
            adaptor = new OleDbDataAdapter("Select * from tblUsers", baglanti);
            verikumesi = new DataSet();
            baglanti.Open();
            adaptor.Fill(verikumesi, "tblUsers");
            dataGridView1.DataSource = verikumesi.Tables["tblUsers"];
            baglanti.Close();

        }
        private void btnUyeOl_Click(object sender, EventArgs e)
        {

            if (Convert.ToInt32(txtYas.Text)>=18)
            {

                komut = new OleDbCommand();
                baglanti.Open();
                komut.Connection = baglanti;
                komut.CommandText = "insert into tblUsers (ad,soyad, eposta,sifre, yas) values (@pad,@psoyad, @peposta,@psifre, @pyas)";
                komut.Parameters.AddWithValue("@pad", txtAd.Text);
                komut.Parameters.AddWithValue("@psoyad", txtSoyad.Text);
                komut.Parameters.AddWithValue("@peposta", txtEposta.Text);
                komut.Parameters.AddWithValue("@psifre", txtSifre.Text);
                komut.Parameters.AddWithValue("@pyas", txtYas.Text);

                komut.ExecuteNonQuery();
                baglanti.Close();
                VeriDoldur();
            }
            else
            {
                MessageBox.Show("Yaşınız 18'den küçük olamaz.");
            }




        }

        private void Form1_Load(object sender, EventArgs e)
        {
            VeriDoldur();
        }
    }
}
