using System.Threading;
using System.Threading.Tasks;

namespace JG.FinTechTest.Data
{
    public interface IRepository
    {
        Task<int> Save(Donation donation, CancellationToken cancellation);
    }
}