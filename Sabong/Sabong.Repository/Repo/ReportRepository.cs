using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabong.Repository.EntityModel;

namespace Sabong.Repository.Repo
{
    public class ReportRepository
    {
        public List<view_matchResult> GetMatchResultByDate(int arenaid,string strDate)
        {
            
            using (s_dbEntities context = new s_dbEntities())
            {
                return  (from matchresult in context.view_matchResult
                    where matchresult.fdate == strDate &&
                    matchresult.arena==arenaid
                    select matchresult).ToList();
                try
                {
                  //  return result.ToList();
                }
                catch (DbEntityValidationException e)
                {

                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;

                    throw;
                }

                
            }

        }

        //where transaction.playerid='$_SESSION[useridval]' and transaction.delflag='0' and DATE_FORMAT(transaction.date,'%Y-%m-%d') between '$start2' and '$end2'";
         public List<view_transactionReport> GetTransactionReports(int userId,DateTime fromdate,DateTime toDate)
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                var result = from transac in context.view_transactionReport
                             where transac.playerid == userId &&
                             transac.date >= fromdate &&
                             transac.date <= toDate
                               select transac;
                return result.ToList();

               
            }

        }
    }
}
