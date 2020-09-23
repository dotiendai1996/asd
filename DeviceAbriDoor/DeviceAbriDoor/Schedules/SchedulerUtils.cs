using DeviceAbriDoor.Configs;
using DeviceAbriDoor.Models.Base;
using Quartz;
using Quartz.Impl;
using System;
using System.Globalization;
using TestDeviceAbriDoor;

namespace ScheduledService.Schedules
{
    public class SchedulerUtils
    {
        private static SchedulerUtils instance = new SchedulerUtils();
        public static SchedulerUtils Instance
        {
            get
            {
                if (instance == null) instance = new SchedulerUtils();
                return instance;
            }
        }

        private IScheduler scheduler;

        public void StartAsync()
        {
            var configs = AppConfigSection.GetInstance();
            if (configs.Scheduler.Enable)
            {
                TimeSpan time;
                if (TimeSpan.TryParse(configs.Scheduler.TimeStartJob, out time))
                {
                    // construct a scheduler factory
                    ISchedulerFactory schedulerFactory = new StdSchedulerFactory();

                    // get a scheduler, start the schedular before triggers or anything else
                    scheduler = schedulerFactory.GetScheduler();
                    scheduler.Start();

                    IJobDetail job = JobBuilder.Create<DeviceJob>()
                        .WithIdentity("DeviceJob", "DeviceChamCong")
                        .Build();

                    ITrigger trigger = TriggerBuilder.Create()
                        .WithIdentity("DeviceTrigger", "DeviceChamCong")
                        .StartAt(DateBuilder.TodayAt(time.Hours, time.Minutes, time.Seconds))
                        .WithSimpleSchedule(schedule =>
                        {
                            schedule.RepeatForever()
                                .WithIntervalInHours(configs.Scheduler.HoursRepeat);
                        })
                        .Build();

                    scheduler.ScheduleJob(job, trigger);
                }
            }
        }

        public void ShutdownAsync()
        {
            AbriDoorSDK.Instance.Disconnect();
            if (scheduler != null)
                scheduler.Shutdown();
        }
    }
}
