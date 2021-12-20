using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VTYS_Proje
{
    public partial class Form1 : Form
    {
        NpgsqlConnection baglanti = new NpgsqlConnection("server=localHost; port=5432; Database=Kitapevi; user ID=postgres; password=Seda.1812");
        string sorgu;
        public Form1()
        {
            InitializeComponent();
        }
        private void ComboboxIlNo(ComboBox comboBox)
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from \"Il\"", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox.DisplayMember = "iladi";
            comboBox.ValueMember = "ilno";
            comboBox.DataSource = dt;
            baglanti.Close();
        }
        private void ComboboxKategoriNo(ComboBox comboBox)
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from \"Kategori\"", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox.DisplayMember = "kategoriadi";
            comboBox.ValueMember = "kategorino";
            comboBox.DataSource = dt;
            baglanti.Close();
        }
        private void ComboboxMusteriNo(ComboBox comboBox)
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from \"Musteri\",\"Kisi\" where \"Musteri\".\"kisino\"=\"Kisi\".\"kisino\"", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox.DisplayMember = "adsoyad";
            comboBox.ValueMember = "kisino";
            comboBox.DataSource = dt;
            baglanti.Close();
        }
        private void ComboboxUrunNo(ComboBox comboBox)
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from \"Urun\",\"Kategori\",\"Envanter\" where \"Urun\".\"kategori\"=\"Kategori\".\"kategorino\" and \"Urun\".\"envanter\"=\"Envanter\".\"envanterno\"", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox.DisplayMember = "urunadi";
            comboBox.ValueMember = "urunno";
            comboBox.DataSource = dt;
            baglanti.Close();
        }
        private void ComboboxOdemeTuru(ComboBox comboBox)
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from \"Odeme\"", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox.DisplayMember = "odemeturu";
            comboBox.ValueMember = "odemeno";
            comboBox.DataSource = dt;
            baglanti.Close();
        }
        private void ComboboxKargoNo(ComboBox comboBox)
        {
            baglanti.Open();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from \"Kargolama\"", baglanti);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox.DisplayMember = "firmaadi";
            comboBox.ValueMember = "firmano";
            comboBox.DataSource = dt;
            baglanti.Close();
        }
        private void MusteriListele()
        {
            sorgu = "SELECT \"Kisi\".kisino as \"Müşteri No\", \"Kisi\".adsoyad as \"Adı Soyadı\", \"Musteri\".kimlikno as \"Kimlik No\", \"Iletisim\".telefon as \"Telefon Numarası\", \"Iletisim\".ilno as \"İl Kodu\", \"Iletisim\".adres as \"Adresi\" " +
                "FROM \"Kisi\",\"Musteri\",\"Iletisim\" " +
                "WHERE \"Kisi\".kisino=\"Musteri\".kisino AND \"Iletisim\".kisino=\"Musteri\".kisino";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dgv_Musteri.DataSource = ds.Tables[0];
        }
        private void UrunListele()
        {
            sorgu = "SELECT \"Urun\".urunno as \"Ürün No\",\"Kategori\".kategoriadi as \"Kategori\", \"Urun\".urunadi as \"Ürün Adı\", \"Urun\".urunfiyati as \"Fiyat\", \"Envanter\".stok as \"Stok\"" +
                "FROM \"Urun\",\"Envanter\",\"Kategori\"" +
                "WHERE \"Urun\".envanter=\"Envanter\".envanterno and \"Urun\".kategori=\"Kategori\".kategorino";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dgv_Urun.DataSource = ds.Tables[0];
        }
        private void SiparisListele()
        {
            sorgu = "SELECT \"Siparis\".siparisno as \"Sipariş No\",\"Kisi\".adsoyad as \"Müşteri\", \"Urun\".urunadi as \"Ürün Adı\", \"Urun\".urunfiyati as \"Toplam Tutar\", \"Fatura\".faturatarihi as \"Fatura Tarihi\", \"Odeme\".odemeturu as \"Ödeme Türü\", \"Kargolama\".firmaadi as \"Kargo Firması\"" +
                "FROM \"Siparis\",\"Kisi\",\"Urun\",\"Fatura\",\"Odeme\",\"Kargolama\"" +
                "WHERE \"Siparis\".musterino=\"Kisi\".kisino and \"Siparis\".kargono=\"Kargolama\".firmano and \"Siparis\".urunno=\"Urun\".urunno and \"Siparis\".faturano=\"Fatura\".faturano and \"Odeme\".odemeno=\"Fatura\".odemeno ";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dgv_Siparis.DataSource = ds.Tables[0];
        }
        private void EnvanterListele()
        {
            sorgu = "SELECT \"Envanter\".envanterno as \"Envanter No\", \"Urun\".urunadi as \"Ürün Adı\", \"Envanter\".stok as \"Stok\" " +
                "FROM \"Envanter\",\"Urun\"" +
                "WHERE \"Envanter\".envanterno=\"Urun\".envanter";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dgv_Envanter.DataSource = ds.Tables[0];
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ComboboxIlNo(cbMusteriIl);
            ComboboxKategoriNo(cbUrunKategori);
            ComboboxMusteriNo(cbSiparisMusteri);
            ComboboxOdemeTuru(cbSiparisOdemeTuru);
            ComboboxUrunNo(cbSiparisUrun);
            ComboboxKargoNo(cbKargoNo);
            MusteriListele();
            UrunListele();
            SiparisListele();
            EnvanterListele();
            CesitSayisiListele();
            GuncelMusteriListele();
        }
        private void btnMusteriEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select musteriekle(@p1,@p2,@p3,@p4,@p5);", baglanti);
            komut.Parameters.AddWithValue("@p1", txtMusteriAdSoyad.Text);
            komut.Parameters.AddWithValue("@p2", txtMusteriKimlik.Text);
            komut.Parameters.AddWithValue("@p3", txtMusteriTelefon.Text);
            komut.Parameters.AddWithValue("@p4", rtbMusteriAdres.Text);
            komut.Parameters.AddWithValue("@p5", cbMusteriIl.SelectedValue);

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri ekleme işlemi başarıyla tamamlandı.");
            MusteriListele();
            GuncelMusteriListele();
            SiparisListele();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            sorgu = "SELECT \"Kisi\".kisino as \"Müşteri No\", \"Kisi\".adsoyad as \"Adı Soyadı\", \"Musteri\".kimlikno as \"Kimlik No\", \"Iletisim\".telefon as \"Telefon Numarası\", \"Iletisim\".ilno as \"İl Kodu\", \"Iletisim\".adres as \"Adresi\" " +
         $"FROM \"Kisi\",\"Musteri\",\"Iletisim\" WHERE \"Kisi\".kisino=\"Musteri\".kisino AND  \"Iletisim\".kisino=\"Musteri\".kisino AND \"Kisi\".adsoyad LIKE '{txtMusteriAdSoyad.Text}%' AND \"Musteri\".kimlikno LIKE '{txtMusteriKimlik.Text}%' AND \"Iletisim\".telefon LIKE '{txtMusteriTelefon.Text}%' AND \"Iletisim\".ilno = {cbMusteriIl.SelectedValue} AND \"Iletisim\".adres LIKE '{rtbMusteriAdres.Text}%'";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dgv_Musteri.DataSource = ds.Tables[0];
            baglanti.Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select musteriguncelle(@p1,@p2,@p3,@p4,@p5,@p6);", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(txtMusteriNo.Text));
            komut.Parameters.AddWithValue("@p2", txtMusteriAdSoyad.Text);
            komut.Parameters.AddWithValue("@p3", txtMusteriKimlik.Text);
            komut.Parameters.AddWithValue("@p4", txtMusteriTelefon.Text);
            komut.Parameters.AddWithValue("@p5", rtbMusteriAdres.Text);
            komut.Parameters.AddWithValue("@p6", cbMusteriIl.SelectedValue);

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri güncelleme işlemi başarıyla tamamlandı.");
            MusteriListele();
            SiparisListele();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select musterisil(@p1);", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(txtMusteriNo.Text));

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri silme işlemi başarıyla tamamlandı.");
            MusteriListele();
            GuncelMusteriListele();
        }

        private void btnUrunEkle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select urunekle(@p1,@p2,@p3,@p4);", baglanti);
            komut.Parameters.AddWithValue("@p1", txtUrunAdi.Text);
            komut.Parameters.AddWithValue("@p2", cbUrunKategori.SelectedValue);
            komut.Parameters.AddWithValue("@p3", int.Parse(txtUrunFiyati.Text));
            komut.Parameters.AddWithValue("@p4", int.Parse(txtUrunStok.Text));

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün ekleme işlemi başarıyla tamamlandı.");
            UrunListele();
            CesitSayisiListele();
            EnvanterListele();
            SiparisListele();
        }

        private void btnUrunAra_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            try
            {
                sorgu = "SELECT \"Urun\".urunno as \"Ürün No\", \"Urun\".urunadi as \"Ürün Adı\", \"Kategori\".kategoriadi as \"Kategori\", \"Urun\".urunfiyati as \"Fiyat\", \"Envanter\".stok as \"Stok\" " +
             $"FROM \"Urun\",\"Kategori\",\"Envanter\" WHERE \"Urun\".kategori=\"Kategori\".kategorino AND  \"Urun\".envanter=\"Envanter\".envanterno AND \"Urun\".urunadi LIKE '{txtUrunAdi.Text}%' AND \"Urun\".urunfiyati = {int.Parse(txtUrunFiyati.Text)} AND \"Kategori\".kategorino = {cbUrunKategori.SelectedValue} AND \"Envanter\".stok = {int.Parse(txtUrunStok.Text)}";
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dgv_Urun.DataSource = ds.Tables[0];

            }
            catch
            {
                MessageBox.Show("Boş bırakılamaz.");

            }
            baglanti.Close();
        }

        private void btnUrunSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select urunsil(@p1);", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(txtUrunNo.Text));

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün silme işlemi başarıyla tamamlandı.");
            UrunListele();
            CesitSayisiListele();
            EnvanterListele();
        }

        private void btnUrunGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select urunguncelle(@p1,@p2,@p3,@p4,@p5);", baglanti);
            komut.Parameters.AddWithValue("@p1", int.Parse(txtUrunNo.Text));
            komut.Parameters.AddWithValue("@p2", txtUrunAdi.Text);
            komut.Parameters.AddWithValue("@p3", int.Parse(txtUrunFiyati.Text));
            komut.Parameters.AddWithValue("@p4", cbUrunKategori.SelectedValue);
            komut.Parameters.AddWithValue("@p5", int.Parse(txtUrunStok.Text));

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Ürün güncelleme işlemi başarıyla tamamlandı.");
            UrunListele();
            CesitSayisiListele();
        }

        private void btnSiparisOlustur_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            NpgsqlCommand komut = new NpgsqlCommand("select siparisekle(@p1,@p2,@p3,@p4);", baglanti);
            komut.Parameters.AddWithValue("@p1", cbSiparisMusteri.SelectedValue);
            komut.Parameters.AddWithValue("@p2", cbKargoNo.SelectedValue);
            komut.Parameters.AddWithValue("@p3", cbSiparisUrun.SelectedValue);
            komut.Parameters.AddWithValue("@p4", cbSiparisOdemeTuru.SelectedValue);

            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Sipariş oluşturma işlemi başarıyla tamamlandı.");
            SiparisListele();
            EnvanterListele();
        }

        private void btnMusteriListele_Click(object sender, EventArgs e)
        {
            MusteriListele();
        }

        private void btnUrunListele_Click(object sender, EventArgs e)
        {
            UrunListele();
        }
        private void CesitSayisiListele()
        {
            sorgu = "select * from \"Urunsayilari\"";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            lblKitapCesit.Text = ds.Tables[0].Rows[0]["kitap"].ToString();
            lblFilmCesit.Text = ds.Tables[0].Rows[0]["film"].ToString();
        }
        private void GuncelMusteriListele()
        {
            sorgu = "select * from \"Guncelmusteri\"";
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            lblGuncelMusteri.Text = ds.Tables[0].Rows[0]["musterisayisi"].ToString();
        }
    }
}
