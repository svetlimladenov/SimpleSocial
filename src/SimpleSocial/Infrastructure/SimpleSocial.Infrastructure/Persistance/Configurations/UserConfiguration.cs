using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleSocial.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSocial.Infrastructure.Persistance.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "identity");

            builder.Property(x => x.Username).HasColumnName("UserName");
        }
    }
}
