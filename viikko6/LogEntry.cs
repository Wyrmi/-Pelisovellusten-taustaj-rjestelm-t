using System;

namespace webapi
{
    public class LogEntry
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
        public DateTime CreationTime { get; set; }
    }
}