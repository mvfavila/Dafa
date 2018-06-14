using DAFA.Application.Interfaces;
using System;
using System.Configuration;
using System.Threading;
using System.Web.Mvc;

namespace DAFA.Presentation.UI.MVC
{
    public class StartupTimer
	{
        private readonly static string HOURS_BETWEEN_CHECKS = ConfigurationManager.AppSettings["HOURS_BETWEEN_CHECKS"];
		private readonly static string EVENT_CHECK_TIME = ConfigurationManager.AppSettings["EVENT_CHECK_TIME"];

        private readonly IEventAppService eventAppService;
		private Thread _thread;

		public StartupTimer()
		{
            eventAppService = DependencyResolver.Current.GetService<IEventAppService>();
        }

		public void StartEventWarningWatcher()
		{
			if (_thread == null)
			{
				_thread = new Thread(new ThreadStart(ConfigureTimer));
				_thread.Start();
			}
		}

		private void ConfigureTimer()
		{
			const int MILISECONDS_IN_ONE_HOUR = 3600000;

            var StateObj = new StateObjClass
			{
				TimerCanceled = false,
				SomeValue = 1
			};
			var TimerDelegate = new TimerCallback(TimerTask);

			var FIRST_DELAY = GetNumberOfSecondsUntilFirstRun();

            var TIME_BETWEEN_CHECKS = GetNumberOfHoursBetweenChecks() * MILISECONDS_IN_ONE_HOUR;

            // Create a timer that calls a procedure every 2 seconds.
            // Note: There is no Start method; the timer starts running as soon as
            // the instance is created.
            var TimerItem = new Timer(TimerDelegate, StateObj, FIRST_DELAY, TIME_BETWEEN_CHECKS);

			// Save a reference for Dispose.
			StateObj.TimerReference = TimerItem;
        }

		private static int GetNumberOfSecondsUntilFirstRun()
		{
			var param = EVENT_CHECK_TIME.Split(':');
			var hours = int.Parse(param[0]);
			var minutes = int.Parse(param[1]);
			var seconds = int.Parse(param[2]);
			var TODAY = DateTime.Today;
			var TIME_TO_EXECUTE = new DateTime(TODAY.Year, TODAY.Month, TODAY.Day, hours, minutes, seconds);

			var NOW = DateTime.Now;

            if (NOW.TimeOfDay > TIME_TO_EXECUTE.TimeOfDay)
				return Convert.ToInt32((TIME_TO_EXECUTE.AddDays(1) - NOW).TotalMilliseconds);

            return Convert.ToInt32((TIME_TO_EXECUTE - NOW).TotalMilliseconds);
		}

		private static byte GetNumberOfHoursBetweenChecks()
        {
            const byte STANDARD_VALUE = 24;
            const byte MAX_VALUE = 72;
			if(!byte.TryParse(HOURS_BETWEEN_CHECKS, out byte hours))
                return STANDARD_VALUE;

            return hours < MAX_VALUE ? hours : MAX_VALUE;
        }

        private class StateObjClass
		{
			// Used to hold parameters for calls to TimerTask.
			public int SomeValue;
			public Timer TimerReference;
			public bool TimerCanceled;
		}

		private void TimerTask(object StateObj)
		{
            eventAppService.ProcessEventWarnings();
		}
	}
}