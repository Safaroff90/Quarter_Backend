using Quarter.DAL;

namespace Quarter.Services
{
    public class LayoutService
    {
        private readonly QuarterContext _context;

        public LayoutService(QuarterContext context)
        {
            _context = context;
        }

        public Dictionary<string,string> GetSettings()
        {
            return _context.Settings.ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
