using System.Threading;
using System.Threading.Tasks;
using JG.FinTechTest.Data;

namespace JG.FinTechTest
{
    //I'll not follow TDD for this class intentionally
    public class Repository : IRepository
    {
        public async Task<int> Save(Donation donation, CancellationToken cancellation)
        {
            return 1;
        }
    }
}