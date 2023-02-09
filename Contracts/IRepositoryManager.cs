namespace Contracts
{
    public interface IRepositoryManager
    {
        ITestRepository TestRepository { get; }
        INoteRepository NoteRepository { get; }
        IUserRepository UserRepository { get; }   
        void Save();

    }
}