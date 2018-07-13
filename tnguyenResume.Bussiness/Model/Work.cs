namespace tnguyenResume.Bussiness.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Work
    {
        public int ID { get; set; }

        [StringLength(500)]
        public string WorksTitle { get; set; }

        [Column(TypeName = "ntext")]
        public string WorksInfo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? WorksDate { get; set; }

        [Column(TypeName = "ntext")]
        public string WorksDetail { get; set; }

        public int? ID_User { get; set; }

        public virtual Information Information { get; set; }
    }
}
