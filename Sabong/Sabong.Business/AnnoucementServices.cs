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
           List<AnnoucementBO> retval = new List<AnnoucementBO>();
           Repository.Repo.AnnoucementRepository repo=new AnnoucementRepository();
           var annouce=repo.GetAll();
           foreach (var announcement in annouce)
           {
               AnnoucementBO addValue=new AnnoucementBO();
               addValue.id = announcement.slno;
               addValue.Date = announcement.date.ToString("dd-MM-yyyy");
               addValue.Subject = announcement.subject;

               addValue.ArenaName = repo.GetArenaNameByDate(announcement.date.ToString("dd-MM-yyyy"));

               retval.Add(addValue);
           }
           return retval;
       }
    }
}
