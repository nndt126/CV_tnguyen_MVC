using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tnguyenResume.Bussiness.Interface;
using tnguyenResume.Bussiness.Model;

namespace tnguyenResume.Bussiness.DAL
{
    public class TestimonialDAL : ITestimonialDAL
    {
        private readonly ItnguyenResumeDbContext _itnguyenResumeDbContext;

        public TestimonialDAL(ItnguyenResumeDbContext itnguyenResumeDbContext)
        {
            _itnguyenResumeDbContext = itnguyenResumeDbContext;
        }

        public IEnumerable<Testimonial> GetTestimonial(int number)
        {
            var userId = _itnguyenResumeDbContext.Testimonials.FirstOrDefault().ID_User ?? Guid.Empty;
            var result = new List<Testimonial>();
            if (number > 0)
            {
                result = _itnguyenResumeDbContext.Testimonials.Where(t => t.ID_User == userId).Take(number).ToList();
            }
            else
                result = _itnguyenResumeDbContext.Testimonials.Where(t => t.ID_User == userId).ToList();

            return result ?? null;
        }

        public IEnumerable<Testimonial> GetWorkById(Guid Id)
        {
            var result = _itnguyenResumeDbContext.Testimonials.Where(n => n.ID == Id);
            return result ?? null;
        }
    }
}
