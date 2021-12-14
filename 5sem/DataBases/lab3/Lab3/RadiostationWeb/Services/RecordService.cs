using Microsoft.Extensions.Caching.Memory;
using RadiostationWeb.Data;
using RadiostationWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RadiostationWeb.Services
{
    public class RecordService
    {
        private readonly BDLab1Context _dbContext;
        private readonly IMemoryCache cache;

        public RecordService(BDLab1Context context, IMemoryCache memoryCache)
        {
            _dbContext = context;
            cache = memoryCache;
        }

        public void AddRecords(string key, int count = 20)
        {
            var records = _dbContext.Records.Take(count).ToList();

            cache.Set(key, records, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(246)
            });
        }

        public IEnumerable<Record> GetRecords(int count = 20)
        {
            return _dbContext.Records.Take(count).ToList();
        }

        public IEnumerable<Record> GetRecords(string key, int count = 20)
        {
            IEnumerable<Record> records = null;

            if (!cache.TryGetValue(key, out records))
            {
                records = _dbContext.Records.Take(count).ToList();

                if (records != null)
                {
                    cache.Set(key, records, new MemoryCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(246)
                    });
                }
            }

            return records;
        }
    }
}
