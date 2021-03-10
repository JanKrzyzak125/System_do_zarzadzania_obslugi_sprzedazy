using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_do_zarzadzania_obslugi_sprzedazy
{
    class Company
    {
        public Company(string companyName, string nip, string city, string street, string phoneNumber, string email)
        {
            //SetCompanyID(this.companyID);
            SetCompanyName(this.companyName);
            SetNip(this.nip);
            SetCity(this.city);
            SetStreet(this.street);
            SetPhoneNumber(this.phoneNumber);
            SetEmail(this.email);
        }
        private int companyID;
        
        private string companyName;
       
        private string nip;
        
        private string city;
        
        private string street;
        
        private string phoneNumber;
        
        private string email;
        
        public int GetCompanyID()
        {
            return companyID;
        }
        public void SetCompanyID(int id)
        {
            this.companyID = id;
        }
         public string GetCompanyName()
        {
            return companyName;
        }
        public void SetCompanyName(string name)
        {
            this.companyName = name;
        }
        public string GetNip()
        {
            return nip;
        }
        public void SetNip(string nip)
        {
            this.nip = nip;
        }
        public string GetCity()
        {
            return city;
        }
        public void SetCity(string city)
        {
            this.city = city;
        }
        public string GetStreet()
        {
            return street;
        }
        public void SetStreet(string street)
        {
            this.street = street;
        }
        public string GetPhoneNumber()
        {
            return phoneNumber;
        }
        public void SetPhoneNumber(string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
        }
        public string GetEmail()
        {
            return email;
        }
        public void SetEmail(string email)
        {
            this.email = email;
        }
        // public int CompanyID { get; set; }
        // public string CompanyName { get; set; }
        // public string Nip { get; set; }
        // public string City { get; set; }
        // public string Street { get; set; }
        // public string PhoneNumber { get; set; }
        // public string Email {get;set; }
    }
}
