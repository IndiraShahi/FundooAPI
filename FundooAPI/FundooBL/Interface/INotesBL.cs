using CommonLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuisnessLayer.Interface
{
    public interface INotesBL
    {
        public bool CreateNewNote(AddNote newNote, int userid);
        public bool DeleteNote(int noteId , int userId);
        List<Notes> GetAll(int userId);
        List<Notes> BinNotes(int userId);
        List<Notes> ArchiveNotes(int userId);
        bool ToBin(int noteId, int userId);
        bool ToArchive(int noteId, int userId);
    }
}
