﻿using Microsoft.ApplicationInsights;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppAspNetCore.Services
{
    public class ServiceException : Exception
    {
        public ServiceException() : base("Service Exception") { }
        public ServiceException(Exception inner) : base("Service Exception", inner) { }
    }

    public class SomeService
    {
        public static void ThrowAnExceptionPlease()
        {
            throw new ServiceException();
        }

        public static void SomeWorkWithDependency()
        {
            var success = false;
            var telemetry = new TelemetryClient();
            var startTime = DateTime.UtcNow;
            var timer = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                var dependency = new DependencyService();
                success = dependency.DoSomeWork();
            }
            finally
            {
                timer.Stop();
                telemetry.TrackDependency("DependencyService", "DoSomeWork", startTime, timer.Elapsed, success);
            }
        }

        public static void TrackCustomStuff(int fivekmTime) {
            var telemetry = new TelemetryClient();
            var properties = new Dictionary<string, string> { { "Run", "RunName" }, { "Jog", "Race" } };
            var measurements = new Dictionary<string, double> { { "RunTime", fivekmTime }, { "Opponents", 1 } };
            telemetry.TrackEvent("RunCompleted", properties, measurements);
            telemetry.TrackMetric("RunTime", fivekmTime, properties);
        }

        public class DependencyService
        {
            public bool DoSomeWork()
            {
                System.Threading.Thread.Sleep(5000);
                return true;
            }
        }
    }
}
