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
using System_do_zarzadzania_obslugi_sprzedazy.Classes;

namespace System_do_zarzadzania_obslugi_sprzedazy.Winows
{
    /// <summary>
    /// Logika interakcji dla klasy DebtorWindow.xaml
    /// </summary>
    public partial class DebtorWindow : Window
    {
        private Debter debter;


        public DebtorWindow(Debter Debter) 
        {
            InitializeComponent();
            debter= Debter;
        }

        private void ClientPDF_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
