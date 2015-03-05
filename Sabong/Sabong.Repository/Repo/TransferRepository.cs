using System.Collections.Generic;
using System.Linq;
using Sabong.Repository.EntityModel;

namespace Sabong.Repository.Repo
{
    public class TransferRepository
    {
        public List<multiple_transfer> GeTransfersByType(string type)
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                var result = from are in context.multiple_transfer
                    where are.type==type
                    select are;
                return result.ToList();
            }
        }
    }
}