using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sabong.Business.BO;
using Sabong.Repository.Repo;

namespace Sabong.Business
{
   public  class AnnoucementServices
    {
       public List<AnnoucementBO> GetAll()
       {
           var retval = new List<AnnoucementBO>();
           var annouceRepo=new AnnoucementRepository();
           var arenaRepo = new ArenaRepository();
           var annouce=annouceRepo.GetAll();
           foreach (var announcement in annouce)
           {
               var addValue=new AnnoucementBO();
               addValue.id = announcement.slno;
               addValue.Date = announcement.date.ToString("dd-MM-yyyy");
               addValue.Subject = announcement.subject;

               addValue.ArenaName = arenaRepo.GetArenaNameByDate(announcement.date.ToString("dd-MM-yyyy"));

               retval.Add(addValue);
           }
           return retval;
       }
    }
}
