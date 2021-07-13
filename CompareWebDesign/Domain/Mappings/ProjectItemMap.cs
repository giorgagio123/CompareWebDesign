using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompareWebDesign.Domain.Mappings
{
    public class ProjectItemMap : IEntityTypeConfiguration<ProjectItem>
    {
        public void Configure(EntityTypeBuilder<ProjectItem> builder)
        {
            builder.HasOne(p => p.Project).WithMany(p => p.ProjectItems).HasForeignKey(x => x.ProjectId);
            builder.Ignore(x => x.CropSettingsModel);
        }
    }
}
