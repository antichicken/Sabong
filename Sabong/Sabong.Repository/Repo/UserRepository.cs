using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
    public class MatchRepository
    {
       // $query=mysql_query("select * from `view_matchdetail` where `fslno`='$matchno' and `fdate`='$date' and `status`!='1' and `cancelmatch`!='1'");
        //$que=mysql_query("select * from `match_createstart` where `enddate`='0000-00-00'"); ---> lay ngay
        public view_matchdetail GetCurrentMatch()
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                

                var dateStart = context.match_createstart.FirstOrDefault(i => i.enddate == DateTime.ParseExact("0000-00-00", "yyyy-MM-dd", CultureInfo.InvariantCulture));

                if (dateStart == null)
                    return null;

                string CreatedDate = dateStart.create_date.ToString("yyyy-MM-dd");
                return
                    context.view_matchdetail.FirstOrDefault(
                        i => i.winner_cockid == 0 && i.status != 1 && i.cancelmatch != 1 && i.fdate == CreatedDate);
                

                //var result = from viewMatchdetail in context.view_matchdetail
                //    where viewMatchdetail.status != 1 &&
                //          viewMatchdetail.winner_cockid == 0 &&
                //          viewMatchdetail.cancelmatch != 1
                //    select viewMatchdetail;
                //return result;
            }
        }


        //$query=mysql_query("select case when winner_cockid=-1 then 'DRAW' else case when cock_id=winner_cockid then cock_type else  against_type end end as winner  from fight_assign where `date`='$date' and cancelmatch='0' and winner_cockid !=0 order by slno");

        public IEnumerable GetFightAssignsByDate(string date)
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                var result = from fightAssign in context.fight_assign
                    where fightAssign.date == date &&
                          fightAssign.cancelmatch == 0 &&
                          fightAssign.winner_cockid != 0
                    orderby fightAssign.slno

                    select new
                           {
                               
                              ChickenWin  =(fightAssign.winner_cockid ==-1)? "DRAW" :(fightAssign.cock_id==fightAssign.winner_cockid)? "BANKER":"PLAYER"
                           };
                return result.ToList();


            }
           
        }
    }

    public class OddRepository
    {
        public oddsdiff_calc GetOddsdiffCalcByMatchId(int matchId)
        {
            var xxx = new oddsdiff_calc();
            return xxx;
        }
    }

     public class UserRepository
    {
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
