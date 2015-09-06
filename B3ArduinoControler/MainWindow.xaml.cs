using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace B3ArduinoControler
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.IO.Ports.SerialPort serial;
        private bool isconnect;
        public Window2 ManipulationWindow;
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                this.isconnect = false;
                this.MouseLeftButtonDown += (s, e) => this.DragMove();
                this.ManipulationWindow = new Window2(this);
                this.ManipulationWindow.Show();

                this.ManipulationWindow.TextBlock_IsConnect.Text = this.isconnect.ToString();

                this.Closing += MainWindow_Closing;
                this.TextBox_BaudRate.Text = Properties.Settings.Default.BAUDRATE;
                this.TextBox_PortNumber.Text = Properties.Settings.Default.COMPORT;
                System.IO.FileStream fs;

                try
                {
                    fs = new System.IO.FileStream("data.dat", System.IO.FileMode.Open);
                }
                catch
                {
                    fs = new System.IO.FileStream("data.dat", System.IO.FileMode.Create);
                    System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, Encoding.ASCII);
                    sw.WriteLine(System.Environment.MachineName);
                    sw.Close();
                    fs.Close();
                    MessageBox.Show("ユーザー登録が完了しました．絶対にほかのPCで実行しないでください．");
                    fs = new System.IO.FileStream("data.dat", System.IO.FileMode.Open);
                }
                System.IO.StreamReader sr = new System.IO.StreamReader(fs, Encoding.ASCII);
                if (sr.ReadLine() != System.Environment.MachineName)
                {
                    sr.Close();
                    fs.Close();
                    MessageBox.Show("不正利用です．覚悟しなさい．");
                    for (int y = 0; y < System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Height; y += 1)
                    {
                        for (int x = 0; x < System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width; x += 1)
                        {
                            Window1 window1 = new Window1();
                            window1.Show();
                            window1.Top = y;
                            window1.Left = x;

                        }
                    }
                    this.Close();
                }
                sr.Close();
                fs.Close();
                this.ManipulationWindow.TextBlock_UserName.Text = System.Environment.MachineName;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Properties.Settings.Default.COMPORT = this.TextBox_PortNumber.Text;
            Properties.Settings.Default.BAUDRATE = this.TextBox_BaudRate.Text;
            Properties.Settings.Default.Save();

            this.ManipulationWindow.Close();
            if (this.isconnect)
            {
                this.serial.Close();
                this.serial.Dispose();
            }
        }

        public void Master_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.isconnect)
                {
                    this.serial.Write(e.Key.ToString());
                }
                this.ManipulationWindow.TextBlock_Keyborad.Text = "\"" + e.Key.ToString() + "\" = " + BitConverter.GetBytes(e.Key.ToString()[0])[0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Master_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (this.isconnect)
                {
                    this.serial.Write(" ");
                }
                this.ManipulationWindow.TextBlock_Keyborad.Text = "\" \"= " + BitConverter.GetBytes(' ')[0].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Connect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.isconnect == false)
                {

                    this.serial = new System.IO.Ports.SerialPort(this.TextBox_PortNumber.Text);
                    this.serial.BaudRate = int.Parse(this.TextBox_BaudRate.Text);
                    this.serial.Encoding = Encoding.ASCII;
                    this.serial.Open();

                    this.isconnect = true;
                    this.ManipulationWindow.TextBlock_IsConnect.Text = this.isconnect.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Disconnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.isconnect == true)
                {
                    this.isconnect = false;
                    this.ManipulationWindow.TextBlock_IsConnect.Text = this.isconnect.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
