using Contracts;
using Entities;

namespace Repository
{
    public class TestRepository : RepositoryBase<TestEntity>, ITestRepository
    {
        public TestRepository(PersonalNotesDbContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public string TestMethod()
        {
            return "This is a test method from TestRepository";
        }

        public string TestMethodFromBase()
        {
            throw new NotImplementedException();
        }
    }
}