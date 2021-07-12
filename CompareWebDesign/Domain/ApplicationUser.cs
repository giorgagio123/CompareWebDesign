using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompareWebDesign.Domain
{
    public class ApplicationUser : IdentityUser
    {
        private ICollection<Project> _project;

        public virtual ICollection<Project> Projects
        {
            get { return _project ?? (_project = new List<Project>()); }
            protected set { _project = value; }
        }
    }
}
