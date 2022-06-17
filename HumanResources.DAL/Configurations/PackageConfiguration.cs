using HumanResources.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.DAL.Configurations
{
    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            //builder.HasKey(x => x.Id);
            //builder.HasIndex(x => x.Name).IsUnique();
            //builder.Property(x => x.Name).IsRequired().HasMaxLength(20);
            //builder.Property(x => x.Description).IsRequired().HasMaxLength(200);
            //builder.Property(x => x.Cost).IsRequired();
            builder.HasKey(x => x.Id);

        }
    }
}
