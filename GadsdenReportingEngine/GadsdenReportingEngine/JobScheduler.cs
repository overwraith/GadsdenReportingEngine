/*Author: Cameron Block*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quartz;
using Quartz.Impl;

namespace GadsdenReportingEngine {
    /// <summary>
    /// Job scheduler for Gadsden Reporting engine. 
    /// </summary>
    public class JobScheduler {

        /// <summary>
        /// Schedule all the jobs. 
        /// </summary>
        public static void Start() {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();
            IJobDetail job = JobBuilder.Create<ArchiveCleanupJob>().Build();
            ITrigger trigger = TriggerBuilder.Create()
                .WithDailyTimeIntervalSchedule(sched => sched.WithIntervalInHours(24)
                    .OnEveryDay()
                    .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(12, 0)))
                .StartAt(DateTime.UtcNow)
                .WithPriority(1)
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }//end method

    }//end class

}//end namespace
