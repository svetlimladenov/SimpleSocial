using MediatR;
using Microsoft.Extensions.Logging;
using SimpleSocial.Common.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSocial.Web.Application.Common.PipelineBehaviors
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ISimpleSocialLogger<LoggingBehaviour<TRequest, TResponse>> logger;

        public LoggingBehaviour(ISimpleSocialLogger<LoggingBehaviour<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (Exception exception)
            {
                LogException(exception);
                throw;
            }
        }

        public void LogException(Exception exception)
        {
            this.logger.Error(
                exception,
                typeof(TRequest).Name,
                "insert apm transaction here",
                "insert apm transaction here");
        }
    }
}
