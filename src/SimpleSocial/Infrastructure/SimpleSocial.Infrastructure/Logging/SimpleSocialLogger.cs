using Microsoft.Extensions.Logging;
using SimpleSocial.Common.Logging;
using System;

namespace SimpleSocial.Infrastructure.Logging
{
    public class SimpleSocialLogger<TCategoryName> : ISimpleSocialLogger<TCategoryName>
    {
        private const string MessageTemplate = "operation : {operation}, traceId : {traceId}, transactionId : {transactionId}";
        private const string Dash = "-";
        private readonly ILogger<TCategoryName> logger;

        public SimpleSocialLogger(ILogger<TCategoryName> logger)
        {
            this.logger = logger;
        }

        public void Debug(Exception exception, string operation, string traceId = Dash, string transactionId = Dash)
        {
            this.BaseLogging(
                SimpleSocialLogLevel.Debug,
                exception: exception,
                operation: operation,
                traceId: traceId,
                transactionId: transactionId
                );
        }

        public void Error(Exception exception, string operation, string traceId = Dash, string transactionId = Dash)
        {
            this.BaseLogging(
                SimpleSocialLogLevel.Error,
                exception: exception,
                operation: operation,
                traceId: traceId,
                transactionId: transactionId
                );
        }

        public void Warning(Exception exception, string operation, string traceId = Dash, string transactionId = Dash)
        {
            this.BaseLogging(
                SimpleSocialLogLevel.Warning,
                exception: exception,
                operation: operation,
                traceId: traceId,
                transactionId: transactionId
                );
        }

        public void Write(SimpleSocialLogLevel level, Exception exception, string operation, string traceId = "", string transactionId = "")
        {
            this.BaseLogging(
                level,
                exception: exception,
                operation: operation,
                traceId: traceId,
                transactionId: transactionId
                );
        }

        private void BaseLogging(SimpleSocialLogLevel level, Exception exception, string operation = Dash, string traceId = Dash, string transactionId = Dash)
        {
            this.logger.Log(
                (LogLevel)level,
                exception,
                MessageTemplate,
                operation,
                traceId,
                transactionId
                );
        }
    }
}
