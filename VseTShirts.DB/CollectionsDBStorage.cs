using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VseTShirts.DB.Models;

namespace VseTShirts.DB
{
    public class CollectionsDBStorage : ICollectionsStorage
    {
        private readonly DatabaseContext _dbContext;
        public CollectionsDBStorage(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Collection>> GetAllAsync()
        {
            return await _dbContext.Collections.ToListAsync();
        }
        public async Task AddAsync(Collection collection)
        {
            if (collection.Description == null)
                collection.Description = " ";
            if (collection.Name == null)
                return;
            if (_dbContext.Collections.Where(c => c.Name == collection.Name).Count()>0)
                return;
            _dbContext.Collections.Add(collection);
            await _dbContext.SaveChangesAsync();
        }
        public async Task ClearAsync()
        {
            await _dbContext.Collections.ExecuteDeleteAsync();
        }
        public async Task EditAsync(int id, Collection newCollection)
        {
            var lastCollection = await _dbContext.Collections.FirstOrDefaultAsync(c => c.Id == id);
            if (lastCollection != null)
            {
                lastCollection.Description = newCollection.Description;
                lastCollection.Name = newCollection.Name;
            }
            else
            {
                throw new InvalidOperationException("Коллекция не найдена!!!!");
            }
            await _dbContext.SaveChangesAsync();

        }
        public async Task DeleteAsync(string name)
        {
            var collectionToDelete = await _dbContext.Collections.FirstOrDefaultAsync(c => c.Name == name);
            if (collectionToDelete != null)
            {
                _dbContext.Collections.Remove(collectionToDelete);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(string name, string newName , string description)
        {
            if (_dbContext.Collections.Where(c => c.Name == newName).Count() > 0 || newName == null || name == null)
                return;
            if (description == null)
                description = " ";
            var collection = _dbContext.Collections.First(c => c.Name == name);
            if (collection != null)
            {
                collection.Name = newName;
                collection.Description = description;
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
