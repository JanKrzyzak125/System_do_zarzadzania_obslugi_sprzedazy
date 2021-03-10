using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_do_zarzadzania_obslugi_sprzedazy
{
 public class Company
    {
        private int companyID;

        private string companyName;

        private string nip;

        private string city;

        private string street;

        private string phoneNumber;

        private string email;

        public int CompanyID
        {
            get
            {
                return companyID;
            }
            set
            {
                companyID = value;
            }
        }

        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                companyName = value;
            }
        }

        public string Nip
        {
            get
            {
                return nip;
            }
            set
            {
                nip = value;
            }
        }

        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }

        public string Street
        {
            get
            {
                return street;
            }
            set
            {
                street = value;
            }
        }

        public string PhoneNumber
        {
            get
            {
                return phoneNumber;
            }
            set
            {
                phoneNumber = value;
            }
        }

        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        public Company()
        {

        }

        public Company(int companyID, string companyName, string nip, string city, string street, string phoneNumber, string email)
        {
            CompanyID = companyID;
            CompanyName = companyName;
            Nip = nip;
            City = city;
            Street = street;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public Company(string companyName, string nip, string city, string street, string phoneNumber, string email)
        {
            CompanyName = companyName;
            Nip = nip;
            City = city;
            Street = street;
            PhoneNumber = phoneNumber;
            Email = email;
        }

    }
}
