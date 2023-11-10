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
    public class UserRoleMap : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            // Primary key
            builder.HasKey(r => new { r.UserId, r.RoleId });

            // Maps to the AspNetUserRoles table
            builder.ToTable("AspNetUserRoles");



            //builder.HasData(new AppUserRole
            //{
            //    UserId= Guid.Parse("4A54EC97-53E1-4D9F-9632-287096095140"),
            //    RoleId= Guid.Parse("5C13A2D0 - 7ABB - 46E0 - 8A6C - 7D14CA94EFA8"),
            //},

            //new AppUserRole
            //{
            //    UserId = Guid.Parse("B67E9F35-AB10-4B0B-B947-A08FFF630C4A"),
            //    RoleId= Guid.Parse("D6F827F2-DC33-4345-9AF9-A8835EE3CC3D"),
            //});

        }
    }
}
