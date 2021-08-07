using CommonLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RepositoryLayer.Services
{
    public class NoteRL : INoteRL
    {

        private UserContext fundooContext;
        public NoteRL(UserContext fundooContext, IConfiguration config)
        {
            this.fundooContext = fundooContext;

        }

        public bool CreateNewNote(AddNote newNote, int userId)
        {
            try
            {
                Notes notes = new Notes()
                {
                    Title = newNote.Title,
                    WrittenNote = newNote.WrittenNote,
                    Reminder = newNote.Reminder,
                    Collaborator = newNote.Collaborator,
                    Color = newNote.Color,
                    Image = newNote.Image,
                    IsArchive = newNote.IsArchive,
                    IsPin = newNote.IsPin,
                    IsBin = newNote.IsBin
                };

                User user = new User();

                user = fundooContext.FundooNotes.FirstOrDefault(x => x.UserId == userId);

                notes.User = user;
                if (newNote.Title != null || newNote.WrittenNote != null)
                {
                    fundooContext.Notes.Add(notes);
                    fundooContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Notes> GetAll(int userId)
        {
            try
            {
                var notesOfLoggedInUser = fundooContext.Notes.Where(x => x.IsArchive == false && x.IsBin == false && x.User.UserId == userId).ToList();

                if (notesOfLoggedInUser.Count != 0)
                {
                    return notesOfLoggedInUser;
                }
                return null;
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
                var notesOfLoggedInUser = fundooContext.Notes.Where(x => x.IsArchive == true && x.User.UserId == userId).ToList();

                if (notesOfLoggedInUser.Count != 0)
                {
                    return notesOfLoggedInUser;
                }
                return null;
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
                var notesOfLoggedInUser = fundooContext.Notes.Where(x => x.IsBin == true && x.User.UserId == userId).ToList();

                if (notesOfLoggedInUser.Count != 0)
                {
                    return notesOfLoggedInUser;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool ToBin(int noteId, int userId)
        {
            try
            {
                var note = fundooContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.User.UserId == userId);

                if (note != null)
                {
                    if (note.IsBin == false)
                    {
                        note.IsBin = true;
                        fundooContext.SaveChanges();
                        return true;
                    }
                }
                return false;
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
                var note = fundooContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.User.UserId == userId);

                if (note != null)
                {
                    if (note.IsArchive == false)
                    {
                        note.IsArchive = true;
                        fundooContext.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteNote(int noteId, int userId)
        {
            try
            {
                var noteToBeRemoved = fundooContext.Notes.FirstOrDefault(x => x.NoteId == noteId && x.User.UserId == userId && x.IsBin == true);

                if (noteToBeRemoved != null)
                {
                    fundooContext.Notes.Remove(noteToBeRemoved);
                    fundooContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}




