using System.Collections.Generic;
using System.Linq;
using Sabong.Repository.EntityModel;

namespace Sabong.Repository.Repo
{
    public class DerbyRepository
    {
        public List<derby> GetAll()
        {
            // //select * from `derby` 
            using (s_dbEntities context = new s_dbEntities())
            {
                return context.derbies.ToList();
            }

        }
    }
}