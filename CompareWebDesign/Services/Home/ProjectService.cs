using CompareWebDesign.Domain;
using CompareWebDesign.Services.Account;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CompareWebDesign.Services.Home
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project> _projectRepository;
        private readonly IRepository<ProjectItem> _projectItemRepository;
        private readonly IRepository<Comment> _commentRepository;
        private readonly IAccountService _accountService;

        public ProjectService(IRepository<Project> projectRepository, IAccountService accountService,
            IRepository<ProjectItem> projectItemRepository, IRepository<Comment> commentRepository)
        {
            _projectRepository = projectRepository;
            _accountService = accountService;
            _projectItemRepository = projectItemRepository;
            _commentRepository = commentRepository;
        }

        public IList<Project> GetCustomerProjects()
        {
            return _projectRepository.Table.Where(p => p.UserId == _accountService.GetUserId()).Include(p => p.ProjectItems).AsNoTracking().ToList();
        }

        public IList<ProjectItem> GetCustomerProjectItemsByProjectId(int projectId)
        {
            return _projectItemRepository.Table.Where(pi => pi.ProjectId == projectId).Include(x => x.Project).Include(x => x.Comments).Include(x => x.Pictures).AsNoTracking().ToList();
        }

        public void InsertProject(Project project)
        {
            _projectRepository.Insert(project);
        }

        public void InsertProjectItem(ProjectItem projectItem)
        {
            _projectItemRepository.Insert(projectItem);
        }

        public void UpdateProjectItem(ProjectItem projectItem)
        {
            _projectItemRepository.Update(projectItem);
        }

        public void InsertComment(Comment comment)
        {
            _commentRepository.Insert(comment);
        }

        public ProjectItem GetProjectItemById(int id)
        {
            return _projectItemRepository.Table.Include(p => p.Pictures).Include(x => x.Comments).FirstOrDefault(p => p.Id == id);
        }
    }
}
