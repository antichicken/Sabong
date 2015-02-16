using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Sabong.Repository;
using Sabong.Repository.Repo;


namespace Sabong.Test
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void Text1()
        {
            Repository.Repo.UserRepository user=new UserRepository();
            var xx= user.Login("t","qq123456");
        }
    }
}
