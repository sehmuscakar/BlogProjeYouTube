using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
   public class ProjeContext:IdentityDbContext<AppUser,AppRole,Guid,AppUserClaim,AppUserRole,AppUserLogin,AppRoleClaim,AppUserToken>// burda overlowlarımızı eklememiz lazım bunalr en zor olan kısmı 3 yönetimi var bu en zoru, bu averlowların sırlaması önemli 
    {
        public ProjeContext()
        {

        }


        public ProjeContext(DbContextOptions<ProjeContext> options) : base(options)      
        { 
        
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)// overide on yaz gelir
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-PBFD0LU;  database=OtoGalleriProje; integrated security=true; TrustServerCertificate=true");
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Article>().Property(x=>x.Title).HasMaxLength(150) bunları map sınıflarında tanımla burda olması çok tavsiye edilmiyor
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); // mapping kalsöründe tanımladıklarımızı tek tek tanımlamak yerine tek seferde tanımlıyoruz

            base.OnModelCreating(modelBuilder);// bu yapı migrration oluştuğunda sağlıklı olabilmesi için  
            //modelBuilder.Entity<AppUser>(b => // map lemeye koyamadan burada da kullanabilirsin ama ayırmak daha sağlıklı 
            //{
            //    // Primary key
            //    b.HasKey(u => u.Id);

            //    // Indexes for "normalized" username and email, to allow efficient lookups
            //    b.HasIndex(u => u.NormalizedUserName).HasName("UserNameIndex").IsUnique();
            //    b.HasIndex(u => u.NormalizedEmail).HasName("EmailIndex");

            //    // Maps to the AspNetUsers table
            //    b.ToTable("AspNetUsers");

            //    // A concurrency token for use with the optimistic concurrency checking
            //    b.Property(u => u.ConcurrencyStamp).IsConcurrencyToken();

            //    // Limit the size of columns to use efficient database types
            //    b.Property(u => u.UserName).HasMaxLength(256);
            //    b.Property(u => u.NormalizedUserName).HasMaxLength(256);
            //    b.Property(u => u.Email).HasMaxLength(256);
            //    b.Property(u => u.NormalizedEmail).HasMaxLength(256);

            //    // The relationships between User and other entity types
            //    // Note that these relationships are configured with no navigation properties

            //    // Each User can have many UserClaims
            //    b.HasMany<AppUserClaim>().WithOne().HasForeignKey(uc => uc.UserId).IsRequired();

            //    // Each User can have many UserLogins
            //    b.HasMany<AppUserLogin>().WithOne().HasForeignKey(ul => ul.UserId).IsRequired();

            //    // Each User can have many UserTokens
            //    b.HasMany<AppUserToken>().WithOne().HasForeignKey(ut => ut.UserId).IsRequired();

            //    // Each User can have many entries in the UserRole join table
            //    b.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.UserId).IsRequired();
            //});

        }


    }
}
