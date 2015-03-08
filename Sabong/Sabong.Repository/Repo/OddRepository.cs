using System;
using System.Data;
using System.Linq;
using Sabong.Repository.EntityModel;
using Sabong.Util;

namespace Sabong.Repository.Repo
{
    public class OddRepository
    {
        public oddsdiff_calc GetOddsdiffCalcByMatchId(int matchId)
        {
            try
            {
                using (var context = new s_dbEntities())
                {
                    return context.oddsdiff_calc.FirstOrDefault(i => i.match_slno == matchId);
                }
            }
            catch (Exception ex)
            {
                ex.LogError(string.Format("MatchId: {0}",matchId));
                return null;
            }
        }

        public void UpdateOddDiffCalc(oddsdiff_calc oddsdiff)
        {
            try
            {
                using (var context = new s_dbEntities())
                {
                    context.oddsdiff_calc.Attach(oddsdiff);

                    var entry = context.Entry(oddsdiff);
                    entry.State = EntityState.Modified;

                    entry.Property(e => e.diff).IsModified = false;
                    entry.Property(e => e.match_slno).IsModified = false;

                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                ex.LogError();
            }
        }

    }
}