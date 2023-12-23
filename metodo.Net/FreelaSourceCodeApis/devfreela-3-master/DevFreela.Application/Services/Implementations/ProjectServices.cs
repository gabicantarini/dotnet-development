using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infraestructure.Persistence;
using System;
using System.Collections.Generic;


namespace DevFreela.Application.Services.Implementations //Adicionado referência ao projeto do Core e Infraestrutura
{
    public class ProjectServices : IProjectService
    {
        private readonly DevFreelaDbContext _dbContext; //para que não seja alterado
        public ProjectServices(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext; // Parallel acessar os dados do dbContext em memória
        }
        public int Create(NewProjectInputModel inputModel)
        {
            var project = new Project(inputModel.Title, inputModel.Description, inputModel.IdClient, inputModel.IdFreelancer, inputModel.TotalCost);
            _dbContext.Projects.Add(project); //quando salvarmos a entidade no banco de dados, ele preenche e retorna o id
            return project.Id;
        }

        public void CreateComment(CreateCommentInputModel inputModel)
        {
            var comment = new ProjectComment(inputModel.Content, inputModel.IdProject, inputModel.IdUser);
            _dbContext.Comments.Add(comment);
        }

        public void Delete(int id)
        {
            //_dbContext.Projects.Remove(); //Remove da Lista
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id); // faz a consulta

            project.Cancel(); //cancela o serviço
        }

        public void Finish(int id)
        {
            throw new NotImplementedException(); //add method
        }

        public List<ProjectViewModel> GetAll(string query)
        {
            var projects = _dbContext.Projects;

            var projectViewModel = projects
                .Select(p => new ProjectViewModel(p.Title, p.CreatedAt))
                .ToList();
            return projectViewModel;
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Start(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            throw new NotImplementedException();
        }
    }
}
