namespace tnguyenResume.Bussiness.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Testimonial
    {
        public Guid ID { get; set; }

        [StringLength(500)]
        public string TestimonialsInfo { get; set; }

        [StringLength(500)]
        public string TestimonialsAuthor { get; set; }

        public Guid? ID_User { get; set; }
    }
}
