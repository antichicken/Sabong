
﻿
﻿using System;
using System.Collections.Generic;
﻿using System.Data;
﻿using System.Linq;



using Sabong.Repository.EntityModel;
﻿using Sabong.Util;

namespace Sabong.Repository.Repo
{
    public class AnnoucementRepository
    {
        public List<announcement> GetAll()
        {
            try
            {
                var currData = DateTime.Now.AddDays(-9);
                using (var context = new s_dbEntities())
                {
                    var result = from announcement in context.announcements
                                 where announcement.date >= currData
                                 orderby announcement.date descending

                                 select announcement;
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                ex.LogError();
                return null;
            }
        }
    }

    public class AnnoucementRunningRepository
    {
        //select * from `running_announcement` order by `id` DESC 
        public string GetAll()
        {
            using (var context = new s_dbEntities())
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
            using (var context = new s_dbEntities())
            {
                var result = context.running_announcement.OrderByDescending(i=>i.id);
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