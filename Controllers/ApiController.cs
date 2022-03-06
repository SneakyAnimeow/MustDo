using Microsoft.AspNetCore.Mvc;
using MustDo.Data;
using MustDo.Extensions;
using MustDo.Objects;
using static MustDo.Data.Data;
using static MustDo.Setup;

namespace MustDo.Controllers {
    [Route("api/[action]")]
    [ApiController]
    public class ApiController : ControllerBase {
        [HttpPost]
        public IActionResult Register([FromBody] object body) {
            var request = body.ToString().FromJson<RegisterRequest>();
            request.Password = request.Password.ToSaltedMd5(AppConfig.ApiConfig.Salt);

            if (request.Name.Length > 64) return BadRequest("Username cannot be longer than 64 characters.");

            var context = new MustDoContext();

            var list = context.Users
                .Where(user => user.Name == request.Name)
                .ToList();

            if (list.Count > 0) return BadRequest("User already exists.");

            context.Users.Add(new User {
                Name = request.Name,
                PasswordHash = request.Password
            });

            context.SaveChanges();

            var sessionId = Data.Data.GenerateSessionId();
            SessionIds[request.Name] = sessionId;

            return Ok(sessionId);
        }

        [HttpPost]
        public IActionResult Login([FromBody] object body) {
            var request = body.ToString().FromJson<RegisterRequest>();
            request.Password = request.Password.ToSaltedMd5(AppConfig.ApiConfig.Salt);

            if (request.Name.Length > 64) return BadRequest("Username cannot be longer than 64 characters.");

            var context = new MustDoContext();

            var list = context.Users
                .Where(user => user.Name == request.Name)
                .ToList();

            if (list.Count < 1) return NotFound("User does not exist.");

            var user = list[0];
            if (user.PasswordHash != request.Password) return BadRequest("Incorrect password.");

            var sessionId = Data.Data.GenerateSessionId();
            SessionIds[request.Name] = sessionId;

            return Ok(sessionId);
        }

        [HttpGet]
        public List<Note> GetNotes([FromQuery] string sessionId) {
            var output = new List<Note>();
            var names = from session in SessionIds
                        where session.Value == sessionId
                        select session.Key;
            if (names.Count() < 1) return output;

            var name = names.ToList()[0] ?? "";

            var context = new MustDoContext();
            var user = context.Users.First(user => user.Name == name);
            output = context.Notes.Where(note => note.UserId == user.Id).ToList();

            return output;
        }

        [HttpPost]
        public IActionResult AddNote([FromQuery] string sessionId, [FromBody] object body) {
            var names = from session in SessionIds
                        where session.Value == sessionId
                        select session.Key;
            if (names.Count() < 1) return BadRequest(" ");

            var request = body.ToString().FromJson<AddNoteRequest>();

            if (string.IsNullOrWhiteSpace(request.Name)) return BadRequest("Name cannot be empty or null or white space.");

            if (request.Name.Length > 32) return BadRequest("Note name cannot be longer than 32 characters!");

            if (request.Content.Length > 128) return BadRequest("Note content cannot be longer than 128 characters!");

            var context = new MustDoContext();
            var user = context.Users.First(user => user.Name == names.First());

            context.Notes.Add(new Note {
                UserId = user.Id,
                Name = request.Name,
                Content = request.Content
            });
            context.SaveChanges();

            return Ok("Note added.");
        }

        [HttpDelete]
        public IActionResult DeleteNote([FromQuery] string sessionId, [FromQuery] int noteId) {
            var names = from session in SessionIds
                        where session.Value == sessionId
                        select session.Key;
            if (names.Count() < 1) return BadRequest("Session expired.");

            var context = new MustDoContext();
            var user = context.Users.First(user => user.Name == names.First());

            var notes = context.Notes.Where(note => note.Id == noteId)
                .Where(note => note.UserId == user.Id)
                .ToList();
            if (notes.Count() < 1) return BadRequest("Access denied.");

            context.Notes.Remove(notes[0]);

            context.SaveChanges();

            return Ok("Note added.");
        }
    }
}
