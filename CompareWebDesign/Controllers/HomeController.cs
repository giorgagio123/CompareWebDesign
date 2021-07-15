using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CompareWebDesign.Models;
using CompareWebDesign.Services.Home;
using CompareWebDesign.Domain;
using CompareWebDesign.Services.Account;
using CompareWebDesign.Models.Home;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Drawing.Drawing2D;
using System.Drawing;
using Newtonsoft.Json;
using CompareWebDesign.Services;

namespace CompareWebDesign.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IAccountService _accountService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly WebHelper _webHelper;

        public HomeController(IProjectService projectService, IAccountService accountService, IHostingEnvironment hostingEnvironment, WebHelper webHelper)
        {
            _projectService = projectService;
            _accountService = accountService;
            _webHelper = webHelper;
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult ProjectList()
        {
            var projects = _projectService.GetCustomerProjects();

            return View(projects);
        }

        public IActionResult AddProject()
        {
            var project = new Project();
            return View(project);
        }

        [HttpPost]
        public IActionResult AddProject(Project model)
        {
            model.UserId = _accountService.GetUserId();
            _projectService.InsertProject(model);
            //redirect to projectlist
            return View();
        }

        public IActionResult ProjectItemList(int id)
        {
            var projectItems = _projectService.GetCustomerProjectItemsByProjectId(id);
            ViewBag.ProjectId = id;
            return View(projectItems);
        }

        public IActionResult AddProjectItem(int id)
        {
            var projectItem = new ProjectItemModel() { ProjectId = id };

            return View(projectItem);
        }

        [HttpPost]
        public IActionResult AddProjectItem(ProjectItemModel model)
        {
            var projectItem = new ProjectItem
            {
                ProjectId = model.ProjectId,
                Name = model.Name,
                CropSettings = model.CropSettings
            };

            var designImage = new Picture
            {
                Filename = $"{Guid.NewGuid()}.jpg",
                IsLiveImage = false,
                PictureSrc = model.DesignImageSrc            };

            //designImage.PictureSrc = Path.Combine(_webHelper.GetStoreLocation(), "img", designImage.Filename);

            var liveImage = new Picture
            {
                IsLiveImage = true,
                PictureSrc = model.LiveImageSrc,
                PictureBinary = Convert.FromBase64String(model.LiveImageBuffer),
                Filename = $"{Guid.NewGuid()}.jpg"
            };

            var path = GetPictureWebRootPath(designImage.Filename);

            string GetPictureWebRootPath(string fileName)
            {
                return Path.Combine(_hostingEnvironment.WebRootPath, "img", fileName);
            }

            using (var stream = new MemoryStream(Convert.FromBase64String(model.LiveImageBuffer)))
            using (Image OriginalImage = Image.FromStream(stream))
            {
                if( model.CropSettings != null)
                {
                    var cropSettings = JsonConvert.DeserializeObject<List<CropSettingsModel>>(model.CropSettings);
                    foreach (var item in cropSettings)
                    {
                        using (Bitmap bmp = new Bitmap((int)item.width, (int)item.height))
                        {
                            bmp.SetResolution(OriginalImage.HorizontalResolution, OriginalImage.VerticalResolution);

                            using (Graphics Graphic = Graphics.FromImage(bmp))
                            {
                                Graphic.SmoothingMode = SmoothingMode.AntiAlias;

                                Graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;

                                Graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;

                                Graphic.DrawImage(OriginalImage, new Rectangle(0, 0, (int)item.width, (int)item.height), (int)item.x, (int)item.y, (int)item.width, (int)item.height, GraphicsUnit.Pixel);

                                MemoryStream ms = new MemoryStream();

                                bmp.Save(ms, OriginalImage.RawFormat);

                                path = GetPictureWebRootPath($"{Guid.NewGuid()}.jpg");

                                System.IO.File.WriteAllBytes(path, ms.ToArray());
                            }
                        }
                    }
                }
                
            }

            projectItem.Pictures.Add(liveImage);
            projectItem.Pictures.Add(designImage);

            _projectService.InsertProjectItem(projectItem);

            return Json(new { projectItemId = projectItem.Id });
        }

        [HttpPost]
        public IActionResult AddComment(ProjectItemModel model)
        {
            _projectService.InsertComment(new Comment
            {
                LiveComment = model.CommentsModel.Comment,
                ProjectItemId = model.CommentsModel.ProjectItemId,
                SelectionId = model.CommentsModel.SelectionId
            });

            return NoContent();
        }

        public IActionResult ProjectItemDetails(int id)
        {
            var model = _projectService.GetProjectItemById(id);

            model.CropSettingsModel = JsonConvert.DeserializeObject<List<CropSettingsModel>>(model.CropSettings);

            return View(model);
        }



        //[HttpPost]
        //public IActionResult ProjectItemDetails(ProjectItemDetailsModel model)
        //{
        //    //var projectItem = new ProjectItem();

        //    var xui = model.DesignImage.GetDownloadBits();


        //    return View();
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        
    }

    public static class Extensions
    {
        public static byte[] GetDownloadBits(this IFormFile file)
        {
            using (var fileStream = file.OpenReadStream())
            using (var ms = new MemoryStream())
            {
                fileStream.CopyTo(ms);
                var fileBytes = ms.ToArray();
                return fileBytes;
            }
        }
    }

    //public class CropSettingsModel 
    //{
    //    public int id { get; set; }
    //    public int x { get; set; }

    //    public int y { get; set; }
    //    public int z { get; set; }
    //    public int width { get; set; }
    //    public int height { get; set; }
    //}
}
