namespace DemoKanban.Models
{
    public class AuditLog
    {
        public Guid Id { get; set; }

        public string Path { get; set; } = "";

        public string Route { get; set; } = "";

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public string User { get; set; } = "";

        public AuditLog(string path, string route,  string user)
        {
            Id = Guid.NewGuid();
            Path = path;
            User = user;
            Route = route;
        }
    }
}
