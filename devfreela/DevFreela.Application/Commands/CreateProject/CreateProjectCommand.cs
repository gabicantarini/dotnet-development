using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Commands.CreateProject
{
    public class CreateProjectCommand : IRequest<int>// CreateProjectCommand => classe com as informações para cadastrar um projeto 
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int IdClient { get; set; }
        public int IdFreelancer { get; set; }
        public decimal TotalCost { get; set; }
    }
}
