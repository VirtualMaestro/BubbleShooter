using System;

namespace StubbUnity.StubbFramework.Core
{
    public static class ServiceMapper<T> 
    {
        private static T _instance;
        private static Type _type;

        public static void Map(T service)
        {
            _instance = service;
        }

        public static void Map(Type type)
        {
            _type = type;
        }

        public static T Get()
        {
            if (_instance == null && _type != null)
                _instance = (T) Activator.CreateInstance (_type, true);
            return _instance;
        }
    }
}