namespace Sabong.Repository
{
    public class RepositoryContainer
    {
        public static readonly IRepositoryFactory Factory = new RepositoryFactory();

        static RepositoryContainer()
        {
            // Factory.Add<IAccountLogRepository, AccountLogRepository>();
           
        }
    }


  
}