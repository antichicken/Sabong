using System.Linq;
using Sabong.Repository.EntityModel;

namespace Sabong.Repository.Repo
{
    public class CurrencyRepository
    {
        public string GetCurrencyValueByUserId(int userid)
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                //Select
                user xxx=new user();
                string query = string.Format("select `t1`.`value` AS `value` from (`currency` `t1` join `user` `t2` on((`t1`.`slno` = `t2`.`currency_type`))) where (`t2`.`slno` = {0})",userid);
                  var retval = context.Database.SqlQuery<string>(
                     query).FirstOrDefault();
                return retval;
            }
        }
    }
}