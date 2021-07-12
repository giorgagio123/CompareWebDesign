using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompareWebDesign.Domain
{
    public class Comment : BaseEntity
    {
        public string LiveComment { get; set; }

        public int SelectionId { get; set; }

        public int ProjectItemId { get; set; }

        public ProjectItem ProjectItem { get;set;}
    }
}
