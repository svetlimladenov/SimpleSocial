using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleSocial.Common.Logging
{
    public interface ISimpleSocialLogger<TCategoryName>
    {
        private const string Dash = "";

        void Error(
            Exception exception,
            string operation,
            string traceId = Dash,
            string transactionId = Dash);

        void Warning(
            Exception exception,
            string operation,
            string traceId = Dash,
            string transactionId = Dash);

        void Debug(
           Exception exception,
           string operation,
           string traceId = Dash,
           string transactionId = Dash);

        void Write(
            SimpleSocialLogLevel level,
            Exception exception,
            string operation,
            string traceId = Dash,
            string transactionId = Dash);
    }
}
