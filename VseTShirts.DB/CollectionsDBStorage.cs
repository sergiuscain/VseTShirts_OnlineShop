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
        public List<Collection> GetAll()
        {
            return _dbContext.Collections.ToList();
        }
        public void Add(Collection collection)
        {
            _dbContext.Collections.Add(collection);
            _dbContext.SaveChanges();
        }
        public void Clear()
        {
            _dbContext.Collections.ExecuteDelete();
        }
        public void Edit(int id, Collection newCollection)
        {
            var lastCollection = _dbContext.Collections.FirstOrDefault(c => c.Id == id);
            if (lastCollection != null)
            {
                lastCollection.Description = newCollection.Description;
                lastCollection.Name = newCollection.Name;
            }
            else
            {
                throw new InvalidOperationException("Collection not found");
            }
            _dbContext.SaveChanges();

        }
        public void Delete(int id)
        {
            var collectionToDelete = _dbContext.Collections.FirstOrDefault(c => c.Id == id);
            if (collectionToDelete != null)
            {
                _dbContext.Collections.Remove(collectionToDelete);
            }
            _dbContext.SaveChanges();
        }
    }
}
