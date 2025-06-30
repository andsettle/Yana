using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Yana.Client.Shared.Models;
using System;

namespace Yana.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        private static List<Note> _notes = new List<Note>
        {
            new Note
            {
                Id = Guid.NewGuid(),
                Title = "Sample Note",
                Content = "This is a sample note.",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            },
            new Note
            {
                Id = Guid.NewGuid(),
                Title = "Another Note",
                Content = "This is another sample note.",
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            }
        };
        [HttpGet]
        public ActionResult<IEnumerable<Note>> GetNotes()
        {
            return Ok(_notes);
        }

        [HttpGet("{id}")]
        public ActionResult<Note> GetNote(Guid id)
        {
            var note = _notes.FirstOrDefault(n => n.Id == id);

            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }

        [HttpPost]
        public ActionResult<Note> CreateNote([FromBody] Note note)
        {
            note.Id = Guid.NewGuid();
            note.CreatedDate = DateTime.UtcNow;
            note.UpdatedDate = DateTime.UtcNow;
            
            _notes.Add(note);
            return CreatedAtAction(nameof(GetNote), new { id = note.Id }, note);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateNote(Guid id, [FromBody] Note note)
        {
            if (id != note.Id)
            {
                return BadRequest("Note ID mismatch.");
            }

            var existingNote = _notes.FirstOrDefault(n => n.Id == id);

            if (existingNote == null)
            {
                return NotFound();
            }

            existingNote.Title = note.Title;
            existingNote.Content = note.Content;
            existingNote.UpdatedDate = DateTime.UtcNow;
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNote(Guid id)
        { var noteToRemove = _notes.FirstOrDefault(n => n.Id == id);
            if (noteToRemove == null)
            {
                return NotFound();
            }
            _notes.Remove(noteToRemove);
            return NoContent();
        }
    }
}
