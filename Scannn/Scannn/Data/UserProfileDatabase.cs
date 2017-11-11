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
    public class UserProfileDatabase
    {
        readonly SQLiteAsyncConnection database;

        public UserProfileDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<UserProfile>().Wait();
        }

        public Task<List<UserProfile>> GetItemsAsync()
        {
            return database.Table<UserProfile>().ToListAsync();
        }

        /*
        public Task<List<TodoItem>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }
        */

        public async void DeleteAllAsync()
        {
            await database.DropTableAsync<UserProfile>();

            database.CreateTableAsync<UserProfile>().Wait();
        }

        public Task<int> SaveItemAsync(UserProfile item)
        {
                Debug.WriteLine("Insert database: " + item.sessionid);
                return database.InsertAsync(item);
        }

        public Task<UserProfile> GetItemAsync(string userid)
        {
            return database.Table<UserProfile>().Where(i => i.id == userid).FirstOrDefaultAsync();
        }
    }
}
