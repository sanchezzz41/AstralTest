﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AstralTest.Database;
using AstralTest.Domain.Entities;
using AstralTest.Domain.Interfaces;
using AstralTest.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AstralTest.Domain.Services
{

    /// <summary>
    /// Класс для работы с заметками
    /// </summary>
    public class NoteService : INoteService
    {
        private readonly DatabaseContext _context;

        public NoteService(DatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Note> Notes
        {
            get { return _context.Notes
                    .Include(x => x.Master)
                    .ToList();
            }
        }

        /// <summary>
        /// Добавляет заметку в бд
        /// </summary>
        /// <returns></returns>
        public async Task<Guid> AddAsync(NoteModel noteModel, Guid idMaster)
        {
            if (noteModel == null || idMaster == null)
            {
                throw new NullReferenceException("Объект равен Null");
            }
            var resUser = await _context.Users.SingleOrDefaultAsync(x => x.UserId == idMaster);

            if (resUser == null)
            {
                throw new NullReferenceException("Объект равен Null");
            }

            var result = new Note {Text = noteModel.Text, IdUser = idMaster};

            //note.Master = resUser;
            //note.MasterId = resUser.Id;
            await _context.Notes.AddAsync(result);
            await _context.SaveChangesAsync();
            return result.NoteId;
        }

        /// <summary>
        /// Удаляет заметку из бд
        /// </summary>
        /// <param name="idNote"></param>
        /// <returns></returns>
        public async Task DeleteAsync(Guid idNote)
        {

            var result = await _context.Notes.SingleOrDefaultAsync(x => x.NoteId == idNote);

            if (result == null)
            {
                throw new NullReferenceException("Объект равен Null");
            }
            _context.Notes.Remove(result);
            await _context.SaveChangesAsync();

        }

        /// <summary>
        /// Изменяет заметку
        /// </summary>
        /// <param name="newNote"></param>
        /// <param name="idNote"></param>
        /// <returns></returns>
        public async Task EditAsync(NoteModel newNote, Guid idNote)
        {
            if (newNote == null || idNote == null)
            {
                throw new NullReferenceException("Объект равен Null");
            }

            var result = await _context.Notes.SingleOrDefaultAsync(x => x.NoteId == idNote);
            if (result == null)
            {
                throw new NullReferenceException("Объект равен Null");
            }

            result.Text = newNote.Text;
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Получает заметки из БД
        /// </summary>
        /// <returns></returns>
        public async Task<List<Note>> GetAsync()
        {
            var result = await _context.Notes
                .Include(x => x.Master)
                .ToListAsync();
            return result;
        }

    }
}
