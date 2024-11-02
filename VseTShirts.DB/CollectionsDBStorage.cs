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
            if (collection.Description == null)
                collection.Description = " ";
            if (collection.Name == null)
                return;
            if (_dbContext.Collections.Where(c => c.Name == collection.Name).Count()>0)
                return;
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
        public void Delete(string name)
        {
            var collectionToDelete = _dbContext.Collections.FirstOrDefault(c => c.Name == name);
            if (collectionToDelete != null)
            {
                _dbContext.Collections.Remove(collectionToDelete);
            }
            _dbContext.SaveChanges();
        }

        public void Edit(string name, string newName , string description)
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
                _dbContext.SaveChanges();
            }
        }
    }
}
