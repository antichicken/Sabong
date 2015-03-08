using System;
using System.Collections.Generic;
using System.Linq;
using Sabong.Repository.EntityModel;
using Sabong.Util;

namespace Sabong.Repository.Repo
{
    public class TransferRepository
    {
        public List<multiple_transfer> GeTransfersByType(string type)
        {
            try
            {
                using (var context = new s_dbEntities())
                {
                    var result = from are in context.multiple_transfer
                                 where are.type == type
                                 select are;
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                ex.LogError("Type: "+type);
                return null;
            }
            
        }
    }
}