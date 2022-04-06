using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace vASEK3_4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private double F1(double x)
        {
            double A = Math.Log(1/x);
            return A;
        }
        private double F2(double x)
        {
            double A = Math.Pow(Math.Cosh(Math.Log(x)),-1);
            return A;
        }
        private double F3(double x)
        {
            double A = Math.Sin(Math.Sqrt(Math.Exp(5 / x)));
            return A;
        }
        private double F4(double x)
        {
            int i = 0;
            double A = 0;
            do
            {
                i++;
                A += (i * x * x - 1) * Math.Sqrt(i);

            } while (i<1000000);
            return A;
        }
        private void Vich_Click(object sender, EventArgs e)
        {
            Func.Text = "";
            double x0 = Convert.ToDouble(Textx0.Text);
            double xn = Convert.ToDouble(Textxn.Text);
            double h = Convert.ToDouble(Texth.Text),x = x0;
            double F = 0;
            int ch = 0;
            do
            {
                if (x < 0 || x == 0)
                {
                    Func.Text += x + ": " + "ОDZ ne podxodit x";
                    Func.Text += "\r\n"; 
                    ch++;
                }
                else
                {
                    F = F1(x) + F2(x) + F3(x) + F4(x);
                    Func.Text += x + ": " + F;
                    Func.Text += "\r\n";
                    ch++;
                }
                x = x0 + ch * h ;
            } while (x <= xn);
            if (ch > 0)
            {
                MessageBox.Show( "Odno ili ne odno znach ne korect");
            }
        }

        private void GetFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            string[] Direct = path.Split(new char[] { '\\' });
            string[] DirectDel = path.Split(new char[] { '\\' });
            int n = 0;
            char[] number = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8','9' };
            char[] bukv = new char[10] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J' };
            int DirNumb = 0;
            string CreateSubPath = "";
            foreach (string s in Direct)
            {
                n++;
            }
            textBox2.Text += "Old path: " + path;
            textBox2.Text += "\r\n";
            for (int i = 1; i< n-1; i++)
            {
                int ch = -1;
                ch = Direct[i].IndexOfAny(number);

                if (ch != -1)
                {
                    DirNumb++;
                    for (int j = 0; j < 10; j++)
                    {
                        Direct[i] = Direct[i].Replace(number[j],bukv[j]);
                    }
                        //Direct[i] = Direct[i].Replace('0', 'A');
                        //Direct[i] = Direct[i].Replace('1', 'B');
                        //Direct[i] = Direct[i].Replace('2', 'C');
                        //Direct[i] = Direct[i].Replace('3', 'D');
                        //Direct[i] = Direct[i].Replace('4', 'E');
                        //Direct[i] = Direct[i].Replace('5', 'F');
                        //Direct[i] = Direct[i].Replace('6', 'G');
                        //Direct[i] = Direct[i].Replace('7', 'H');
                        //Direct[i] = Direct[i].Replace('8', 'I');
                        //Direct[i] = Direct[i].Replace('9', 'J');
                }
            }
            string buff = Direct[1];
            Direct[1] = Direct[n - 2];
            Direct[n - 2] = buff;
            for (int i = 1; i < n - 2; i++)
            { 
                    CreateSubPath += Direct[i] + "\\";
            }
            
            CreateSubPath +=  Direct[n-2];
            string CreatePath = Direct[0] +"\\"+ CreateSubPath;
            for (int i = 3; i < n - 1; i++)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(CreatePath);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }
            }
            try
            {
                File.Copy(textBox1.Text, CreatePath + "\\" + Path.GetFileName(textBox1.Text));
            }
            catch 
            {
                MessageBox.Show("Уже существует");
            }
            textBox2.Text += "New path: " + CreatePath + "\\" + Path.GetFileName(textBox1.Text);
            textBox2.Text += "\r\n";
            label4.Text = "Количество папок с цифрой: " + DirNumb;
            string DirDel = DirectDel[0] + "\\" + DirectDel[1];
            DirectoryInfo Info = new DirectoryInfo(DirDel);
            if (Info.Exists)
            {
                Info.Delete(true);
                Console.WriteLine("Каталог удален");
            }
            else
            {
                Console.WriteLine("Каталог не существует");
            }
        }
    }
}
