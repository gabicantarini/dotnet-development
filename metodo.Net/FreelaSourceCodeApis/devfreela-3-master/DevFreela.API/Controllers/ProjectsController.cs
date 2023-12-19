using DevFreela.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFreela.API.Controllers
{
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly OpeningTimeOption _option;
        public ProjectsController(IOptions<OpeningTimeOption> option, ExampleClass exampleClass)
        {
            exampleClass.Name = "Updated at ProjectsController";

            _option = option.Value;
        }

        // api/projects?query=net core
        [HttpGet]
        public IActionResult Get(string query)
        {
            // Buscar todos ou filtrar

            return Ok();
        }

        // api/projects/2
        [HttpGet("{id}")]
        public IActionResult GetById(int id) // consulta com parâmetro de url id
        {
            // Buscar o projeto

            // return NotFound();

            return Ok();
        }

        [HttpPost]

        //[from body] é uma anotação que retorna o corpo da requisição
        //CreateProjectModel é um objeto do corpo da requisição
        public IActionResult Post([FromBody] CreateProjectModel createProject)
        {
            // o post retorna a informação pro frontend
            
            if (createProject.Title.Length > 50) //validação para que o título não seja maior que 50
            {
                // o post retorna bad request quando não cumpre a validação 
                return BadRequest();
            }

            // Se cumprir a validação, o post retorna o o código 201 através do método CreatedAtAction() que por default espera receber 3 parametros
            // 1 parametro retorna o nome da API com os detalhes = nameof(GetById)
            // 2 parametro retorna o ID do objeto que acabou de ser cadastrado =  new { id = createProject.Id }
            // 3 parametro retorna o objeto cadastrado =  createProject
            // Cadastrar o projeto

            return CreatedAtAction(nameof(GetById), new { id = createProject.Id }, createProject);
        }

        // api/projects/2
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateProjectModel updateProject)
        {
            if (updateProject.Description.Length > 200)
            {
                return BadRequest();
            }

            // Atualizo o objeto

            return NoContent();
        }

        // api/projects/3 DELETE
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Buscar, se não existir, retorna NotFound

            // Remover 

            return NoContent();
        }

        // api/projects/1/comments POST
        [HttpPost("{id}/comments")]
        public IActionResult PostComment(int id, [FromBody] CreateCommentModel createComment)
        {
            return NoContent();
        }

        // api/projects/1/start
        [HttpPut("{id}/start")]
        public IActionResult Start(int id)
        {
            return NoContent();
        }

        // api/projects/1/finish
        [HttpPut("{id}/finish")]
        public IActionResult Finish(int id)
        {
            return NoContent();
        }
    }
}
