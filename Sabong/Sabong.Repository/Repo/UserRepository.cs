using System;
using System.Collections.Generic;
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

    public class AnnoucementRepository
    {
        //select * from announcement WHERE (date >= DATE_SUB(CURDATE(), INTERVAL 9 DAY)) order by date DESC,slno desc
        public IQueryable GetAll()
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                var result = from announcement in context.announcements
                    where announcement.date>= DateTime.Now.AddDays(-9)
                    orderby announcement.date descending 

                    select announcement;
              //  return result.ToList();
                return result;

            }
        }
    }
    public class TransactionRepository
    {
        //$alltrans=mysql_query("select t.* from `transaction` t, fight_assign f where t.`date`='$date' and t.`playerid`='$_SESSION[useridval]' and t.matchno=f.slno and f.winner_cockid=0 and f.cancelmatch=0")or die();
        //public List<transaction> GetAllTransaction(int userId, int matchId)
        //{
        //    using (s_dbEntities context = new s_dbEntities())
        //    {
        //       // var result = context.transactions.Where(i=>i.playerid==userId&& i.matchno=matchId&& );

        //       // return result;

        //    }
        //}

        //IEnumerable<string> query = from employee in employees
        //                            join student in students
        //                            on new { employee.FirstName, employee.LastName }
        //                            equals new { student.FirstName, student.LastName }
        //                            select employee.FirstName + " " + employee.LastName;

        //var dealercontacts = from contact in DealerContact
        //                     join dealer in Dealer on contact.DealerId equals dealer.ID
        //                     select contact;

    }
    public class UserRepository
    {
        //select `currency_type` from `user` where `slno`='$userid'----> get currency
        //select `balance` from `openning_balance` where `date` like '%$date%' and `userid`='$userid'
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

         public user Login(string username,string password)
         {


             using (s_dbEntities context = new s_dbEntities())
             {

                 using (MD5 md5Hash = MD5.Create())
                 {
                     string hashPass = GetMd5Hash(md5Hash, password);

                     return context.users.FirstOrDefault(i => i.username == username && i.password==hashPass);
                 }
                 
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
}
