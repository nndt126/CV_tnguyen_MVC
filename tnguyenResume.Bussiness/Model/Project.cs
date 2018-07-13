namespace tnguyenResume.Bussiness.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Project")]
    public partial class Project
    {
        public Guid ID { get; set; }

        [StringLength(500)]
        public string ProjectImage { get; set; }

        [StringLength(200)]
        public string ProjectTitle { get; set; }

        [StringLength(200)]
        public string ProjectInfo { get; set; }

        [StringLength(500)]
        public string ProjectDetail { get; set; }

        [StringLength(200)]
        public string ProjectJob { get; set; }

        [StringLength(500)]
        public string ProjectURL { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ProjectTime { get; set; }

        public Guid? ID_User { get; set; }
    }
}
