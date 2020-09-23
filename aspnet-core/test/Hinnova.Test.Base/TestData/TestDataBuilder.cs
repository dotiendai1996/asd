using Hinnova.EntityFrameworkCore;

namespace Hinnova.Test.Base.TestData
{
    public class TestDataBuilder
    {
        private readonly HinnovaDbContext _context;
        private readonly int _tenantId;

        public TestDataBuilder(HinnovaDbContext context, int tenantId)
        {
            _context = context;
            _tenantId = tenantId;
        }

        public void Create()
        {
            new TestOrganizationUnitsBuilder(_context, _tenantId).Create();
            new TestSubscriptionPaymentBuilder(_context, _tenantId).Create();
            new TestEditionsBuilder(_context).Create();

            _context.SaveChanges();
        }
    }
}
