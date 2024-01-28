using System;
using System.Collections.Generic;
using DevFreela.Application.ViewModels;

namespace DevFreela.Application.Services.Interfaces
{
    public interface IProjectService //interface que representa todas as funcionalidades do projeto
    {
        List<ProjectViewModel> GetAll(string query); //service in referência do IActionResult Get(string query) no ProjectsController
        ProjectDetailsViewModel GetById(int id);

        //modelos de entrada
        //void Update(UpdateProjectInputModel inputModel);
        //void Delete(int id);
        //void CreateComment(CreateCommentInputModel inputModel); //added method in create comment command handler
        //void Start(int id);
        //void Finish(int id);
    }
}
