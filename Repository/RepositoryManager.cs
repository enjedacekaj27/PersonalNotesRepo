using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private Entities.PersonalNotesDbContext _repositoryContext;
        private ITestRepository _testRepository;
        private INoteRepository _noteRepository;
        private IUserRepository _userRepository;
       


        public RepositoryManager(Entities.PersonalNotesDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public ITestRepository TestRepository
        {
            get
            {
                if (_testRepository == null)
                    _testRepository = new TestRepository(_repositoryContext);

                return _testRepository;
            }
        }

        public INoteRepository NoteRepository
        {
            get
            {
                if (_noteRepository == null)
                {
                    _noteRepository = new NoteRepository(_repositoryContext);
                }

                return _noteRepository;
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_repositoryContext);

                return _userRepository;
            }
        }

    

        public void Save() => _repositoryContext.SaveChanges();


    }
}