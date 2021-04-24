using Elastic.Apm.Api;
#nullable enable

namespace SimpleSocial.Common.Tracing
{
    // TODO: Move the interface to common and use our interfaces for transactions and spans
    public interface ISimpleSocialTracer
    {
        ITransaction CurrentTransaction { get; }

        ITransaction? TryStartTransaction(string name, string operation, string distributedOpenTracingData);

        ISpan? TryStartSpan(string name, ITransaction transaction);
    }
}
