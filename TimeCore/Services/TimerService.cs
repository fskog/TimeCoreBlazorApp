using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TimeCore.Models;


namespace TimeCore.Services
{

    public class TimerService
    {
        private readonly TimeLogService _timeLogService;
        private readonly CategoryService _categoryService;
        private readonly ILogger<TimerService> _logger;
        private Timer CurrentTimer;
        public event EventHandler TimerWasUpdated;

        public Timer TestTimer;

        public TimerService(TimeLogService timeLogService, CategoryService categoryService, ILogger<TimerService> logger)
        {
            _categoryService = categoryService;
            _timeLogService = timeLogService;
            _logger = logger;
            CurrentTimer = new Timer();
            TestTimer = new Timer();
        }

        

        void NotifyTimerWasUpdated()
        {
            _logger.LogInformation($"Fire event TimerWasUpdated.");
            TimerWasUpdated.Invoke(this, null);
        }

        public string Title
        {
            get
            {
                return CurrentTimer.Title;
            }
            set
            {
                CurrentTimer.SetTitle(value);
                NotifyTimerWasUpdated();
            }
        }

        public string CategorySystemId => CurrentTimer.CategorySystemId.ToString();

        public string StartTime => CurrentTimer.StartTime.ToString("t");

        public string StartDate => CurrentTimer.StartTime.ToString("M");

        public string EndTime => CurrentTimer.EndTime.ToString("t");
        public string EndDate => CurrentTimer.EndTime.ToString("M");

        public TimeSpan Elapsed => IsRunning ? DateTime.Now - CurrentTimer.StartTime : CurrentTimer.EndTime - CurrentTimer.StartTime;
        public string ElapsedFormatted => Elapsed.ToString((Elapsed.Hours > 0 ? "hh' h '" : "") + (Elapsed.Minutes > 0 ? "m' min '" : "") + "s' sec '");


        public List<KeyValuePair<string, string>> GetAllCategories()
        {
            var categories = _categoryService.GetAll()?.Select(x => new KeyValuePair<string, string>(x.SystemId.ToString(), x.Name));
            return categories.Any() ? categories.ToList() : new List<KeyValuePair<string, string>>() { new KeyValuePair<string, string>(Guid.NewGuid().ToString(), "empty...") };
        }

        public void SetTitle(string title)
        {
            _logger.LogInformation($"Setting new title: {title}", title);
            CurrentTimer.SetTitle(title);
            NotifyTimerWasUpdated();

        }

        public void SetCategory(string categoryId)
        {
            if (Guid.TryParse(categoryId, out var systemId))
            {
                var category = _categoryService.Get(systemId);
                if (category != null)
                {
                    _logger.LogInformation($"Setting new Category {category.SystemId}", category);
                    CurrentTimer.SetCategorySystemId(category.SystemId);
                    NotifyTimerWasUpdated();

                }
                else
                {
                    _logger.LogWarning($"Did not get back category with input {systemId}");
                }
            }
            else
            {
                CurrentTimer.SetCategorySystemId(Guid.Empty);
                NotifyTimerWasUpdated();

                _logger.LogWarning("Setting empty Category", categoryId);
            }

        }

        public string GetCategory()
        {
            return _categoryService.Get(CurrentTimer.CategorySystemId)?.Name ?? "";
        }

        public Guid GetCategorySystemId() => CurrentTimer.CategorySystemId;
        public DateTime GetStartTime() => CurrentTimer.StartTime;
        public DateTime GetEndTime() => CurrentTimer.EndTime;

        public void SetStartTime(DateTime time)
        {
            CurrentTimer.SetStartTime(time);
            NotifyTimerWasUpdated();
        }

        public void SetEndTime(DateTime time)
        {
            CurrentTimer.SetEndTime(time);
            NotifyTimerWasUpdated();
        }

        public bool IsRunning => CurrentTimer.StartTime > DateTime.MinValue && CurrentTimer.EndTime != null && CurrentTimer.EndTime == DateTime.MinValue;

        private void Start()
        {
            _logger.LogInformation("Starting new Timer");
            CurrentTimer.SetStartTime(DateTime.Now);
            NotifyTimerWasUpdated();
        }

        private void Stop()
        {
            _logger.LogInformation("Stopping Timer");
            CurrentTimer.SetEndTime(DateTime.Now);
            SaveTime();
            CurrentTimer = new Timer();
            NotifyTimerWasUpdated();
        }

        public void SaveTime()
        {
            var newTimeLogItem = new TimeLogItem(
                    CurrentTimer.StartTime,
                    CurrentTimer.EndTime,
                    CurrentTimer.Title,
                    CurrentTimer.CategorySystemId);

            _logger.LogInformation($"Adding new time log: {JsonSerializer.Serialize(newTimeLogItem)}");
            _timeLogService.Add(newTimeLogItem);
        }



        public void ToggleRunning()
        {
            if (IsRunning)
            {
                Stop();
            }
            else
            {
                Start();
            }
        }

    }
}
