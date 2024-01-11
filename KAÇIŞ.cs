using System;
using System.Collections.Generic;
using System.Linq;

enum OyunSeviyesi
{
    Seviye1,
    Seviye2,
    Seviye3
}

class Skor
{
    public string OyuncuAdi { get; set; }
    public int Puan { get; set; }
}

class SkorListesi
{
    private List<Skor> skorlar;

    public SkorListesi()
    {
        skorlar = new List<Skor>();
        // Skorları bir dosyadan okuyabilirsiniz.
    }

    public void SkorEkle(string oyuncuAdi, int puan)
    {
        Skor yeniSkor = new Skor { OyuncuAdi = oyuncuAdi, Puan = puan };
        skorlar.Add(yeniSkor);

        // Skorları sırala ve en iyi 5'i güncelle...
        skorlar = skorlar.OrderByDescending(s => s.Puan).Take(5).ToList();
    }

    public void SkorlariGoruntule()
    {
        Console.WriteLine("En İyi Skorlar:");
        foreach (var skor in skorlar)
        {
            Console.WriteLine($"{skor.OyuncuAdi}: {skor.Puan}");
        }
    }
}

class Oyuncu
{
    public int CanHakki { get; set; }

    public Oyuncu()
    {
        CanHakki = 3; // İstediğiniz başlangıç can sayısını belirleyebilirsiniz.
    }

    public void HareketEt(ConsoleKeyInfo keyInfo)
    {
        // Oyuncu hareket etme işlemleri...
    }
}

class Tuzak
{
    public void Uygula(Oyuncu oyuncu)
    {
        // Tuzak uygulama işlemleri...
    }
}

class Bomba
{
    public void Uygula(Oyuncu oyuncu)
    {
        // Bomba uygulama işlemleri...
    }
}

class DusmanAsker
{
    public void HareketEt()
    {
        // Düşman askeri hareket etme işlemleri...
    }
}

class GizliKutu
{
    public void Kontrol(Oyuncu oyuncu)
    {
        // Gizli kutu kontrol işlemleri...
    }
}

class PauseTusu
{
    public void Kontrol()
    {
        // Pause tuşu kontrol işlemleri...
    }
}

class OyunMotoru
{
    private OyunSeviyesi seviye;
    private int gecenSure;
    private Oyuncu oyuncu;

    public void Baslat()
    {
        Console.WriteLine("Oyuna hoş geldiniz!");
        MenuGoster();
    }

    private void MenuGoster()
    {
        Console.WriteLine("Oyun Menüsü");
        Console.WriteLine("a- Seviye 1'i başlatmak için '1' tuşuna basınız.");
        Console.WriteLine("b- Seviye 2'yi başlatmak için '2' tuşuna basınız.");
        Console.WriteLine("c- Seviye 3'ü başlatmak için '3' tuşuna basınız.");
        Console.WriteLine("d- En iyi skorları görüntüle");
        Console.WriteLine("e- Oyundan çıkmak için 'ESC' tuşuna basınız.");

        ConsoleKeyInfo keyInfo = Console.ReadKey(true);
        switch (keyInfo.Key)
        {
            case ConsoleKey.D1:
                seviye = OyunSeviyesi.Seviye1;
                SeviyeBaslat();
                break;
            case ConsoleKey.D2:
                seviye = OyunSeviyesi.Seviye2;
                SeviyeBaslat();
                break;
            case ConsoleKey.D3:
                seviye = OyunSeviyesi.Seviye3;
                SeviyeBaslat();
                break;
            case ConsoleKey.D:
                SkorlariGoruntule();
                break;
            case ConsoleKey.Escape:
                Environment.Exit(0);
                break;
            default:
                MenuGoster();
                break;
        }
    }

    private void SeviyeBaslat()
    {
        Console.WriteLine($"Seviye {seviye} başlıyor!");
        oyuncu = new Oyuncu();

        while (oyuncu.CanHakki > 0)
        {
            Guncelle();
            // Oyun ekranını güncelle...
            // Yeniden çizme işlemleri...
        }

        OyunuBitir();
    }

    private void Guncelle()
    {
        gecenSure++;

        switch (seviye)
        {
            case OyunSeviyesi.Seviye1:
                // Seviye 1 özel güncelleme işlemleri...
                break;

            case OyunSeviyesi.Seviye2:
                // Seviye 2 özel güncelleme işlemleri...
                break;

            case OyunSeviyesi.Seviye3:
                // Seviye 3 özel güncelleme işlemleri...
                break;
        }

        // Diğer genel güncelleme işlemleri...
        GizliKutu gizliKutu = new GizliKutu();
        gizliKutu.Kontrol(oyuncu);

        PauseTusu pauseTusu = new PauseTusu();
        pauseTusu.Kontrol();

        // Seviyeler arası geçişleri kontrol et...
        if (gecenSure % 60 == 0) // Her 60 saniyede bir
        {
            if (gecenSure % 180 == 0) // Her 3 seviyede bir
            {
                SkorHesapla();
                seviye++;
                SeviyeBaslat();
            }
        }
    }

    private void SkorHesapla()
    {
        int puan = oyuncu.CanHakki * 500 + (1000 - gecenSure);
        Console.WriteLine($"Seviye {seviye} tamamlandı! Toplam puan: {puan}");
        SkorListesi skorListesi = new SkorListesi();
        skorListesi.SkorEkle("Oyuncu", puan);
    }

    private void OyunuBitir()
    {
        Console.WriteLine("Oyun bitti. Skorlar:");
        SkorListesi skorListesi = new SkorListesi();
        skorListesi.SkorlariGoruntule();
        Environment.Exit(0);
    }
}

class Program
{
    static void Main()
    {
        OyunMotoru oyunMotoru = new OyunMotoru();
        oyunMotoru.Baslat();
    }
}
