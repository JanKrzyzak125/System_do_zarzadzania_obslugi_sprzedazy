using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_do_zarzadzania_obslugi_sprzedazy
{
    class Seller
    {
        private int idSeller;

        private string name;

        private string surname;

        private string city;

        private string street;

        private string numberPhone;

        public int IdSeller
        {
            get
            {
                return idSeller;
            }
            set
            {
                idSeller = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public string Surname
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
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

        public string NumberPhone
        {
            get
            {
                return numberPhone;
            }
            set
            {
                numberPhone = value;
            }
        }

        public Seller()
        {

        }

        public Seller(int idSeller, string name, string surname, string city, string street, string numberPhone)
        {
            IdSeller = idSeller;
            Name = name;
            Surname = surname;
            City = city;
            Street = street;
            NumberPhone = numberPhone;
        }

        public Seller(string name, string surname, string city, string street, string numberPhone)
        {
            Name = name;
            Surname = surname;
            City = city;
            Street = street;
            NumberPhone = numberPhone;
        }
    }
}
