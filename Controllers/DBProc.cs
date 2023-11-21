using Tarea2._4.Models;
using SQLite;

namespace Tarea2._4.Controllers
{
    public class DBProc : ContentPage
    {
        private readonly SQLiteAsyncConnection _connection;

        public DBProc()
        { }

        public DBProc(string path)
        {
            _connection = new SQLiteAsyncConnection(path);
            // todos objetos BD
            _connection.CreateTableAsync<Videos>().Wait();
        }

        /*  crud DBProc*/
        // create, read, update, delete


        public Task<int> AddVideo(Videos video)
        {
            if (video.id == 0)
            {
                return _connection.InsertAsync(video);
            }
            else
            {
                return _connection.UpdateAsync(video);
            }
        }

        public Task<List<Videos>> ListVideos()
        {
            return _connection.Table<Videos>().ToListAsync();
        }

        public Task<Videos> GetVideoID(int id)
        {
            return _connection.Table<Videos>()
                .Where(p => p.id == id)
                .FirstOrDefaultAsync();
        }

 
    }
}