using System.Linq;
using Sabong.Repository.EntityModel;

namespace Sabong.Repository.Repo
{
    public class CurrencyRepository
    {
        public currency GetBetComUserId(int id)
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                return context.currencies.FirstOrDefault(i => i.slno == id);
            }
        }
    }
}