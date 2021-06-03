using System.ComponentModel;

namespace System_do_zarzadzania_obslugi_sprzedazy.Classes
{
    /// <summary>
    /// Klasa InvoiceCorrection, która zawiera informacje odnośnie korekt faktur
    /// </summary>
    public class InvoiceCorrection
    {
        private int correctionID;
        private string correctionNumber;
        private string correctionDate;
        private string correctionReason;
        private int invoiceConnection;
        private int correctionConnection;

        /// <summary>
        /// ID korekty
        /// </summary>
        [DisplayName("ID korekty")]
        public int CorrectionID
        {
            get { return correctionID; }
            set { correctionID = value; }
        }

        /// <summary>
        /// Numer Korekty
        /// </summary>
        [DisplayName("Numer korekty")]
        public string CorrectionNumber
        {
            get { return correctionNumber; }
            set { correctionNumber = value; }
        }

        /// <summary>
        /// Data wykonanej korekty
        /// </summary>
        [DisplayName("Data korekty")]
        public string CorrectionDate
        {
            get { return correctionDate; }
            set { correctionDate = value; }
        }

        /// <summary>
        /// Powód korekty
        /// </summary>
        [DisplayName("Powód korekty")]
        public string CorrectionReason
        {
            get { return correctionReason; }
            set { correctionReason = value; }
        }

        /// <summary>
        /// Do połączenia podstawowej faktury z faktura korekta 
        /// </summary>
        public int InvoiceConnection
        {
            get { return invoiceConnection; }
            set { invoiceConnection = value; }
        }

        /// <summary>
        /// Do połączenie faktury korekty z kolejna korekta
        /// </summary>
        public int CorrectionConnection
        {
            get { return correctionConnection; }
            set { correctionConnection = value; }
        }

        /// <summary>
        /// Konstruktor domyślny klasy 
        /// </summary>
        public InvoiceCorrection()
        {

        }

        /// <summary>
        /// Konstruktor przeładowany danymi
        /// </summary>
        /// <param name="correctionID">ID Korekty</param>
        /// <param name="correctionNumber">Numer korekty</param>
        /// <param name="correctionDate">Data korekty</param>
        /// <param name="correctionReason">Powód korekty</param>
        /// <param name="invoiceConnection">Połączenie z podstawową wersja faktury</param>
        public InvoiceCorrection(int correctionID, string correctionNumber, string correctionDate, string correctionReason, int invoiceConnection)
        {
            CorrectionID = correctionID;
            CorrectionNumber = correctionNumber;
            CorrectionDate = correctionDate;
            CorrectionReason = correctionReason;
            InvoiceConnection = invoiceConnection;
        }

        /// <summary>
        /// Konstruktor przeładowany danymi
        /// </summary>
        /// <param name="correctionID">ID Korekty</param>
        /// <param name="correctionNumber">Numer korekty</param>
        /// <param name="correctionDate">Data korekty</param>
        /// <param name="correctionReason">Powód korekty</param>
        /// <param name="invoiceConnection">Połączenie z podstawową wersja faktury</param>
        /// <param name="correctionConnection">Połączenie z korekta faktury</param>
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
