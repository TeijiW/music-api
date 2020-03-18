using webapi.Context;

namespace musics_api.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private MusicRepository _musicRepo;
        private AuthorRepository _authorRepo;
        public DatabaseContext _context;

        public UnitOfWork(DatabaseContext context)
        {
            _context = context;
        }

        public IMusicRepository MusicRepository
        {
            get
            {
                return _musicRepo = _musicRepo ?? new MusicRepository(_context);
            }
        }

        public IAuthorRepository AuthorRepository
        {
            get
            {
                return _authorRepo = _authorRepo ?? new AuthorRepository(_context);
            }
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}