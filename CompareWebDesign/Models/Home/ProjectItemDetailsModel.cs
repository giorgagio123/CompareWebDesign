using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompareWebDesign.Models.Home
{
    public class ProjectItemDetailsModel
    {
        public int ProjectItemId { get; set; }
        public IFormFile DesignImage { get; set; }
        public string LiveImageSrc { get; set; }
    }
}
