using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyLogger.Api.EasyTools
{
    public class TimeTools
    {
        public static List<DateTime> GetMonthByList(string quoTimeStart, string quoTimeEnd)
        {

            // 获取开始时间和结束时间 中间几张表 / 以及开始时间最大多少 / 结束时间最大是当月\

            var minTableMonth = IocManager.Configuration["EasyLogger:MinMonth"];


            DateTime MaxStartTime = Convert.ToDateTime($"{minTableMonth}-01 00:00:00");
            DateTime MaxEndTime = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;

            var quoTimeStartTmp = Convert.ToDateTime(quoTimeStart + "-01 00:00:00");
            var quoTimeEndTmp = Convert.ToDateTime(quoTimeEnd + "-01 00:00:00");

            if ((quoTimeStartTmp - MaxStartTime).Days < 0)
            {
                quoTimeStart = minTableMonth;
                quoTimeStartTmp = Convert.ToDateTime(quoTimeStart + "-01 00:00:00");
            }


            if ((quoTimeEndTmp - MaxEndTime).Days > 0)
            {
                quoTimeStart = MaxEndTime.ToString("yyyy-MM");
                quoTimeEndTmp = Convert.ToDateTime(quoTimeEnd + "-01 00:00:00");
            }


            var dateList = new List<DateTime>();
            for (; quoTimeStartTmp <= quoTimeEndTmp; quoTimeStartTmp = quoTimeStartTmp.AddMonths(1))
            {
                dateList.Add(quoTimeStartTmp);
            }

            return dateList;

        }

        public static List<int> GetDayDiff(DateTime dateStart, DateTime dateEnd)
        {
            var dateList = new List<int>();
            for (; dateStart <= dateEnd; dateStart = dateStart.AddDays(1))
            {
                dateList.Add(dateStart.Day);
            }
            return dateList;
        }
    }
}
