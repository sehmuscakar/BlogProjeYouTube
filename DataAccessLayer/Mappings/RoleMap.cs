using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Mappings
{
    public class RoleMap : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
            // Primary key
            builder.HasKey(r => r.Id);

            // Index for "normalized" role name to allow efficient lookups
            builder.HasIndex(r => r.NormalizedName).HasName("RoleNameIndex").IsUnique();

            // Maps to the AspNetRoles table
            builder.ToTable("AspNetRoles");

            // A concurrency token for use with the optimistic concurrency checking
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            // Limit the size of columns to use efficient database types
            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.NormalizedName).HasMaxLength(256);

            // The relationships between Role and other entity types
            // Note that these relationships are configured with no navigation properties

            // Each Role can have many entries in the UserRole join table
            builder.HasMany<AppUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();

            // Each Role can have many associated RoleClaims
            builder.HasMany<AppRoleClaim>().WithOne().HasForeignKey(rc => rc.RoleId).IsRequired();

            // özeleştirme yaptığımız kısım burda 

            //builder.HasData(new AppRole//burda roller biz burdan ekliyoruz özeleştime sayesinde 
            //{
            //    Id = Guid.Parse("5C13A2D0 - 7ABB - 46E0 - 8A6C - 7D14CA94EFA8"),
            //    Name="Superadmin",
            //    NormalizedName="SUPERADMİN",
            //    ConcurrencyStamp=Guid.NewGuid().ToString(),//ROLE ÜZERİNDE aynı rool üzerinde işlemler yaparsa hangi önce onu alıyor gibi bişey 
            //},
            
            //new AppRole
            //{
            //    Id=Guid.Parse("D6F827F2-DC33-4345-9AF9-A8835EE3CC3D"),
            //    Name="Admin",
            //    NormalizedName="ADMİN",
            //    ConcurrencyStamp= Guid.NewGuid().ToString(),
            //},


            //   new AppRole
            //   {
            //       Id = Guid.Parse("31A3814C-7982-4159-A355-12444CFCD061"),
            //       Name = "User",
            //       NormalizedName = "USER",
            //       ConcurrencyStamp = Guid.NewGuid().ToString(),
            //   }
            //);

        }
    }
}
