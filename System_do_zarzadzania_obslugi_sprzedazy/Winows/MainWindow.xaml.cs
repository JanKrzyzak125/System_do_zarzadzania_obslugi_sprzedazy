﻿using System;
using System.Collections.Generic;
using System.Collections;
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
using System.Drawing;
using System.ComponentModel;

namespace System_do_zarzadzania_obslugi_sprzedazy
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        List<Company> companies = new List<Company>();
        List<Seller> sellers = new List<Seller>();
        List<BaseInvoice> baseInvoices = new List<BaseInvoice>();
        List<Invoice> invoices = new List<Invoice>();

        //private bool invoiceOpen=false;


        public MainWindow()
        {
            InitializeComponent();
            LoadInvoicesList();
        }

        private void LoadCompaniesList()
        {
            companies = SQLiteDataAccess.LoadUsers();
            WiredUpCompaniesList();
        }

        private void Test_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("dziala");
        }

        private void WiredUpCompaniesList()
        {
            CompanyDataGrid.ItemsSource = null;
            CompanyDataGrid.ItemsSource = companies;

        }

        private void WiredUpSellersList()
        {
            CompanyDataGrid.ItemsSource = null;
            CompanyDataGrid.ItemsSource = sellers;
        }

        private void WiredUpInvoicesList()
        {
            CompanyDataGrid.ItemsSource = null;
            CompanyDataGrid.ItemsSource = baseInvoices;
        }

        private void LoadInvoicesList()
        {
            invoices = SQLiteDataAccess.LoadInvoices();
            baseInvoices = invoices.ConvertAll(x => (BaseInvoice)x);
            WiredUpInvoicesList();
        }

        private void LoadSellersList()
        {
            sellers = SQLiteDataAccess.LoadSellers();
            WiredUpSellersList();
      
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadCompaniesList();
        }

        private void Add_User(object sender, RoutedEventArgs e)
        { 
                addUser addUser = new addUser();
                addUser.Show();
                addUser.Closed += (s, eventarg) =>
                {
                    LoadCompaniesList();
                };
        }

        private void Remove_User(object sender, RoutedEventArgs e)
        {
           if (CompanyDataGrid.SelectedItem != null)
            {
              SQLiteDataAccess.DeleteUsers((Company)CompanyDataGrid.SelectedItem);
              companies.Remove((Company)CompanyDataGrid.SelectedItem);
              WiredUpCompaniesList();
            }
        }

        private void Invoice_Open(object sender, RoutedEventArgs e)
        {
            LoadInvoicesList();
        }

        private void Storage_Open(object sender, RoutedEventArgs e)
        {
            CompanyDataGrid.ItemsSource = null;
        }

        private void Settlements_Open(object sender, RoutedEventArgs e)
        {
            CompanyDataGrid.ItemsSource = null;
        }

        private void Contractors_Open(object sender, RoutedEventArgs e)
        {
            LoadSellersList();
        }

        private void Statments_Open(object sender, RoutedEventArgs e)
        {
            CompanyDataGrid.ItemsSource = null;
        }

        private void VATRegister_Open(object sender, RoutedEventArgs e)
        {
            CompanyDataGrid.ItemsSource = null;
        }

        private void Print_Open(object sender, RoutedEventArgs e)
        {

        }

        private void Search_Open(object sender, RoutedEventArgs e)
        {

        }

        private void CompanyDataGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyDescriptor is PropertyDescriptor descriptor)
            {
                e.Column.Header = descriptor.DisplayName ?? descriptor.Name;
            }
        }
    }
}