using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabong.Repository.EntityModel;
using Sabong.Util;

namespace Sabong.Repository.Repo
{
    public class MatchCreateStartRepository
    {
        public match_createstart GetMatchDetail(DateTime createDate,int arena)
        {
            try
            {
                using (var context = new s_dbEntities())
                {
                    return  context.match_createstart.FirstOrDefault(i => i.create_date == createDate && i.arenaid == arena);
                }
            }
            catch (Exception ex)
            {
                ex.LogError(string.Format("CreateDate: {0}, arena: {1}",createDate,arena));
                return null;
            }
        }
    }
}
