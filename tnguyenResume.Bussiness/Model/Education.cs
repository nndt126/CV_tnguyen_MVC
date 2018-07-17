namespace tnguyenResume.Bussiness.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Education")]
    public partial class Education
    {
        public Guid ID { get; set; }

        [StringLength(500)]
        public string EducationTitle { get; set; }

        [Column(TypeName = "ntext")]
        public string EducationInfo { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/M/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EducationDate { get; set; }

        [Column(TypeName = "ntext")]
        public string EducationDetail { get; set; }

        public Guid? ID_User { get; set; }
    }
}
