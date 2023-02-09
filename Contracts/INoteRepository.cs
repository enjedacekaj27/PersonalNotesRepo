using Entities.Helper;
using Entities.Models;

namespace Contracts
{
    public interface INoteRepository : IRepositoryBase<Note>
    {
       
        PagedList<Note> GetAllNotes(PagesParameter pagesParameter);
       
        Note GetNoteById(int ID);
    }
   
}
