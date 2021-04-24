using MediatR;
using Microsoft.Extensions.Logging;
using SimpleSocial.Common.Logging;
using SimpleSocial.Common.Tracing;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SimpleSocial.Web.Application.Common.PipelineBehaviors
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ISimpleSocialLogger<LoggingBehaviour<TRequest, TResponse>> logger;
        private readonly ISimpleSocialTracer tracer;

        public LoggingBehaviour(ISimpleSocialLogger<LoggingBehaviour<TRequest, TResponse>> logger, ISimpleSocialTracer tracer)
        {
            this.logger = logger;
            this.tracer = tracer;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var span = tracer.TryStartSpan(typeof(TRequest).Name, tracer.CurrentTransaction);
            try
            {
                return await next();
            }
            catch (Exception exception)
            {
                span?.CaptureException(exception);
                LogException(exception);
                throw;
            }
            finally
            {
                span?.End();
            }
        }

        public void LogException(Exception exception)
        {
            this.logger.Error(
                exception,
                typeof(TRequest).Name,
                traceId: tracer.CurrentTransaction.Id,
                transactionId: tracer.CurrentTransaction.TraceId);
        }
    }
}
