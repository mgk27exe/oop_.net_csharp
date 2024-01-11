using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace TimerOyun
{
    public partial class Form1 : Form
    {
        int sayi,sure;
        int b2 = 22; int b5 = 22; int b7 = 22; int b9 = 22; int b11 = 22;
        int b3 = 22; int b6 = 22; int b8 = 22; int b10 = 22; int b12 = 22;
        int b4 = 22;
        Random sayi1 = new Random();
        public Form1()
        {
            InitializeComponent();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayi = sayi1.Next(1, 12);
            if (sayi == 1) 
            {  button2.Location = new Point(b2, 170);   b2 = b2 + 5;  }
            if (sayi == 2)
            { button3.Location = new Point(b3, 221); b3 = b3 + 5; } 
            if (sayi == 3)
            { button4.Location = new Point(b4, 272); b4 = b4 + 5; } 
            if (sayi == 4)
            { button5.Location = new Point(b5, 323); b5 = b5 + 5; } 
            if (sayi == 5)
            { button6.Location = new Point(b6, 374); b6 = b6 + 5; } 
            if (sayi == 6)
            { button7.Location = new Point(b7, 425); b7 = b7 + 5; } 
            if (sayi == 7)
            { button8.Location = new Point(b8, 476); b8 = b8 + 5; } 
            if (sayi == 8)
            { button9.Location = new Point(b9, 527); b9 = b9 + 5; } 
            if (sayi == 9)
            { button10.Location = new Point(b10, 578); b10 = b10 + 5; } 
            if (sayi == 10)
            { button11.Location = new Point(b11, 629); b11 = b11 + 5; } 
            if (sayi == 11)
            { button12.Location = new Point(b12, 680); b12 = b12 + 5; }
            if (b2 >= 880 || b3 >= 880 || b4 >= 880 || b5 >= 880 || b6 >= 880 || b7 >= 880 || b8 >= 880 || b9 >= 880 || b10 >= 880 || b11 >= 880 || b12 >= 880)
            {
                timer1.Stop();
                timer2.Stop();
                if (b2 >= 880) { MessageBox.Show("Kazım Yarışı "+sure+" Saniyede Kazandı"); }
                if (b3 >= 880) { MessageBox.Show("Erdem Yarışı " + sure + " Saniyede Kazandı"); }
                if (b4 >= 880) { MessageBox.Show("Ömer Yarışı " + sure + " Saniyede Kazandı"); }
                if (b5 >= 880) { MessageBox.Show("Fuat Yarışı " + sure + " Saniyede Kazandı"); }
                if (b6 >= 880) { MessageBox.Show("Furkan Yarışı " + sure + " Saniyede Kazandı"); }
                if (b7 >= 880) { MessageBox.Show("Semih Yarışı " + sure + " Saniyede Kazandı"); }
                if (b8 >= 880) { MessageBox.Show("Sezai Yarışı " + sure + " Saniyede Kazandı"); }
                if (b9 >= 880) { MessageBox.Show("Mutlucan Yarışı " + sure + " Saniyede Kazandı"); }
                if (b10 >= 880) { MessageBox.Show("Abdulkadir Yarışı " + sure + " Saniyede Kazandı"); }
                if (b11 >= 880) { MessageBox.Show("Doğukan Yarışı " + sure + " Saniyede Kazandı"); }
                if (b12 >= 880) { MessageBox.Show("Bahadır Yarışı " + sure + " Saniyede Kazandı"); }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            timer2.Start();
            
        }
        private void timer2_Tick(object sender, EventArgs e)
        {
            sure++;
            label2.Text = sure.ToString();
        }
    }
}