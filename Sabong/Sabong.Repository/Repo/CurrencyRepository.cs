using System;
using System.Linq;
using Sabong.Repository.EntityModel;
using Sabong.Util;

namespace Sabong.Repository.Repo
{
    public class CurrencyRepository
    {
        public string GetCurrencyValueByUserId(int userid)
        {
            try
            {
                using (var context = new s_dbEntities())
                {
                    string query = string.Format("select `t1`.`value` AS `value` from (`currency` `t1` join `user` `t2` on((`t1`.`slno` = `t2`.`currency_type`))) where (`t2`.`slno` = {0})", userid);
                    var retval = context.Database.SqlQuery<string>(
                       query).FirstOrDefault();
                    return retval;
                }
            }
            catch (Exception ex)
            {
                ex.LogError("UserId: "+userid);
                return string.Empty;
            }
            
        }
    }
}