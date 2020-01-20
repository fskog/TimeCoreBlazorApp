using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCore.Models
{
    public class Timer
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string Title { get; private set; }
        public Guid CategorySystemId { get; private set; }

        public Timer()
        {
            StartTime = DateTime.MinValue;
            EndTime = DateTime.MinValue;
            Title = "new task";
            CategorySystemId = Guid.Empty;
        }


        public void SetTitle(string title) => Title = title;
        public void SetCategorySystemId(Guid systemId) => CategorySystemId = systemId;

        public void SetStartTime(DateTime startTime) => StartTime = startTime;

        public void SetEndTime(DateTime endTime) => EndTime = endTime;

    }
}
