using Elastic.Apm.Api;
using SimpleSocial.Common.Tracing;
using System;
#nullable enable

namespace SimpleSocial.Infrastructure.Tracing
{
    // Move to Infrastructe and remove the Elastic Dependency from Common, by wrapping in our own files
    public class SimpleSocialTracer : ISimpleSocialTracer
    {
        private readonly ITracer tracer;

        public SimpleSocialTracer(ITracer tracer)
        {
            this.tracer = tracer;
        }

        public ITransaction CurrentTransaction => this.tracer.CurrentTransaction;

        public ISpan? TryStartSpan(string name, ITransaction transaction)
        {
            return transaction?.StartSpan(name, ApiConstants.ActionExec);
        }

        public ITransaction? TryStartTransaction(string name, string operation, string distributedOpenTracingData)
        {
            return this.tracer.StartTransaction(
                name,
                operation,
                DistributedTracingData.TryDeserializeFromString(distributedOpenTracingData));
        }
    }
}
