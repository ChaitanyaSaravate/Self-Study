namespace Logging.SerilogClient.Extensions
{
    /// <summary>
    /// Used to set the Connection String from the source other than appsettings.json.
    /// </summary>
    public class SerilogSqlSinkOptions
    {
        public string ConnectionString { get; set; }
        public string ModuleName { get; set; } = "Cognosco";
    }
}
