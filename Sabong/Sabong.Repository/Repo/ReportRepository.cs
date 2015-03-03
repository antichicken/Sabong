using System;
using System.Collections.Generic;
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
                var result = from matchresult in context.view_matchResult
                    where matchresult.fdate == strDate &&
                    matchresult.arena==arenaid
                    select matchresult;
                return result.ToList();
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
