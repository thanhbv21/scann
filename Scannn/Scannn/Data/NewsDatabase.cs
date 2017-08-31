using Scannn.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scannn.Data
{
    public class NewsDatabase
    {
        readonly SQLiteAsyncConnection database;

        public NewsDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<NewsItem>().Wait();
        }

        public Task<List<NewsItem>> GetItemsAsync()
        {
            return database.Table<NewsItem>().ToListAsync();
        }

        public async Task DeleteAllAsync()
        {
            await database.DropTableAsync<NewsItem>();
            await database.CreateTableAsync<NewsItem>();
        }

        public Task<int> SaveItemsAsync(NewsItem item)
        {
            if (item.ID != 0)
            {
                Debug.WriteLine("Update database: " + item.title + " | " + item.time);
                return database.UpdateAsync(item);
            }
            else
            {
                Debug.WriteLine("Insert database: " + item.title + " | " + item.time);
                return database.InsertAsync(item);
            }
        }

        public Task<NewsItem> GetItemAsync(string title)
        {
            return database.Table<NewsItem>().Where(i => i.title == title).FirstOrDefaultAsync();

        }
    }
}
