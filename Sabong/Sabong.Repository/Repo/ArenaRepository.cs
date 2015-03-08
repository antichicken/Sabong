using System;
using System.Collections.Generic;
using System.Linq;
using Sabong.Repository.EntityModel;
using Sabong.Util;

namespace Sabong.Repository.Repo
{
    public class ArenaRepository
    {
        public List<arena> GetAll()
        {
            try
            {
                using (var context = new s_dbEntities())
                {
                    var result = from are in context.arenas
                                 select are;
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                ex.LogError();
                return null;
            }
        }

        //select * from announcement WHERE (date >= DATE_SUB(CURDATE(), INTERVAL 9 DAY)) order by date DESC,slno desc

        //select arena_name from arena,fight_assign where fight_assign.date='$dt' and fight_assign.arena=arena.id and fight_assign.`cancelmatch`!=1 order by fight_assign.slno desc
        public string GetArenaNameByDate(string date)
        {
            try
            {
                using (var context = new s_dbEntities())
                {
                    var arena = (from are in context.arenas
                                 join fight in context.fight_assign on are.id equals fight.arena
                                 where fight.date == date &&
                                 fight.cancelmatch != 1
                                 orderby fight.slno descending
                                 select are.arena_name).FirstOrDefault();
                    return arena;
                }
            }
            catch (Exception ex)
            {
                ex.LogError("ArenaRepository.GetArenaNameByDate: " + date);
                return string.Empty;
            }
            //  string datestring = date.ToString("dd-MM-yyyy");
            
        }
    }
}