﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DbFirst.DTO;

namespace DbFirst.Services
{
    public interface IBookDbService
    {
        Task<IList<BookDTO>> GetBooksListAsync();

        Task AddBookAsync(BookDTO bookDTO);

        Task UpdateBookAsync(BookDTO bookDTO);
    }
}
