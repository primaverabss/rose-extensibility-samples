using System;
using System.Collections.Generic;
using System.Text;

namespace RoseSample.Entities
{
    /// <summary>
    /// The invoice entity. Have only a set of properties.
    /// More information where https://apidoc.rose.primaverabss.com/
    /// </summary>
    internal class Invoice
    {
        #region Public Properties

        public Guid Id
        {
            get;
            set;
        }

        public string DocumentType
        {
            get;
            set;
        }

        public string DeliveryMode
        {
            get;
            set;
        }
        public string Serie
        {
            get;
            set;
        }

        public int SeriesNumber
        {
            get;
            set;
        }

        public string Company
        {
            get;
            set;
        }
        public string AccountingPartyName
        {
            get;
            set;
        }
        public string DueDate
        {
            get;
            set;
        }
        public string NaturalKey
        {
            get;
            set;
        }
        #endregion
    }
}
