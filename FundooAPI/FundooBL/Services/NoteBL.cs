using BuisnessLayer.Interface;
using CommonLayer;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Services
{
    public class NoteBL : INotesBL
    {
        private INoteRL NoteRL;
        public NoteBL(INoteRL noteRL, IConfiguration config)
        {
            NoteRL = noteRL;

        }

        public bool CreateNewNote(AddNote newNote, int userid)
        {
            try
            {
                return this.NoteRL.CreateNewNote(newNote, userid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteNote(int noteId, int userId)
        {
            try
            {
                return NoteRL.DeleteNote(noteId, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Notes> GetAll(int userId)
        {
            try
            {
                return this.NoteRL.GetAll(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Notes> ArchiveNotes(int userId)
        {
            try
            {
                return this.NoteRL.ArchiveNotes(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Notes> BinNotes(int userId)
        {
            try
            {
                return this.NoteRL.BinNotes(userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ToArchive(int noteId, int userId)
        {
            try
            {
                return this.NoteRL.ToArchive(noteId, userId);
            }
            catch
            {
                throw;
            }
        }

        public bool ToBin(int noteId, int userId)
        {
            try
            {
                return this.NoteRL.ToBin(noteId, userId);
            }
            catch
            {
                throw;
            }
        }
    }
}

        