using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompareWebDesign.Domain
{
    public class Picture : BaseEntity
    {
        public byte[] PictureBinary { get; set; }

        public string MimeType { get; set; }

        public string PictureSrc { get; set; }

        public bool IsLiveImage { get; set; }
        
        public string Filename { get; set; }

        public string SeoFilename { get; set; }

        public int ProjectItemId { get; set; }

        public ProjectItem ProjectItem { get; set; }
    }
}
