using System;
using System.Collections.Generic;
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

        public int CorrectionID
        {
            get { return correctionID; }
            set { correctionID = value; }
        }

        public string CorrectionNumber
        {
            get { return correctionNumber; }
            set { correctionNumber = value; }
        }

        public string CorrectionDate
        {
            get { return correctionDate; }
            set { correctionDate = value; }
        }

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
