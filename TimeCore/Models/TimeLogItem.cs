using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCore.Models
{
    public class TimeLogItem
    {
        public Guid SystemId { get; private set; }
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string Title { get; private set; }
        public Guid CategorySystemId { get; private set; }


        public TimeLogItem(DateTime startTime, DateTime endTime, string title = "untitled") =>
            new TimeLogItem(startTime, endTime, title, Guid.Empty);

        public TimeLogItem(DateTime startTime, DateTime endTime, string title, Guid cateogrySystemId)
        {
            SystemId = Guid.NewGuid();
            StartTime = startTime;
            EndTime = endTime;
            Title = title;
            CategorySystemId = cateogrySystemId;
        }





        public void SetTitle(string title) => Title = title;
        public void SetCategorySystemId(Guid systemId) => CategorySystemId = systemId;

        public void SetStartTime(DateTime startTime) => StartTime = startTime;

        public void SetEndTime(DateTime endTime) => EndTime = endTime;
    }
}
