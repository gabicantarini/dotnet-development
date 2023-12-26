using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using DevFreela.Core.Entities;
using DevFreela.Infraestructure.Persistence;
using System;
using System.Collections.Generic;


namespace DevFreela.Application.Services.Implementations //Adicionado referência ao projeto do Core e Infraestrutura
{
    public class ProjectServices : IProjectService // para implementar o project service e criar um metodo para todas as funcionalidades
    {
        private readonly DevFreelaDbContext _dbContext; //para que não seja alterado
        public ProjectServices(DevFreelaDbContext dbContext)
        {
            _dbContext = dbContext; // Parallel acessar os dados do dbContext em memória através dessa injeção de dependência
        }
        public int Create(NewProjectInputModel inputModel)
        {
            var project = new Project(inputModel.Title, inputModel.Description, inputModel.IdClient, inputModel.IdFreelancer, inputModel.TotalCost);// converte o NewProjectInputModel para um project
            _dbContext.Projects.Add(project); //Aqui o id não é inicializado. Mas quando salvarmos a entidade no banco de dados, ele preenche e retorna o id
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

            project.Cancel(); //cancela o serviço para não remover de uma vez
        }

        public void Finish(int id)
        {
            var project = _dbContext.Projects.FirstOrDefault(p => p.Id == id);

            project.Finished();

        }

        public List<ProjectViewModel> GetAll(string query)
        {
            var projects = _dbContext.Projects;

            var projectViewModel = projects
                .Select(p => new ProjectViewModel(p.Id, p.Title, p.CreatedAt)) 
                .ToList(); //retorna uma lista a partir da projeção dos dados
            return projectViewModel;
        }

        public ProjectDetailsViewModel GetById(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p=> p.Id == id); //faz consulta ao db e retorna o id
            var projectDetailsViewMdel = new ProjectDetailsViewModel(
                project.Id,
                project.Title,
                project.Description,
                project.TotalCost,
                project.StartedAt,
                project.FinishedAt); //retorna um novodado através desse mapeamento.
                                     //obs: esse mapeamento pode ser substituido pelo auto mapper
            return projectDetailsViewMdel;
        }

        public void Start(int id)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == id);

            project.Started();
        }

        public void Update(UpdateProjectInputModel inputModel)
        {
            var project = _dbContext.Projects.SingleOrDefault(p => p.Id == inputModel.Id);

            project.Update(inputModel.Title, inputModel.Description, inputModel.TotalCost);
        }
    }
}
