﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.DeleteProject
{
    public class DeleteProjectCommand : IRequest<Unit> //Unit -> usamos quando não há tipo de retorno
    {
        public DeleteProjectCommand(int id)
        {
            Id = id;
        }

        public int Id { get; private set; }
    }
}
