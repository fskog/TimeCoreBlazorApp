using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeCore.Models;

namespace TimeCore.Services
{
    public class TimeLogService
    {

        private List<TimeLogItem> TimeLog;
        private readonly ILogger<TimeLogService> _logger;


        public TimeLogService(ILogger<TimeLogService> logger)
        {
            _logger = logger;
            TimeLog = new List<TimeLogItem>();
        }

        public TimeLogItem Get(Guid systemId)
        {
            return TimeLog.FirstOrDefault(x => x.SystemId.Equals(systemId));
        }

        public List<TimeLogItem> GetAll()
        {
            _logger.LogInformation($"Getting all TimeLogs ({TimeLog.Count()})");

            return TimeLog;
        }

        public List<TimeLogItem> GetInterval(DateTime startTime)
            => GetInterval(startTime, DateTime.Now);


        /// <summary>
        /// Gets TimeLogItems in specified interval. 
        /// Use overload GetInterval(DateTime startTime) to return items from startTime until current time.
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public List<TimeLogItem> GetInterval(DateTime startTime, DateTime endTime)
        {
            _logger.LogInformation($"Getting TimeLogs with interval {startTime.ToString()} - {endTime.ToString()}");

            if (startTime.Equals(DateTime.MinValue))
            {
                _logger.LogDebug($"Get interval called with min startTime set, calling GetAll");

                return GetAll();
            }

            return TimeLog.Where(x => (x.StartTime > startTime) && (x.StartTime < endTime)).ToList();
        }   

        public List<TimeLogItem> GetByCategory(Guid categorySystemId)
        {
            return TimeLog.Where(x => x.CategorySystemId.Equals(categorySystemId)).ToList();
        }

        public void Add(TimeLogItem timeLogItem)
        {
            TimeLog.Add(timeLogItem);
        }

    }

}