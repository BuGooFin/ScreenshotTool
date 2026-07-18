using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ScreenshotTool
{
    /// <summary>
    /// Interaction logic for SelectionWindow.xaml
    /// </summary>
    public partial class SelectionWindow : Window
    {
        private System.Windows.Point baslangicNoktasi;
        public SelectionWindow()
        {
            InitializeComponent();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e) 
        {
            baslangicNoktasi = e.GetPosition(this);

            secimDikdortgeni.Visibility = Visibility.Visible;
            secimDikdortgeni.Width = 0;
            secimDikdortgeni.Height = 0;
            secimDikdortgeni.Margin = new Thickness(baslangicNoktasi.X, baslangicNoktasi.Y, 0, 0);
        }
        private void Grid_MouseMove(object sender, MouseEventArgs e) 
        {
            if (e.LeftButton == MouseButtonState.Pressed) 
            {
                System.Windows.Point simdikiNokta = e.GetPosition(this);

                double x = Math.Min(baslangicNoktasi.X, simdikiNokta.X);
                double y = Math.Min(baslangicNoktasi.Y, simdikiNokta.Y);
                double genislik = Math.Abs(simdikiNokta.X - baslangicNoktasi.X);
                double yukseklik = Math.Abs(simdikiNokta.Y - baslangicNoktasi.Y);

                secimDikdortgeni.Margin = new Thickness(x, y, 0, 0);
                secimDikdortgeni.Width = genislik;
                secimDikdortgeni.Height = yukseklik;
            }
        }
        private void Grid_MouseUp(object sender, MouseButtonEventArgs e) 
        {
            System.Windows.Point bitisNoktasi = e.GetPosition(this);
            int x = (int)Math.Min(baslangicNoktasi.X, bitisNoktasi.X);
            int y = (int)Math.Min(baslangicNoktasi.Y, bitisNoktasi.Y);
            int genislik = (int)Math.Abs(bitisNoktasi.X - baslangicNoktasi.X);
            int yukseklik = (int)Math.Abs(bitisNoktasi.Y - baslangicNoktasi.Y);
            if (genislik == 0 || yukseklik == 0)
            {
                MessageBox.Show("Lütfen geçerli bir alan seçin!");
                this.Close();
                return;
            }
            double ekranOlcegi = VisualTreeHelper.GetDpi(this).DpiScaleX;

            x = (int)(x * ekranOlcegi);
            y = (int)(y * ekranOlcegi);
            genislik = (int)(genislik * ekranOlcegi);
            yukseklik = (int)(yukseklik * ekranOlcegi);

            this.Hide();

            Bitmap ekranGoruntusu = new Bitmap(genislik, yukseklik);
            Graphics grafik = Graphics.FromImage(ekranGoruntusu);
            grafik.CopyFromScreen(x, y, 0, 0, new System.Drawing.Size(genislik, yukseklik));

            ekranGoruntusu.Save("C:/Users/burbu/Desktop/bolge_screenshot.png", ImageFormat.Png);

            this.Close();

            MessageBox.Show("Seçilen alan kaydedildi!");

        }
    }
}
