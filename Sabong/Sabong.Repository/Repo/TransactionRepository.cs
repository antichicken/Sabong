using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using Sabong.Repository.EntityModel;

namespace Sabong.Repository.Repo
{
    public class TransactionRepository
    {
        //$qu1=mysql_query("select bet_commission  from commission where agentid='$resuplpt[firstparent_id]' and for_id='$resuplpt[player_id]'");
        //$re1=mysql_fetch_array($qu1); 
        //$qu2=mysql_query("select bet_commission  from commission where agentid='$resuplpt[secondparent_id]' and for_id='$resuplpt[firstparent_id]'");
        //$re2=mysql_fetch_array($qu2); 
        //$qu3=mysql_query("select bet_commission  from commission where agentid='$resuplpt[thirdparent_id]' and for_id='$resuplpt[secondparent_id]'");
        //$re3=mysql_fetch_array($qu3);
        //$qu4=mysql_query("select bet_commission  from commission where agentid='$resuplpt[fourthparent_id]' and for_id='$resuplpt[thirdparent_id]'");
        //$re4=mysql_fetch_array($qu4); 
        //$qu5=mysql_query("select bet_commission  from commission where agentid='0' and for_id='$resuplpt[fourthparent_id]'");
        //$re5=mysql_fetch_array($qu5); 
        public transaction GetBetComUserId(transaction trans)
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                var playerPt = context.player_pt_calc.FirstOrDefault(i => i.player_id == trans.playerid);

                //1484
                //   var callStore = context.SP_GetBetCommisionByUserId(result.player_id, result.firstparent_id,
                //     result.secondparent_id, result.thirdparent_id, result.fourthparent_id);

                transaction returnTransaction = trans;
                //  $condplpt=" betcomstat ='$res_betstat1[status]',
                returnTransaction.betcomstat = true;

                //`agentid`='$resuplpt[firstparent_id]',
                if (playerPt != null)
                {
                    returnTransaction.agentid = playerPt.firstparent_id;
                    // `agentbcper`='$resuplpt[frstparentcomm]',

                    returnTransaction.agentbcper = playerPt.frstparentcomm;


                    //`agentfbper`='$resuplpt[firstparent]',
                    returnTransaction.agentfbper = playerPt.firstparent;

                    //`masterid`='$resuplpt[secondparent_id]',
                    returnTransaction.masterid = playerPt.secondparent_id;

                    //`masterbcper`='$resuplpt[secondparentcomm]',
                    returnTransaction.masterbcper = playerPt.secondparentcomm;

                    //`masterfbper`='$resuplpt[secondparent]',
                    returnTransaction.masterfbper = playerPt.secondparent;

                    //`srmasterid`='$resuplpt[thirdparent_id]',
                    returnTransaction.srmasterid = playerPt.thirdparent_id;
                    //`srmasterbcper`='$resuplpt[thirdparentcomm]',
                    returnTransaction.srmasterbcper = playerPt.thirdparentcomm;

                    //`srmasterfbper`='$resuplpt[thirdparent]',
                    returnTransaction.srmasterfbper = playerPt.thirdparent;
                    //`houseid`='$resuplpt[fourthparent_id]',
                    returnTransaction.houseid = playerPt.fourthparent_id;
                    //`housebcper`='$resuplpt[fourthparentcomm]',
                    returnTransaction.housebcper = playerPt.fourthparentcomm;
                    //`housefbper`='$resuplpt[fourthparent]',
                    returnTransaction.housefbper = playerPt.fourthparent;
                    //`adminbcper`='$resuplpt[admincomm]',
                    returnTransaction.adminbcper = playerPt.admincomm;
                    //`adminfbper`='$adminfbper',
                    // adminfbper=   100-($resuplpt['firstparent']+$resuplpt['secondparent']+$resuplpt['thirdparent']+$resuplpt['fourthparent']);
                    returnTransaction.adminfbper = 100 - (playerPt.firstparent + playerPt.secondparent + playerPt.thirdparent + playerPt.fourthparent);





                    var alllever = context.Database.SqlQuery<AllLevelComm>(
                        @"SELECT  ( SELECT bet_commission  FROM   commission  where agentid={1} and for_id={0}  ) AS agentbetcomm,
    ( SELECT bet_commission FROM   commission where agentid={2} and for_id={1} ) AS masterbetcomm,
    ( SELECT bet_commission FROM   commission where agentid={3} and for_id={2}) AS srmasterbetcomm,
 ( SELECT bet_commission FROM   commission where agentid={4} and for_id={3} ) AS housebetcomm,

 ( SELECT bet_commission FROM   commission where agentid=0 and for_id={4} ) AS adminbetcomm", playerPt.player_id, playerPt.firstparent_id,
                        playerPt.secondparent_id, playerPt.thirdparent_id, playerPt.fourthparent_id).FirstOrDefault();

                    //`agentbetcomm`='$re1[bet_commission]',
                    if (alllever != null)
                    {
                        returnTransaction.agentbetcomm = alllever.AgentBetComm;
                        // masterbetcomm='$re2[bet_commission]',
                        returnTransaction.masterbetcomm = alllever.MasterBetComm;
                        // srmasterbetcomm='$re3[bet_commission]',
                        returnTransaction.srmasterbetcomm = alllever.SrmasterBetComm;
                        // housebetcomm='$re4[bet_commission]',
                        returnTransaction.housebetcomm = alllever.HouseBetComm;
                        // adminbetcomm='$re5[bet_commission]'";
                        returnTransaction.adminbetcomm = alllever.AdminBetComm;
                    }
                }

                return returnTransaction;
            }
        }

        //$alltrans=mysql_query("select t.* from `transaction` t, fight_assign f where t.`date`='$date' and t.`playerid`='$_SESSION[useridval]' and t.matchno=f.slno and f.winner_cockid=0 and f.cancelmatch=0")or die();
        public List<transaction> GetAllTransaction(int userId, DateTime date )
        {
            // date = date.ToString("dd-MM-yyyy");
            using (s_dbEntities context = new s_dbEntities())
            {
                var query = (from t in context.transactions
                    join f in context.fight_assign on t.matchno equals f.slno
                           
                    where t.playerid == userId && f.winner_cockid==0 && f.cancelmatch==0 && t.date==date
                    select t).ToList();

                return query;

            }
        }

        public List<transaction> GetAcceptedTransactions(int userId, int matchId)
        {
            // date = date.ToString("dd-MM-yyyy");
            using (s_dbEntities context = new s_dbEntities())
            {
                var query = (from  f in context.transactions 

                    where f.playerid == userId &&  f.matchno == matchId
                    select f).ToList();

                return query;

            }
        }

        public void Insert(transaction trans)
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                //trans.type = "";
                //trans.odds = "1:6";
                context.transactions.Add(trans);
                try
                {
                    context.SaveChanges();
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

        public void InsertTest(test trans)
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                context.tests.Add(trans);
                context.SaveChanges();

            }
        }

        public void UpdateTest(test trans)
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                context.tests.Attach(trans);

                var entry = context.Entry(trans);
                entry.State = EntityState.Modified;

                //entry.Property(e => e.nn).IsModified = false;
              

                context.SaveChanges();
            }
        }
       



     

    }
}