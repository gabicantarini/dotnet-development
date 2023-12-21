using System;
using System.Collections.Generic;
using DevFreela.Application.ViewModels;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IProjectService
    {
        List<ProjectViewModel> GetAll(string query); //service in referência do IActionResult Get(string query) no ProjectsController
        ProjectDetailsViewModel GetById(int id);

        int Create(NewProjectInputModel inputModel);
        void Update(UpdateProjectInputModel inputModel);
        void Delete(int id);
        void CreateComment(CreateCommentInputModel inputModel);
        void Start(int id);
        void Finish(int id);
    }
}
