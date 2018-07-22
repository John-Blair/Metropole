using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Metropole.Helpers
    {


    public class MetropoleContext : IdentityDbContext<ApplicationUser>
    {

        public MetropoleContext() : base("DefaultConnection", throwIfV1Schema: false)
        { }
        public DbSet<Address> Addresses { get; set; }


        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Response> Responses { get; set; }
        public DbSet<Answer> Answers { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>()
                        .HasRequired(x => x.Question)
                        .WithMany()
                        .HasForeignKey(x => x.QuestionId)
                        .WillCascadeOnDelete(false);
            Database.SetInitializer<MetropoleContext>(null);
            base.OnModelCreating(modelBuilder);
        }

        public static MetropoleContext Create()
        {
            return new MetropoleContext();
        }
    }
}
