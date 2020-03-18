namespace musics_api.Repository
{
    public interface IUnitOfWork
    {
        IMusicRepository MusicRepository { get; }
        IAuthorRepository AuthorRepository { get; }
        void Commit();
    }
}