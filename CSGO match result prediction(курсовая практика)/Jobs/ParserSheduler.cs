using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSGO_match_result_prediction_курсовая_практика_.Jobs
{
    public class ParserSheduler
    {
        public static async void Start()
        {
            IScheduler sheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await sheduler.Start();

            IJobDetail loadNewData = JobBuilder.Create<Parser>()
                .WithIdentity("loadDataTrigger", "parser")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("loadData", "parser")
                .StartNow()
                .WithSimpleSchedule(x => x
                .WithIntervalInMinutes(30)
                .RepeatForever()).Build();

            await sheduler.ScheduleJob(loadNewData, trigger);
        }
    }
}