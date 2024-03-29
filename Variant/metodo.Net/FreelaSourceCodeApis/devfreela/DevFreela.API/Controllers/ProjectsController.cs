﻿using DevFreela.API.Models;
using DevFreela.Application.Commands.CreateProject;
using DevFreela.Application.Services.Interfaces;
using DevFreela.Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        private readonly IMediator _mediator;
        public ProjectsController(IProjectService projectService, IMediator mediator) //interface de projectservice
        {
            _projectService = projectService;
            _mediator = mediator;
        }

        // api/projects?query=net core
        [HttpGet]
        public IActionResult Get(string query)//query é um parâmetro para consulta
        {
            var projects = _projectService.GetAll(query);

            return Ok(projects);
        }

        // api/projects/2
        [HttpGet("{id}")]
        public IActionResult GetById(int id) // consulta com parâmetro de url id
        {
            var project = _projectService.GetById(id);

            if (project == null)
            {
                return NotFound(); //404
            }

            return Ok(project);
        }


        [HttpPost]
        //o post retorna uma anotação com o corpo da requisição [from body] com o objeto da CreateProjectModel (q tem o id, o titulo e a description)
        public async Task<IActionResult> Post([FromBody] CreateProjectCommand  command)
        {
            // o post retorna a informação pro frontend
            
            if (command.Title.Length > 50) //validação para que o título não seja maior que 50
            {
                // o post retorna bad request quando não cumpre a validação 
                return BadRequest();
            }

            //var id = _projectService.Create(inputModel);
            var id = await _mediator.Send(command); //O metodo send sempre retorna uma task

            // Se cumprir a validação, o post retorna o o código 201 através do método CreatedAtAction() que por default espera receber 3 parametros
            // 1 parametro retorna o nome da API com os detalhes = nameof(GetById)
            // 2 parametro retorna o ID do objeto que acabou de ser cadastrado =  new { id = createProject.Id }
            // 3 parametro retorna o objeto cadastrado =  createProject
            // Cadastrar o projeto

            return CreatedAtAction(nameof(GetById), new { id = id }, command);
        }

        // api/projects/2 - Ex: vai atualizar o objeto com o id
        [HttpPut("{id}")]
        // o put retorna uma anotação com o corpo da requisição [from body] com o objeto da UpdateProjectModel (que só tem a descrição)
        public IActionResult Put(int id, [FromBody] UpdateProjectInputModel inputModel)
        {
            if (inputModel.Description.Length > 200)
            {
                return BadRequest();
            }

            _projectService.Update(inputModel);
            // reotrno padrão do put é NoContent() que atualiza o objeto
            return NoContent();
        }

        // api/projects/3 DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _projectService.Delete(id);

            // Remover 

            return NoContent();
        }

        // api/projects/1/comments POST
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, [FromBody] CreateCommentInputModel inputModel)
        {
            _projectService.CreateComment(inputModel);
            return NoContent();
        }

        // api/projects/1/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            _projectService.Start(id);
            return NoContent();
        }

        // api/projects/1/finish
        [HttpPut("{id}/finish")]
        public IActionResult Finish(int id)
        {
            _projectService.Finish(id);
            return NoContent();
        }
    }
}
