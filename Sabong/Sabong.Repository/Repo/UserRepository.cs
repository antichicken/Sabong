using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Sabong.Repository.EntityModel;
using System.Security.Cryptography;

namespace Sabong.Repository.Repo
{
    public class ChickenWin
    {
        public string ChickenResult { get; set; }
    }

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
                context.transactions.Add(trans);
                context.SaveChanges();

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
    public class UserRepository
    {

        //  $queryplpt=mysql_query("SELECT * FROM `player_pt_calc` where player_id='$_SESSION[useridval]'");
        public player_pt_calc GetPlayerPtByUserId(int userId)
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                var result = context.player_pt_calc.FirstOrDefault(i => i.player_id == userId);
                return result;
            }
        }

        //select `currency_type` from `user` where `slno`='$userid'----> get currency
        //select `balance` from `openning_balance` where `date` like '%$date%' and `userid`='$userid'

        //  mysql_query("update `bidpoints` set `updated_bidpoint`=`updated_bidpoint`-$sacceptval where `agent_id`='$_SESSION[useridval]'")or die();

        public void UpdateBidPoint(bidpoint creditBalance)
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                context.bidpoints.Attach(creditBalance);

                var entry = context.Entry(creditBalance);
                entry.State = EntityState.Modified;

                entry.Property(e => e.bidpoint1).IsModified = false;
               
                entry.Property(e => e.agent_id).IsModified = false;
                context.SaveChanges();
            }
        }

        public playerbet_limit GetPlayerbetLimit(int userId)
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                var result = context.playerbet_limit.FirstOrDefault(i => i.playerid == userId);
                return result;
            }
        }
         public double DayCashBalance(int userId,DateTime todayDateTime)
         {
             using (s_dbEntities context = new s_dbEntities())
             {
                 var result = context.openning_balance.FirstOrDefault(i => i.userid == userId&& i.date==todayDateTime);

                 return result != null ? result.balance : 0;
             }
         }

         //select `unique_idval` from `user` where `slno`='$userid'
         public string GetUniqueIdVal(int userId)
         {
             using (s_dbEntities context = new s_dbEntities())
             {
                 var result = context.users.FirstOrDefault(i => i.slno == userId );

                 return result != null ? result.unique_idval : "";
             }
         }


         public double GetCashBalance(int userId)
         {
             using (s_dbEntities context = new s_dbEntities())
             {
                 var result = context.bidpoints.FirstOrDefault(i => i.agent_id == userId);

                 return result != null ? result.updated_bidpoint : 0;
             }
         }
         //select `bidpoint` from `bidpoints` where `agent_id`='$agntid'
         public double GetCreditBalance(int userId)
         {
             using (s_dbEntities context = new s_dbEntities())
             {
                 var result = context.bidpoints.FirstOrDefault(i => i.agent_id == userId);

                 return result != null ? result.bidpoint1 : 0;
             }
         }

        public user Login(string username, string password)
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                using (var md5Hash = MD5.Create())
                {
                    string hashPass = GetMd5Hash(md5Hash, password);

                    return context.users.FirstOrDefault(i => i.username == username && i.password == hashPass);
                }
            }
        }

        public user GetUser(int id)
        {
            using (s_dbEntities context=new s_dbEntities())
            {
                return context.users.FirstOrDefault(i => i.slno == id);
            }
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
         {

             // Convert the input string to a byte array and compute the hash. 
             byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

             // Create a new Stringbuilder to collect the bytes 
             // and create a string.
             StringBuilder sBuilder = new StringBuilder();

             // Loop through each byte of the hashed data  
             // and format each one as a hexadecimal string. 
             for (int i = 0; i < data.Length; i++)
             {
                 sBuilder.Append(data[i].ToString("x2"));
             }

             // Return the hexadecimal string. 
             return sBuilder.ToString();
         }
    }

    public class AllLevelComm
    {
        public float AgentBetComm { get; set; }
        public float MasterBetComm { get; set; }
        public float SrmasterBetComm { get; set; }
          public float HouseBetComm { get; set; }
          public float AdminBetComm { get; set; }
    }
}
