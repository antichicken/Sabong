using System;
using System.Collections.Generic;
using System.Linq;
using Sabong.Repository.EntityModel;
using Sabong.Util;

namespace Sabong.Repository.Repo
{
    public class ReportRepository
    {
        public List<view_matchResult> GetMatchResultByDate(int arenaid, string strDate)
        {

            try
            {
                using (var context = new s_dbEntities())
                {
                    return (from matchresult in context.view_matchResult
                        where matchresult.fdate == strDate &&
                              matchresult.arena == arenaid
                        select matchresult).ToList();
                }
            }
            catch (Exception ex)
            {
                ex.LogError(string.Format("arena: {0}, strDate: {1}",arenaid,strDate));
                return null;
            }
        }

        //where transaction.playerid='$_SESSION[useridval]' and transaction.delflag='0' and DATE_FORMAT(transaction.date,'%Y-%m-%d') between '$start2' and '$end2'";
         public List<view_transactionReport> GetTransactionReports(int userId,DateTime fromdate,DateTime toDate)
        {
             try
             {
                 using (var context = new s_dbEntities())
                 {
                     var result = from transac in context.view_transactionReport
                                  where transac.playerid == userId &&
                                  transac.date >= fromdate &&
                                  transac.date <= toDate
                                  select transac;
                     return result.ToList();


                 }
             }
             catch (Exception ex)
             {
                 ex.LogError(string.Format("userId: {0}, From: {1}, To: {2}",userId,fromdate,toDate));
                 return null;
             }
        }
    }
}
