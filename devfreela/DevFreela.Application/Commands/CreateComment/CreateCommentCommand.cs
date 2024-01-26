using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<Unit>
    {
        public string Content { get; private set; }
        public int IdProject { get; private set; }
        public int IdUser { get; private set; }
    }
}
