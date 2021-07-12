using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompareWebDesign.Domain
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }

        //user
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        
        //project items
        private ICollection<ProjectItem> _projectItem;
        public virtual ICollection<ProjectItem> ProjectItems
        {
            get { return _projectItem ?? (_projectItem = new List<ProjectItem>()); }
            protected set { _projectItem = value; }
        }
    }
}
