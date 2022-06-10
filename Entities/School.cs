using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SqlRawSpTestApp.Entities
{
    public class School : IEntityTypeConfiguration<School>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.Property(x => x.Id)
                .IsRequired()
                .UseIdentityColumn(1, 1);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(75);
        }
    }
}
