using System.Threading;

namespace JG.FinTechTest.Data
{
    public interface IRepository
    {
        int Save(Donation donation, CancellationToken cancellation);
    }
}