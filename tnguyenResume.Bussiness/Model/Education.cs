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
        public int ID { get; set; }

        [StringLength(500)]
        public string EducationTitle { get; set; }

        [Column(TypeName = "ntext")]
        public string EducationInfo { get; set; }

        [Column(TypeName = "date")]
        public DateTime? EducationDate { get; set; }

        [Column(TypeName = "ntext")]
        public string EducationDetail { get; set; }

        public int? ID_User { get; set; }

        public virtual Information Information { get; set; }
    }
}
