using System;
using System.Collections.Generic;

namespace Sabong.Repository
{
    internal class RepositoryFactory : IRepositoryFactory
    {
        private readonly IDictionary<Type, Type> _maps = new Dictionary<Type, Type>();
        private readonly IDictionary<Type, object> _cache = new Dictionary<Type, object>();

        public void Add<T, TImpl>() where TImpl : T
        {
            _maps[typeof (T)] = typeof (TImpl);
        }

        public T Get<T>()
        {
            Type t;
            if (!_maps.TryGetValue(typeof (T), out t))
            {
                throw new Exception(string.Format("Not FOUND implementation of {0}", typeof (T)));
            }

            object impl;

            if (!_cache.TryGetValue(typeof (T), out impl))
            {
                var r = (T) Activator.CreateInstance(t);

                _cache[typeof (T)] = r;
                return r;
            }

            return (T) impl;
        }

      
    }
}