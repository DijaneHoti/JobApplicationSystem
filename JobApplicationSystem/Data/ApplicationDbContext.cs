using JobApplicationSystem.Models;
using JobApplicationSystem.Models.Dto;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobApplicationSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
          : base(options)
        {
        }
        public DbSet<Company>Companies { get; set; }
        public DbSet<Job>Jobs { get; set; }

        public DbSet<Jobseeker>Jobseekers { get; set; }

        public DbSet<JobPosting>JobPostings { get; set; }

        public DbSet<Employer> Employers { get; set; }

        public DbSet<HiringManager > HiringManagers { get; set; }


    }
}
