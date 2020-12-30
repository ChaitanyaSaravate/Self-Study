using System;
using System.Collections.Generic;
using System.Text;
using Serilog;
using Serilog.Configuration;

namespace Logging.SerilogClient.Extensions
{
    public static class CustomEnrichmentConfigurationExtension
    {
        public static LoggerConfiguration WithCustomEnricher(
            this LoggerEnrichmentConfiguration enrichmentConfiguration)
        {
            if (enrichmentConfiguration == null) throw new ArgumentNullException(nameof(enrichmentConfiguration));
            return enrichmentConfiguration.With<AuditLogEventEnricher>();
        }
    }
}
