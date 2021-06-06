﻿using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace StubbUnity.StubbFramework.Pooling
{
    /// <summary>
    /// Generic pool where T has default constructor.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class Pool<T> : IPool<T>
    {
        public event RemovePool OnRemove;

        private readonly int _initialCapacity;
        private int _freeToPutIndex;
        private T[] _storage;
        private ICreator<T> _creator;
        private Func<T> _createMethod;

        public bool IsEmpty => _freeToPutIndex == 0;
        public bool IsFull => _freeToPutIndex == Size;
        public bool IsDisposed => _storage == null;
        public int Available => IsEmpty ? Size : Size - _freeToPutIndex;
        public int Size => _storage.Length;

        public Func<T> CreateMethod
        {
            get => _createMethod;
            set
            {
                if (_creator != null)
                {
                    _creator.Dispose();
                    _creator = null;
                }

                _createMethod = value;
            }
        }

        public ICreator<T> Creator
        {
            get => _creator;
            set
            {
                _createMethod = null;
                _creator = value;
            }
        }

        public Pool(int initialCapacity = 5)
        {
            _initialCapacity = initialCapacity < 5 ? 5 : initialCapacity;
            _storage = new T[_initialCapacity];
        }

        public Pool(int initialCapacity, Func<T> createMethod, bool preWarm = false) : this(initialCapacity)
        {
            _createMethod = createMethod;

            if (preWarm)
                PreWarm(_initialCapacity);
        }

        public Pool(int initialCapacity, ICreator<T> creator, bool preWarm = false) : this(initialCapacity)
        {
            _creator = creator;

            if (preWarm)
                PreWarm(_initialCapacity);
        }

        public T Get()
        {
            T instance;

            if (IsEmpty)
                instance = _CreateInstance();
            else
            {
                instance = _storage[--_freeToPutIndex];
                _creator?.AfterRestore(instance);
                _storage[_freeToPutIndex] = default;
            }

            return instance;
        }

        public void Put(T item)
        {
            if (IsFull)
                _ResizePool(_storage.Length * 2);

            _creator?.BeforeStore(item);
            _storage[_freeToPutIndex++] = item;
        }

        public void PreWarm()
        {
            PreWarm(Available);
        }

        public void PreWarm(int count)
        {
            if (count > Available)
                _ResizePool(count - Available);

            for (; count > 0; count--)
            {
                Put(_CreateInstance());
            }
        }

        public void Clear(bool shrink = false)
        {
            _freeToPutIndex = 0;

            if (shrink)
            {
                _storage = new T[_initialCapacity];
                return;
            }

            for (var i = 0; i < _storage.Length; i++)
            {
                _storage[i] = default;
            }
        }

        public void Dispose()
        {
            OnRemove?.Invoke(this, typeof(T));
            OnRemove = null;
            _storage = null;
            _creator = null;
            _createMethod = null;
        }

        public override string ToString()
        {
            return $"Size: {Size}, Available: {Available}";
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private T _CreateInstance()
        {
            _CheckIfCreatorsExist();
            return _creator == null ? _createMethod() : _creator.Create();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void _ResizePool(int count)
        {
            var newOne = new T[_storage.Length + count];
            Array.Copy(_storage, newOne, _storage.Length);
            _storage = newOne;
        }

        [Conditional("DEBUG")]
        private void _CheckIfCreatorsExist()
        {
            if (_creator == null && _createMethod == null)
            {
                throw new Exception($"Can't create instance! Creators are not set!");
            }
        }
    }
}