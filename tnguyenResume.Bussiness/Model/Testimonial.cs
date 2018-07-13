namespace tnguyenResume.Bussiness.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Testimonial
    {
        public int ID { get; set; }

        [StringLength(500)]
        public string TestimonialsInfo { get; set; }

        [StringLength(500)]
        public string TestimonialsAuthor { get; set; }

        public int? ID_User { get; set; }

        public virtual Information Information { get; set; }
    }
}
