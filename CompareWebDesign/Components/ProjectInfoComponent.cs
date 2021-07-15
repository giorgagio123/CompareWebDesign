using CompareWebDesign.Services.Home;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompareWebDesign.Components
{
    [ViewComponent(Name = "ProjectInfo")]
    public class ProjectInfoComponent : ViewComponent
    {
        private readonly IProjectService _projectService;

        public ProjectInfoComponent(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public IViewComponentResult Invoke()
        {
            //var model = _productModelFactory.PrepreProductFilterModel();

            return View();
        }
    }
}
