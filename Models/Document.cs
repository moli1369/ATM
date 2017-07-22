namespace ATM.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Document
    {
        public virtual DateDimension DateDimension { get; set; }

        public virtual DateDimension DateDimension1 { get; set; }

        public virtual Person Person { get; set; }
    }
}
