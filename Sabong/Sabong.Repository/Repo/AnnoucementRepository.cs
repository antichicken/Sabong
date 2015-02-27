
﻿
﻿using System;
using System.Collections.Generic;
using System.Linq;



using Sabong.Repository.EntityModel;

namespace Sabong.Repository.Repo
{
    public class AnnoucementRepository
    {
        //select * from announcement WHERE (date >= DATE_SUB(CURDATE(), INTERVAL 9 DAY)) order by date DESC,slno desc
        public List<announcement> GetAll()
        {
            var currData = DateTime.Now.AddDays(-9);
            using (s_dbEntities context = new s_dbEntities())
            {
                context.Database.Connection.Open();
                var result = from announcement in context.announcements
                    where announcement.date>= currData
                    orderby announcement.date descending 

                    select announcement;
                //  return result.ToList();
                context.Database.Connection.Close();
                return result.ToList();

                

            }
        }
    }

    public class AnnoucementRunningRepository
    {
        //select * from `running_announcement` order by `id` DESC 
        public string GetAll()
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                var result = context.running_announcement;

                if (result == null) return "";
                string retVal = "";
                foreach (var announcement in result)
                {
                    retVal += announcement.announcement;
                }
                return retVal;
            }
        }

        public string GetLatest()
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                var result = context.running_announcement.OrderByDescending(i=>i.id);

                if (result == null) return "";
                var row = result.FirstOrDefault();
                if (row!=null)
                {
                    return row.announcement;
                }
                return string.Empty;
            }
        }
    }
}