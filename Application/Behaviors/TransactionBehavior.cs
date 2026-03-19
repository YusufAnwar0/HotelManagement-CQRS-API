using Application.Interface;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private readonly ITransactionalContext _transactionalContext;
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;

        public TransactionBehavior(ITransactionalContext transactionalContext, ILogger<TransactionBehavior<TRequest, TResponse>> logger)
        {
            _transactionalContext = transactionalContext;
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(!request.GetType().Name.EndsWith("Command"))
            {
                return await next();
            }

            try
            {
                await _transactionalContext.BeginTransactionAsync(cancellationToken);
                _logger.LogInformation($"Transaction started for {request.GetType().Name}");

                var response = await next(); 

                await _transactionalContext.CommitTransactionAsync(cancellationToken);
                _logger.LogInformation($"Transaction committed for {request.GetType().Name}");

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Transaction failed for {request.GetType().Name}, rolling back");
                await _transactionalContext.RollbackTransactionAsync(cancellationToken);
                throw;
            }


        }
    }
}
