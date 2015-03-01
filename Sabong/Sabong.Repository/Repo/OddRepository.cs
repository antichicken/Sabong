using System.Data;
using Sabong.Repository.EntityModel;

namespace Sabong.Repository.Repo
{
    public class OddRepository
    {
        public oddsdiff_calc GetOddsdiffCalcByMatchId(int matchId)
        {
            var xxx = new oddsdiff_calc();
            return xxx;
        }

        public void UpdateOddDiffCalc(oddsdiff_calc oddsdiff)
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                context.oddsdiff_calc.Attach(oddsdiff);

                var entry = context.Entry(oddsdiff);
                entry.State = EntityState.Modified;

                entry.Property(e => e.diff).IsModified = false;
                entry.Property(e => e.match_slno).IsModified = false;

                context.SaveChanges();
            }
        }

    }
}