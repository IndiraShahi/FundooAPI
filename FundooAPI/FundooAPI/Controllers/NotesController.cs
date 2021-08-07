using BuisnessLayer.Interface;
using BuisnessLayer.Services;
using CommonLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FundooAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class NotesController : ControllerBase
    {
        INotesBL NotesBL;

        public NotesController(INotesBL noteBL)
        {
            this.NotesBL = noteBL;

        }
        [HttpPost]
        public ActionResult AddNotes(AddNote notes)
        {
            try
            {
                int userId = GetIdFromToken();
                var noteAdded = NotesBL.CreateNewNote(notes, userId);
                if (noteAdded == true)
                {
                    return Ok(new { Success = true, message = "New Note added Successfully!", data = notes });
                }
                return BadRequest(new { Success = false, message = "Failed to Add New Note to Database" });
            }
            catch (Exception ex)
            {

                return NotFound(new { sucess = false, message = ex.Message });

            }


        }
        private int GetIdFromToken()
        {
            return Convert.ToInt32(User.FindFirst(x => x.Type == "userId").Value);
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                int userId = GetIdFromToken();
                var notes = NotesBL.GetAll(userId);

                if (notes != null)
                {
                    return Ok(new { message = "Showing all the notes", data = notes });
                }
                return BadRequest(new { message = "No notes available" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("Archive")]
        public ActionResult ArchiveNotes()
        {
            try
            {
                int userId = GetIdFromToken();
                var archiveNotes = NotesBL.ArchiveNotes(userId);

                if (archiveNotes != null)
                {
                    return Ok(new { message = "Showing all archive notes", data = archiveNotes });
                }
                return BadRequest(new { message = "No notes available" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("Bin")]
        public ActionResult BinNotes()
        {
            try
            {
                int userId = GetIdFromToken();
                var binNotes = NotesBL.BinNotes(userId);

                if (binNotes != null)
                {
                    return Ok(new { message = "Showing all bin notes", data = binNotes });
                }
                return BadRequest(new { message = "No notes available" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{noteId}/Bin")]
        public ActionResult MoveToBin(int noteId)
        {
            try
            {
                int userId = GetIdFromToken();
                var noteInBin = NotesBL.ToBin(noteId, userId);

                if (noteInBin == true)
                {
                    return Ok(new { message = "Moved to bin" });
                }
                return BadRequest(new { message = "Cannot move to bin" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("{noteId}/Archive")]
        public ActionResult ToArchive(int noteId)
        {
            try
            {
                int userId = GetIdFromToken();
                var noteArchived = NotesBL.ToArchive(noteId, userId);

                if (noteArchived == true)
                {
                    return Ok(new { message = "Moved to archieve" });
                }
                return BadRequest(new { message = "Cannot move to archieve" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{noteId}")]
        public ActionResult DeleteNote(int noteId)
        {
            try
            {
                int userId = GetIdFromToken();
                var noteDeleted = NotesBL.DeleteNote(noteId, userId);

                if (noteDeleted == true)
                {
                    return Ok(new { message = "Notes deleted" });
                }
                return BadRequest(new { message = "Cannot be deleted" });
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}




