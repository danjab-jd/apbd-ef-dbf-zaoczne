using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DbFirst.DTO;
using DbFirst.Entities;
using Microsoft.EntityFrameworkCore;

namespace DbFirst.Services
{
    public class BookDbService : IBookDbService
    {
        private readonly jdContext _context;

        public BookDbService(jdContext context)
        {
            _context = context;
        }

        public async Task<IList<BookDTO>> GetBooksListAsync()
        {
            /*
             * await _context.Trips
             * .Include(x => x.ClientTrips).ThenInclude(x => x.IdClientNavigation)             * 
             */

            /*
             * await _context.ClientTrips
             * .Include(x => x.IdClientNavigation)
             * .Include(x => x.IdTripNavigation)
             */

            return await _context
                .Books
                .Include(x => x.IdAuthorNavigation)
                .Select(x => new BookDTO 
                {
                    IdBook = x.IdBook,
                    Title = x.Title,
                    Author = new()
                    {
                        IdAuthor = x.IdAuthor,
                        FirstName = x.IdAuthorNavigation.Name,
                        LastName = x.IdAuthorNavigation.Surname
                    }
                })
                .ToListAsync();
        }

        public async Task AddBookAsync(BookDTO bookDTO)
        {
            Author authorFromDb = await _context
                .Authors
                .SingleOrDefaultAsync(x => x.IdAuthor == bookDTO.Author.IdAuthor);

            if(authorFromDb == null)
            {
                // Logika gdy autora nie ma w bazie
                return;
            }

            await _context.Books.AddAsync(new Book 
            { 
                IdBook = bookDTO.IdBook,
                Title = bookDTO.Title,
                //IdAuthor = bookDTO.Author.IdAuthor
                IdAuthorNavigation = authorFromDb
                //IdAuthorNavigation = new()
                //{
                //}
            });

            await _context.SaveChangesAsync();
        }

        public async Task UpdateBookAsync(BookDTO bookDTO)
        {
            Book bookFromDb = await _context
                .Books
                .SingleOrDefaultAsync(x => x.IdBook == bookDTO.IdBook);

            if(bookFromDb == null)
            {
                // Logika gdy w bazie nie ma książki o podanym ID
                return;
            }

            bookFromDb.Title = bookDTO.Title;
            //...

            await _context.SaveChangesAsync();
        }
    }
}
