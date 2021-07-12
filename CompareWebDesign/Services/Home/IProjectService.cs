using CompareWebDesign.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompareWebDesign.Services.Home
{
    public interface IProjectService
    {
        IList<Project> GetCustomerProjects();
        IList<ProjectItem> GetCustomerProjectItemsByProjectId(int projectId);
        void InsertProject(Project project);
        void InsertProjectItem(ProjectItem projectItem);
        ProjectItem GetProjectItemById(int id);

        void InsertComment(Comment comment);
    }
}
