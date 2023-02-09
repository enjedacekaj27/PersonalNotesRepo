using AutoMapper;
using Entities.DTO;
using Entities.Models;

namespace PersonalNotes
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Note, AllNotesDTO>();
            CreateMap<Note,UpdateNoteDTO>();
            CreateMap<UpdateNoteDTO, Note>();
            CreateMap<NoteDTO, Note>();
          
            CreateMap<UserAddDTO, User>();
         


            
        }
    }
    
}
