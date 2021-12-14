using Microsoft.Extensions.Caching.Memory;
using RadiostationWeb.Data;
using RadiostationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadiostationWeb.Services
{
    public class PerformerService
    {
        private readonly BDLab1Context _dbContext;
        private readonly IMemoryCache cache;

        public PerformerService(BDLab1Context context, IMemoryCache memoryCache)
        {
            _dbContext = context;
            cache = memoryCache;
        }

        public void AddPerformers(string key, int count = 20)
        {
            var performers = _dbContext.Performers.Take(count).ToList();

            cache.Set(key, performers, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(246)
            });
        }

        public IEnumerable<Performer> GetPerformers(int count = 20)
        {
            return _dbContext.Performers.Take(count).ToList();
        }

        public IEnumerable<Performer> GetPerformers(string key, int count = 20)
        {
            IEnumerable<Performer> performers = null;

            if (!cache.TryGetValue(key, out performers))
            {
                performers = _dbContext.Performers.Take(count).ToList();

                if (performers != null)
                {
                    cache.Set(key, performers, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(246)
                    });
                }
            }

            return performers;

        }
    }
}
