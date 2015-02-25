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
    }
}