using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System_do_zarzadzania_obslugi_sprzedazy.Classes
{
    public class InvoiceCorrection
    {
        private int correctionID;
        private string correctionNumber;
        private string correctionDate;
        private string correctionReason;
        private int invoiceConnection;
        private int correctionConnection;

        [DisplayName("ID korekty")]
        public int CorrectionID
        {
            get { return correctionID; }
            set { correctionID = value; }
        }

        [DisplayName("Numer korekty")]
        public string CorrectionNumber
        {
            get { return correctionNumber; }
            set { correctionNumber = value; }
        }

        [DisplayName("Data korekty")]
        public string CorrectionDate
        {
            get { return correctionDate; }
            set { correctionDate = value; }
        }

        [DisplayName("Powód korekty")]
        public string CorrectionReason
        {
            get { return correctionReason; }
            set { correctionReason = value; }
        }

        public int InvoiceConnection
        {
            get { return invoiceConnection; }
            set { invoiceConnection = value; }
        }

        public int CorrectionConnection
        {
            get { return correctionConnection; }
            set { correctionConnection = value; }
        }

        public InvoiceCorrection()
        {

        }

        public InvoiceCorrection(int correctionID, string correctionNumber, string correctionDate, string correctionReason, int invoiceConnection)
        {
            CorrectionID = correctionID;
            CorrectionNumber = correctionNumber;
            CorrectionDate = correctionDate;
            CorrectionReason = correctionReason;
            InvoiceConnection = invoiceConnection;
        }

        public InvoiceCorrection(int correctionID, string correctionNumber, string correctionDate, string correctionReason, int invoiceConnection, int correctionConnection)
        {
            CorrectionID = correctionID;
            CorrectionNumber = correctionNumber;
            CorrectionDate = correctionDate;
            CorrectionReason = correctionReason;
            InvoiceConnection = invoiceConnection;
            CorrectionConnection = correctionConnection;
        }



    }
}
