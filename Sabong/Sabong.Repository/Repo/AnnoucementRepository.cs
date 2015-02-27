using System.Linq;
using Sabong.Repository.EntityModel;

namespace Sabong.Repository.Repo
{
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