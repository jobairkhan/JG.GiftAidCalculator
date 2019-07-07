using System.Threading;
using System.Threading.Tasks;

namespace JG.FinTechTest.Data
{
    //I'll not follow TDD for this class intentionally
    public class Repository : IRepository
    {
        private readonly GiftAidDbContext _context;

        public Repository(GiftAidDbContext context)
        {
            _context = context;
        }
        public async Task<int> Save(Donation donation, CancellationToken cancellation)
        {
            cancellation.ThrowIfCancellationRequested();
            await _context.AddAsync(donation, cancellation);
            await _context.SaveChangesAsync(cancellation);
            return donation.Id;
        }
    }
}