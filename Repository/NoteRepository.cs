using Contracts;
using Entities;
using Entities.Helper;
using Entities.Models;

namespace Repository
{
    public class NoteRepository : RepositoryBase<Note>, INoteRepository
    {
        public NoteRepository(PersonalNotesDbContext repositoryContext)
            : base(repositoryContext)
        {
        }

        public PagedList<Note> GetAllNotes(PagesParameter pagesParameter)
        {
            return PagedList<Note>.ToPagedList(FindAll(false).OrderBy(note => note.ID),
         pagesParameter.PageNumber,
         pagesParameter.PageSize);
        }

        public Note GetNoteById(int id)
        {
            var note = base.FindByCondition(x => x.ID == id, false).FirstOrDefault();
            return note;

        }
    }
}

