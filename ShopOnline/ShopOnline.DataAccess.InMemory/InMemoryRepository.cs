﻿using ShopOnline.Core.Contracts;
using ShopOnline.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.DataAccess.InMemory
{
    public class InMemoryRepository<T> : IRepository<T> where T : BaseEntity
    {
        ObjectCache cache = MemoryCache.Default;
        List<T> items;
        string className;

        public InMemoryRepository()
        {
            className = typeof(T).Name;
            items = cache[className] as List<T>;
            if (items == null)
            {
                items = new List<T>();
            }
        }

        public void Commit()
        {
            cache[className] = items;
        }
        public void Insert(T t)
        {
            items.Add(t);
        }

        public void Update(T t)
        {
            T TtoUpdate = items.Find(i => i.Id == t.Id);
            if (TtoUpdate != null)
            {
                TtoUpdate = t;
            }
            else
            {
                throw new Exception(className + "Not Found");
            }
        }

        public T Find(string Id)
        {
            T t = items.Find(i => i.Id == Id);
            if (t != null)
            {
                return t;
            }
            else
            {
                throw new Exception(className + "Not Found");
            }
        }

        public IQueryable<T> Collection()
        {
            return items.AsQueryable();
        }

        public void Delete(string Id)
        {
            T TtoDelete = items.Find(i => i.Id == Id);
            if (TtoDelete != null)
            {
                items.Remove(TtoDelete);
            }
            else
            {
                throw new Exception(className + "Not Found");
            }
        }
    }
}
