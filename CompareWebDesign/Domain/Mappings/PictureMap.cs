using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompareWebDesign.Domain.Mappings
{
    public class PictureMap : IEntityTypeConfiguration<Picture>
    {
        public void Configure(EntityTypeBuilder<Picture> builder)
        {
            builder.ToTable("Pictures");
            builder.Property(p => p.Filename).HasMaxLength(1000);
            builder.Property(p => p.MimeType).HasMaxLength(1000);
            builder.Property(p => p.PictureBinary).HasMaxLength(1000000);

            builder.HasOne(p => p.ProjectItem).WithMany(p => p.Pictures).HasForeignKey(p => p.ProjectItemId);
        }
    }
}
