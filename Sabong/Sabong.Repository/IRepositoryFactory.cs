using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabong.Repository
{
    public interface IRepositoryFactory
    {
        void Add<T, TImpl>() where TImpl : T;
        T Get<T>();
    }
}