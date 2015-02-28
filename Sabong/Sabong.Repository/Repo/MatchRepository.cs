using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Sabong.Repository.EntityModel;

namespace Sabong.Repository.Repo
{
    
    public class MatchRepository
    {

        //select * from `matchending_announcement
        public matchending_announcement GetMatchendingAnnouncement()
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                return context.matchending_announcement.FirstOrDefault();
            }
        }
        //set warning update `fight_assign` set `block_warning_time`='$time',`block_warning_entry_time`='$now' where `slno`='$matchno'
        //fight_assign set block='1' ---> stop bet
        // start match  --select `matchstarttime` where `matchno`='$matchno',`starttime`='$now'
        //Stop match  --update `matchstarttime` set `matchno`='$matchno',`stoptime`='$now' where `matchno`='$matchno  

        //update `fight_assign` set `cancelmatch`='1'   ---> cancel match

        //start time & stop time is varchar
        //block_warning_time float  ..block_warning_entry_time varchar
        //set winner_id or cancel match

   

        public bool IsMatchStart(int slno,out bool matchEnd)
        {
            matchEnd = false;
            using (s_dbEntities context = new s_dbEntities())
            {
                var result = context.matchstarttimes.FirstOrDefault(i => i.matchno == slno);

                if (result == null)
                {
                    //match not start
                    
                    return false;
                }
                if (result.stoptime !="")
                {
                    //match not end
                    matchEnd = true;
                    // return true;
                }

                return true;

            }
        }

//        cocktype and against type is null then dey r not set meron/wala
        public bool IsCockTypeNull(int slno)
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                var result = context.fight_assign.FirstOrDefault(i => i.slno == slno);

                if (result != null && (result.cock_type == null && result.against_type == null))
                {
                    return true;// meron/wala unconfirmed
                }
                return false;
            }
        }

        public view_matchdetail GetMatchStatus(int snlo)
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                var xxx = from fightassign in context.view_matchdetail
                          where
                              fightassign.fslno==snlo
                          select fightassign;


                return xxx.FirstOrDefault();//
            }
        }

        // $query=mysql_query("select * from `view_matchdetail` where `fslno`='$matchno' and `fdate`='$date' and `status`!='1' and `cancelmatch`!='1'");
        //$que=mysql_query("select * from `match_createstart` where `enddate`='0000-00-00'"); ---> lay ngay
        public view_matchdetail GetCurrentMatch(out string confirmCockStatus)
        {
            
            using (s_dbEntities context = new s_dbEntities())
            {
                confirmCockStatus = "meron/wala un-confirmed";
                var dateStart = context.View_match_createGetEndDateNull.FirstOrDefault();

                if (dateStart == null)
                    return null;
                string createdDate = dateStart.create_date.ToString("dd-MM-yyyy");
                var xxx = from fightassign in context.view_matchdetail
                          where
                              fightassign.winner_cockid == 0 &&
                              fightassign.status != 1 &&
                              fightassign.cancelmatch != 1 
                        && fightassign.fdate == createdDate
                    select fightassign; 

                
               var xyz= xxx.FirstOrDefault();
                var retVal = new view_matchdetail();
                //if (xyz != null)
                //{


                //    if (string.IsNullOrEmpty(xyz.cock_type))
                //    {
                        
                //    }
                //    else
                //    {
                //        if (xyz.cock_type.ToLower() == "wala")
                //        {
                //            retVal = xyz;
                //            retVal.cname = xyz.agname;
                //            retVal.cbreedername = xyz.abreedername;
                //            retVal.cock_type = xyz.against_type;
                //            retVal.cimage = xyz.agimage;
                //            retVal.cid = xyz.acid;

                //            retVal.acid = xyz.cid;

                //        }
                //    }
                //}
                
                return xyz;


                //IEnumerable<string> query = from employee in employees
                //                            join student in students
                //                            on new { employee.FirstName, employee.LastName }
                //                            equals new { student.FirstName, student.LastName }
                //                            select employee.FirstName + " " + employee.LastName;

                //var dealercontacts = from contact in DealerContact
                //                     join dealer in Dealer on contact.DealerId equals dealer.ID
                //                     select contact;
            }
        }


        //$query=mysql_query("select case when winner_cockid=-1 then 'DRAW' else case when cock_id=winner_cockid then cock_type else  against_type end end as winner  from fight_assign where `date`='$date' and cancelmatch='0' and winner_cockid !=0 order by slno");

        public List<string> GetFightAssignsByDate()
        {
            using (s_dbEntities context = new s_dbEntities())
            {
                var dateStart = context.View_match_createGetEndDateNull.FirstOrDefault();

                if (dateStart == null)
                    return null;
                string createdDate = dateStart.create_date.ToString("dd-MM-yyyy");

                var result = from fightAssign in context.fight_assign
                             where fightAssign.date == createdDate &&
                          fightAssign.cancelmatch == 0 &&
                          fightAssign.winner_cockid != 0
                    orderby fightAssign.slno

                    select new
                           {
                               
                               ChickenWin  =(fightAssign.winner_cockid ==-1)? "DRAW" :(fightAssign.cock_id==fightAssign.winner_cockid)? "BANKER":"PLAYER"
                           };
                return result.Select(i=>i.ChickenWin.ToLower()).ToList();


            }
           
        }
    }
}