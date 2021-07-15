using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompareWebDesign.Models.Home
{
    public class ProjectItemModel
    {
        public ProjectItemModel()
        {
            CommentsModel = new CommentsModel();
        }

        public int ProjectId { get; set; }
        public string Name { get; set; }

        public IFormFile DesignImage { get; set; }
        public string LiveImageSrc { get; set; }
        public string DesignImageSrc { get; set; }

        public string CropSettings { get; set; }

        public string LiveImageBuffer { get; set; }
        public string DesignImageBuffer { get; set; }

        public CommentsModel CommentsModel { get; set; }
    }

    public class CropSettingsModel
    {
        public decimal x { get; set; }
        public decimal y { get; set; }
        public decimal z { get; set; }
        public decimal width { get; set; }
        public decimal height { get; set; }
    }

    public class CommentsModel
    {
        public int SelectionId { get; set; }
        public int ProjectItemId { get; set; }

        public string Comment { get; set; }


    }
}
