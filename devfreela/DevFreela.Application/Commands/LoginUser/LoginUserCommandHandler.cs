using DevFreela.Application.ViewModels;
using DevFreela.Core.Repositories;
using DevFreela.Core.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserViewModel>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;

        public LoginUserCommandHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<LoginUserViewModel> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            //Recebe a senha e utiliza o mesmo algoritmo para criar a hash dessa senha
            var passwordHash = _authService.ComputeSha256Hash(request.Password);

            //Busca no meu banco de dados um User que tenha meu email e minha senha em formato hash
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(request.Email, passwordHash);

            //Se não existir, erro no login
            if (user == null)
            {
                return null;
            }

            //Se existir, gera o token usando os dados do User
            var token = _authService.GenerateJwTToken(user.Email, user.Role);

            return new LoginUserViewModel(user.Email, token);

        }
    }
}
