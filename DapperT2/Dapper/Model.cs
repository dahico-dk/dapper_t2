using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DapperT2.Dapper
{
    public class Address
    {
        public int AddressID { get; set; }
        public int UserID { get; set; }
        public string AddressType { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    public class AracModel
    {
        public string MODEL { get; set; }

        public AracModel()
        {
           

        }
        public AracModel(string MODEL)
        {
            this.MODEL = MODEL;
          
        }
    }
    public class CARILISTE
    {
        public string belge_no { get; set; }
        public float borc { get; set; }
        public DateTime vade_tarihi { get; set; }
        public DateTime tarih { get; set; }
        
        public string aciklama { get; set; }
        public float alacak { get; set; }
        public float bakiye { get; set; }

        public CARILISTE()
        {

        }

        public CARILISTE(string BELGE_NO,DateTime VADE_TARIHI, DateTime TARIH,float BORC, float ALACAK, string ACIKLAMA, float BAKIYE  )
        {
            this.belge_no = BELGE_NO;
            this.vade_tarihi = VADE_TARIHI;
            this.tarih = TARIH;
            this.borc = BORC;
            this.alacak = ALACAK;
            this.aciklama = ACIKLAMA;
            this.bakiye = BAKIYE;

        }
    }
    public class User
    {
        public User()
        {
            this.Address = new List<Address>();
        }

        public int UserID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public List<Address> Address { get; set; }
    }
}