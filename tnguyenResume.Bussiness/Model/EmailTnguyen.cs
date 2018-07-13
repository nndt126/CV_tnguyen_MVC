namespace tnguyenResume.Bussiness.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmailTnguyen")]
    public partial class EmailTnguyen
    {
        public int ID { get; set; }

        [StringLength(350)]
        public string Name { get; set; }

        [StringLength(350)]
        public string Email { get; set; }

        [StringLength(500)]
        public string Subject { get; set; }

        [StringLength(500)]
        public string Message { get; set; }
    }
}
