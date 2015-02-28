using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabong.Business
{
    public class FetchNews
    {
        private static FetchNews _instance;
        public static FetchNews Instance
        {
            get
            {
                if (_instance==null)
                {
                    _instance=new FetchNews();
                }
                return _instance;
            }
        }

        private bool Started = false;

        public void Start()
        {
            if (!Started)
            {
                Started = true;
                InternalLoop();
            }
        }

        void InternalLoop()
        {
            //match
            //match confirm
            //Running
            //chart
            
        }
    }
}
