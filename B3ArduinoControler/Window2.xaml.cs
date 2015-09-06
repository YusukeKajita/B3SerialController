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
using System.Windows.Shapes;

namespace B3ArduinoControler
{
    /// <summary>
    /// Interaction logic for Window2.xaml
    /// </summary>
    public partial class Window2 : Window
    {
        MainWindow mainwindow;
        public Window2(MainWindow mainwindow)
        {
            InitializeComponent();
            this.mainwindow = mainwindow;
            this.MouseLeftButtonDown += (s, e) => this.DragMove();
        }

        private void Manipulate_KeyDown(object sender, KeyEventArgs e)
        {
            this.mainwindow.Master_KeyDown(this, e);
        }

        private void Manipulate_KeyUp(object sender, KeyEventArgs e)
        {
            this.mainwindow.Master_KeyUp(this, e);

        }
    }
}
