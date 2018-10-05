using System.Threading.Tasks;

namespace webapi
{
    public class LogProcessor
    {
        private IRepository _repository;

        public LogProcessor(IRepository repository)
        {
            _repository = repository;
        }

        public Task<LogEntry[]> GetLog()
        {
            return _repository.GetLog();
        }
    }
}