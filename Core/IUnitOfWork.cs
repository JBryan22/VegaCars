using System.Threading.Tasks;

namespace Vega_New.Core
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}