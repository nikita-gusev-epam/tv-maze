using System.Diagnostics;
using Serilog.Core;
using Serilog.Events;

namespace EPAM.TvMaze.Infrastructure
{
    public class CorrelationIdEnricher : ILogEventEnricher
    {
        private const string CorrelationIdPropertyName = "CorrelationId";

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var correlationId = Trace.CorrelationManager.ActivityId;
            var correlationIdProperty = new LogEventProperty(CorrelationIdPropertyName, new ScalarValue(correlationId));

            logEvent.AddPropertyIfAbsent(correlationIdProperty);
        }
    }
}
