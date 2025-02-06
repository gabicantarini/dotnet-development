using FluentValidation;
using MediatR;

namespace VatRate;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
                                          where TRequest : IRequest<TResponse>
{
    private readonly IValidator<TRequest> _validator;

    public ValidationBehavior(IValidator<TRequest> validator)
    {
        _validator = validator;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse>
                                                next, CancellationToken cancellationToken)
    {
        
        if (_validator is null)
        {
            return await next();
        }

        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        return await next();
    }
}