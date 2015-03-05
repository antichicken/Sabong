using System.Collections.Generic;
using System.Linq;
using Sabong.Repository.EntityModel;

namespace Sabong.Repository.Repo
{
    public class ArenaRepository
    {
        public List<arena> GetAll()
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                var result = from are in context.arenas
                    select are;
                return result.ToList();
            }
        }
    }
}