using AutoMapper;
using Entities.DTO;
using Entities.Helper;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Contracts;

namespace PersonalNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class NoteController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IRepositoryManager _repository;

        public NoteController(
            IMapper mapper,
            IRepositoryManager repositoryManager
           )
        {
            _mapper = mapper;
            _repository = repositoryManager;
        }


        //krijimi i nje note
        [HttpPost("AddNote")]
        //   [Authorize]
        public async Task<ActionResult> AddNote([FromBody] NoteDTO addNoteDTO)
        {


            var currentUser = HttpContext?.User.Claims.FirstOrDefault()?.Value ?? "1";       
            var note = _mapper.Map<Note>(addNoteDTO);
            {
                note.Title = addNoteDTO.Title;
                note.Description = addNoteDTO.Description;
                note.DateCreated = DateTime.Now;
                note.UserID = Convert.ToInt32(currentUser);


            };
            _repository.NoteRepository.Create(note);

            _repository.Save();
            return Ok(note);
        }

        // Lexojme te gjithe notes nga db
        [HttpGet("GetAllNotes")]
        public ActionResult GetAllNotes([FromQuery] PagesParameter pagesParameter)
        {

            var notes = _repository.NoteRepository.GetAllNotes(pagesParameter);

            var metadata = new
            {
                notes.TotalCount,
                notes.PageSize,
                notes.CurrentPage,
                notes.TotalPages,
                notes.HasNext,
                notes.HasPrevious
            };
            if (notes == null)
            {
                return NotFound("You dont have any note on db");
            }
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
   


            var noteDto = _mapper.Map<IEnumerable<AllNotesDTO>>(notes);
            return Ok(noteDto);

        }

        //Lexojme notes sipas id 
        [HttpGet("GetNoteById/{ID}")]
     
        public IActionResult GetNoteById(int ID)
        {
            var noteObject = _repository.NoteRepository.GetNoteById(ID);

            if (noteObject == null)
            {
                return NotFound($"Note with id {ID} not found on db");
            }
            var noteObjectDto_id = _mapper.Map<AllNotesDTO>(noteObject);
            return Ok(noteObjectDto_id);
        }

        // bejme update te dhenat e notes
        [HttpPut("Update/{ID}")]
    
        public IActionResult UpdateNote(int ID, NoteDTO updateNoteDTO)
        {
            if (updateNoteDTO == null)
            {
                return BadRequest("Note object is null");
            }
            var existingNote = _repository.NoteRepository.GetNoteById(ID);
            if (existingNote == null)
            {
                return BadRequest();
            }
            //_mapper.Map(existingNote, updateNoteDTO);
            existingNote.Title = updateNoteDTO.Title;
            existingNote.Description = updateNoteDTO.Description;
            existingNote.DateModified = DateTime.Now;
            _repository.NoteRepository.Update(existingNote);
            
            _repository.Save();
            return NoContent();
        }

        //Fshijme nje note sipas id
        [HttpDelete("Delete/{ID}")]
        public IActionResult DeleteById(int ID)
        {

            var note = _repository.NoteRepository.GetNoteById(ID);

            if (note == null)
            {
                return NotFound($"Note with id {ID} not found on db");
            }
            _repository.NoteRepository.Delete(note);
            _repository.Save();
            return NoContent();

        }
    }
}
