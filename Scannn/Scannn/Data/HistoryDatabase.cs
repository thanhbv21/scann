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
    public class HistoryDatabase
    {
        readonly SQLiteAsyncConnection database;

        public HistoryDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<HistoryScanItem>().Wait();
        }

        public Task<List<HistoryScanItem>> GetItemsAsync()
        {
            return database.Table<HistoryScanItem>().ToListAsync();
        }

        /*
        public Task<List<TodoItem>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
        }
        */

        public async void DeleteAllAsync()
        {
            await database.DropTableAsync<HistoryScanItem>();

            database.CreateTableAsync<HistoryScanItem>().Wait();
        }

        public Task<int> SaveItemAsync(HistoryScanItem item)
        {
            if (item.ID != 0)
            {
                Debug.WriteLine("Update database: " + item.itemcode + " | "+ item.itemname);
                return database.UpdateAsync(item);
            }
            else
            {
                Debug.WriteLine("Insert database: " + item.itemcode + " | " + item.itemname);
                return database.InsertAsync(item);
            }
        }

        public Task<HistoryScanItem> GetItemAsync(string itemcode)
        {
            return database.Table<HistoryScanItem>().Where(i => i.itemcode == itemcode).FirstOrDefaultAsync();
        }

    }
}
