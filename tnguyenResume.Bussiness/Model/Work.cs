namespace tnguyenResume.Bussiness.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Work
    {
        public Guid ID { get; set; }

        [StringLength(500)]
        public string WorksTitle { get; set; }

        [Column(TypeName = "ntext")]
        public string WorksInfo { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? WorksDate { get; set; }

        [Column(TypeName = "ntext")]
        public string WorksDetail { get; set; }

        public Guid? ID_User { get; set; }
    }
}
