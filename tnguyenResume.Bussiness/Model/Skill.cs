namespace tnguyenResume.Bussiness.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Skill
    {
        public Guid ID { get; set; }

        [Column(TypeName = "ntext")]
        public string SkillsTitle { get; set; }

        [StringLength(50)]
        public string SkillValue { get; set; }

        public Guid? ID_User { get; set; }
    }
}
