using CompareWebDesign.Models.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompareWebDesign.Domain
{
    public class ProjectItem : BaseEntity
    {
        public ProjectItem()
        {
            CropSettingsModel = new List<CropSettingsModel>();
        }

        public string Name { get; set; }


        //project
        public int ProjectId { get; set; }
        public string CropSettings { get; set; }

        public List<CropSettingsModel> CropSettingsModel { get;set;}
        public virtual Project Project { get; set; }

        //pictures
        private ICollection<Picture> _picture;
        public virtual ICollection<Picture> Pictures
        {
            get { return _picture ?? (_picture = new List<Picture>()); }
            protected set { _picture = value; }
        }

        //comments
        private ICollection<Comment> _comment;
        public virtual ICollection<Comment> Comments
        {
            get { return _comment ?? (_comment = new List<Comment>()); }
            protected set { _comment = value; }
        }
    }
}
