using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Xml.Linq;

// Oyuncu sınıfı
public class Player
{
    public int Lives { get; set; }
    public int Score { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public List<string> SpecialAbilities { get; set; }

    public Player()
    {
        Lives = 3;
        Score = 0;
        X = 0;
        Y = 0;
        SpecialAbilities = new List<string>();
    }

    public void Move(int deltaX, int deltaY)
    {
        // Oyuncunun hareket etmesi
        X += deltaX;
        Y += deltaY;
    }

    public void TakeDamage(int damage)
    {
        // Oyuncunun hasar alması
        Lives -= damage;
        if (Lives < 0)
        {
            Lives = 0;
        }
    }

    public void CollectSpecialAbility(string ability)
    {
        // Oyuncunun özel yetenek toplaması
        SpecialAbilities.Add(ability);
    }

    public void UseSpecialAbility(string ability)
    {
        // Oyuncunun özel yeteneği kullanması
        if (SpecialAbilities.Contains(ability))
        {
            // Özel yeteneğin kullanılmasıyla ilgili işlemler
            SpecialAbilities.Remove(ability);
        }
    }
}


// Tuzak sınıfı
public class Trap
{
    public int X { get; set; }
    public int Y { get; set; }
    public string Type { get; set; }

    public Trap(int x, int y, string type)
    {
        X = x;
        Y = y;
        Type = type;
    }

    // Bu metod, tuzakların rastgele konumlandırılmasını sağlar
    public static Trap[] GenerateTraps(int totalTraps)
    {
        Random random = new Random();
        string[] trapTypes = { "Type1", "Type2", "Type3" }; // Tuzak türleri

        Trap[] traps = new Trap[totalTraps];

        for (int i = 0; i < totalTraps; i++)
        {
            int x = random.Next(0, 10); // Rastgele X koordinatı (0-9)
            int y = random.Next(0, 10); // Rastgele Y koordinatı (0-9)
            string type = trapTypes[random.Next(trapTypes.Length)]; // Rastgele tuzak türü

            traps[i] = new Trap(x, y, type);
        }

        return traps;
    }
}


// Oyun sınıfı
public class Game
{
    private Player player;
    private List<Trap> traps;
    private int elapsedTimeInSeconds;
    private char[,] gameBoard; // Oyun alanını temsil eden matris

    //oyuncunun özel yetenekler toplaması ve kullanmasına olanak tanır. Örneğin, oyuncu bir "Zıplama" yeteneği toplayabilir ve bu yeteneği kullanarak engelleri aşabilir.
    public List<SpecialAbility> SpecialAbilities { get; set; }

    public void CollectSpecialAbility(string ability)
    {
        SpecialAbilities.Add(ability);
    }

    public void UseSpecialAbility(string ability)
    {
        if (SpecialAbilities.Contains(ability))
        {
            // Özel yeteneğin kullanılmasıyla ilgili işlemler
            SpecialAbilities.Remove(ability);
        }
    }
    public Game()
    {
        player = new Player { Lives = 3, Score = 0, X = 0, Y = 0 };
        traps = GenerateTraps();
        elapsedTimeInSeconds = 0;
        gameBoard = new char[Console.WindowWidth, Console.WindowHeight];
        InitializeGameBoard();
    }

    // Tuzakları oluştur
    public List<Trap> GenerateTraps()
    {
        // Tuzakları oluşturacak bir dizi oluştur
        List<Trap> traps = new List<Trap>();

        // Tuzak sayısını belirleyin
        int totalTraps = 10;

        // Her bir tuzağı oluşturun
        for (int i = 0; i < totalTraps; i++)
        {
            // Tuzak koordinatlarını rastgele olarak oluşturun
            int x = new Random().Next(0, Console.WindowWidth);
            int y = new Random().Next(0, Console.WindowHeight);

            // Tuzak türünü rastgele olarak oluşturun
            string type = "Type1"; // Varsayılan tuzak türü
            switch (new Random().Next(0, 3))
            {
                case 0:
                    type = "Type1"; // Küçük hasar veren tuzak
                    break;
                case 1:
                    type = "Type2"; // Orta hasar veren tuzak
                    break;
                case 2:
                    type = "Type3"; // Büyük hasar veren tuzak
                    break;
            }

            // Tuzağı oluştur
            traps.Add(new Trap(x, y, type));
        }

        return traps;
    }

    public void Start()
    {
        Console.WriteLine("===== Oyun Başlıyor =====");
        Console.WriteLine("Oyunu başlatmak için ENTER tuşuna basınız...");
        Console.ReadLine();
        Console.Clear();

        // Oyuncu başlangıç pozisyonunu belirle
        player.X = 0;
        player.Y = 0;

        // Oyuncuya başlangıç canı ekle
        player.Lives = 3;

        // Oyuncunun başlangıç skoru
        player.Score = 0;

        // Zaman sayacını sıfırla
        elapsedTimeInSeconds = 0;

        // Oyunu göster
        Render();
    }

    public void Update()
    {
        // Zamanı güncelle
        elapsedTimeInSeconds++;

        // Oyuncu hareketini kontrol et
        if (Console.KeyAvailable)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            HandlePlayerInput(keyInfo);
        }

        // Tuzak kontrolü
        CheckTraps();

        // Diğer kontrolleri buraya ekleyebilirsiniz (bomba düşme, düşman hareketi, vb.)
        Render();
    }

    private void InitializeGameBoard()
    {
        // Oyun alanını boş karakterlerle doldur
        for (int x = 0; x < Console.WindowWidth; x++)
        {
            for (int y = 0; y < Console.WindowHeight; y++)
            {
                gameBoard[x, y] = ' ';
            }
        }
    }

    private void DrawGameBoard()
    {
        // Oyun alanını temizle
        Console.Clear();

        // Oyun tahtasını çiz
        for (int y = 0; y < Console.WindowHeight; y++)
        {
            for (int x = 0; x < Console.WindowWidth; x++)
            {
                // Oyuncu pozisyonunu kontrol et
                if (x == player.X && y == player.Y)
                {
                    Console.Write("P");
                }
                else
                {
                    // Tuzak kontrolü
                    bool trapExists = false;
                    foreach (var trap in traps)
                    {
                        if (x == trap.X && y == trap.Y)
                        {
                            Console.Write("T");
                            trapExists = true;
                            break;
                        }
                    }

                    // Tuzak yoksa boş karakter çiz
                    if (!trapExists)
                    {
                        Console.Write(" ");
                    }
                }
            }
            Console.WriteLine();
        }
    }

    public void Render()
    {
        // Oyun ekranını çiz
        Console.Clear();

        // Oyun alanını çiz
        DrawGameBoard();

        // Oyuncu bilgilerini göster
        Console.WriteLine($"Can: {player.Lives}  Skor: {player.Score}  Geçen Süre: {elapsedTimeInSeconds} saniye");
        Console.WriteLine("------------------------------");

        // Oyuncu pozisyonunu işle
        gameBoard[player.X, player.Y] = 'P';

        // Oyuncu pozisyonunu göster
        int clampedX = Math.Max(0, Math.Min(player.X, Console.WindowWidth - 1));
        int clampedY = Math.Max(0, Math.Min(player.Y, Console.WindowHeight - 1));

        Console.SetCursorPosition(clampedX, clampedY);
        Console.Write("P");

        // Tuzakları işe
        foreach (var trap in traps)
        {
            gameBoard[trap.X, trap.Y] = 'T';
        }
    }

        // Tuzakları göster
        foreach (var trap in traps)
        {
            int trapClampedX = Math.Max(0, Math.Min(trap.X, Console.WindowWidth - 1));
            int trapClampedY = Math.Max(0, Math.Min(trap.Y, Console.WindowHeight - 1));

            Console.SetCursorPosition(trapClampedX, trapClampedY);
            Console.Write("T");
        }
    }
}

private void HandlePlayerInput(ConsoleKeyInfo keyInfo)
    {
        // Oyuncu hareketini kontrol et
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                player.Y--;
                break;
            case ConsoleKey.DownArrow:
                player.Y++;
                break;
            case ConsoleKey.LeftArrow:
                player.X--;
                break;
            case ConsoleKey.RightArrow:
                player.X++;
                break;
            case ConsoleKey.P:
                // Pause işlevini buraya ekleyebilirsiniz
                break;
            default:
                break;
        }
    }

private void CheckTraps()
{
    // Oyuncunun tuzaklara çarpıp çarpmadığını kontrol et
    foreach (var trap in traps)
    {
        if (player.X == trap.X && player.Y == trap.Y)
        {
            Console.WriteLine("Ouch! Bir tuzaga yakalandınız!");
            player.Lives--;
            // Daha fazla tuzak kontrolü ekleyebilirsiniz
        }
    }
}

// Program sınıfı
class Program
{
    static void Main()
    {
        // Oyunu başlat
        Game game = new Game();

        // Menüyü göster
        ShowMenu(game);

        // Oyun döngüsü
        while (true)
        {
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            // Oyuncu ENTER tuşuna bastığında oyunu başlat
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                game.Start();

                // Oyun döngüsü
                while (true)
                {
                    game.Update();
                    game.Render();

                    // Oyunu duraklatmak için P tuşuna basın
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo pauseKey = Console.ReadKey(true);
                        if (pauseKey.Key == ConsoleKey.P)
                        {
                            Console.WriteLine("Oyun duraklatıldı. Devam etmek için ENTER tuşuna basınız...");
                            Console.ReadLine();
                        }
                    }

                    // Oyunu bitirme koşulları burada kontrol edilebilir
                    // Örneğin, oyuncunun canları bitmişse veya oyun bitiş çizgisine ulaşmışsa
                    // ...

                    // Her seferinde ekran temizle
                    Console.Clear();
                }
            }
            else if (keyInfo.Key == ConsoleKey.I)
            {
                // En iyi skorları gösterme
                ShowTopScores();
            }
        }
    }

    static void ShowMenu(Game game)
    {
        Console.WriteLine("===== Oyun Menüsü =====");
        Console.WriteLine("Oyunu başlatmak için ENTER tuşuna basınız.");
        Console.WriteLine("En iyi skorları görmek için I tuşuna basınız.");
        Console.WriteLine("Oyunu kapatmak için herhangi bir tuşa basınız.");
    }

    static void ShowTopScores()
    {
        // En iyi skorları görüntüleme
        // Bu kısmı daha sonra doldurun
        // ...
        Console.WriteLine("===== En İyi Skorlar =====");
    }
}
