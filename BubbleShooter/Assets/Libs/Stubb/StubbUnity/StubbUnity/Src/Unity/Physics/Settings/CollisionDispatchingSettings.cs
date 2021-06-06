using StubbUnity.Unity.Physics.Dispatchers;
using UnityEngine;

namespace StubbUnity.Unity.Physics.Settings
{
    public class CollisionDispatchingSettings
    {
        private CollisionDispatchProperties _triggerProperties;
        private CollisionDispatchProperties _collisionProperties;
        private readonly GameObject _gameObject;

        private TriggerEnterDispatcher _triggerEnter;
        private TriggerEnter2DDispatcher _triggerEnter2D;
        private TriggerStayDispatcher _triggerStay;
        private TriggerStay2DDispatcher _triggerStay2D;
        private TriggerExitDispatcher _triggerExit;
        private TriggerExit2DDispatcher _triggerExit2D;

        private CollisionEnterDispatcher _collisionEnter;
        private CollisionEnter2DDispatcher _collisionEnter2D;
        private CollisionStayDispatcher _collisionStay;
        private CollisionStay2DDispatcher _collisionStay2D;
        private CollisionExitDispatcher _collisionExit;
        private CollisionExit2DDispatcher _collisionExit2D;

        public CollisionDispatchingSettings(CollisionDispatchProperties triggerProperties, CollisionDispatchProperties collisionProperties, GameObject gameObject)
        {
            _gameObject = gameObject;
            _triggerProperties = triggerProperties;
            _collisionProperties = collisionProperties;

            _InitDispatchers();
        }

        private void _InitDispatchers()
        {
            if (_triggerProperties.Enter)
                _gameObject.AddComponent<TriggerEnterDispatcher>();
            
            if (_triggerProperties.Enter2D)
                _gameObject.AddComponent<TriggerEnter2DDispatcher>();

            if (_triggerProperties.Stay)
                _gameObject.AddComponent<TriggerStayDispatcher>();

            if (_triggerProperties.Stay2D)
                _gameObject.AddComponent<TriggerStay2DDispatcher>();

            if (_triggerProperties.Exit)
                _gameObject.AddComponent<TriggerExitDispatcher>();

            if (_triggerProperties.Exit2D)
                _gameObject.AddComponent<TriggerExit2DDispatcher>();

            if (_collisionProperties.Enter)
                _gameObject.AddComponent<CollisionEnterDispatcher>();

            if (_collisionProperties.Enter2D)
                _gameObject.AddComponent<CollisionEnter2DDispatcher>();

            if (_collisionProperties.Stay)
                _gameObject.AddComponent<CollisionStayDispatcher>();

            if (_collisionProperties.Stay2D)
                _gameObject.AddComponent<CollisionStay2DDispatcher>();

            if (_collisionProperties.Exit)
                _gameObject.AddComponent<CollisionExitDispatcher>();

            if (_collisionProperties.Exit2D)
                _gameObject.AddComponent<CollisionExit2DDispatcher>();
        }

        public bool EnableTriggerEnter
        {
            get => _triggerProperties.Enter;
            set
            {
                if (value && !_triggerProperties.Enter)
                {
                    _triggerProperties.Enter = true;
                    _triggerEnter = _gameObject.AddComponent<TriggerEnterDispatcher>();
                }
                else if (!value && _triggerProperties.Enter)
                {
                    _triggerProperties.Enter = false;
                    Object.Destroy(_triggerEnter);
                    _triggerEnter = null;
                }
            }
        }

        public bool EnableTriggerEnter2D
        {
            get => _triggerProperties.Enter2D;
            set
            {
                if (value && !_triggerProperties.Enter2D)
                {
                    _triggerProperties.Enter2D = true;
                    _triggerEnter2D = _gameObject.AddComponent<TriggerEnter2DDispatcher>();
                }
                else if (!value && _triggerProperties.Enter2D)
                {
                    _triggerProperties.Enter2D = false;
                    Object.Destroy(_triggerEnter2D);
                    _triggerEnter2D = null;
                }
            }
        }

        public bool EnableTriggerStay
        {
            get => _triggerProperties.Stay;
            set
            {
                if (value && !_triggerProperties.Stay)
                {
                    _triggerProperties.Stay = true;
                    _triggerStay = _gameObject.AddComponent<TriggerStayDispatcher>();
                }
                else if (!value && _triggerProperties.Stay)
                {
                    _triggerProperties.Stay = false;
                    Object.Destroy(_triggerStay);
                    _triggerStay = null;
                }
            }
        }

        public bool EnableTriggerStay2D
        {
            get => _triggerProperties.Stay2D;
            set
            {
                if (value && !_triggerProperties.Stay2D)
                {
                    _triggerProperties.Stay2D = true;
                    _triggerStay2D = _gameObject.AddComponent<TriggerStay2DDispatcher>();
                }
                else if (!value && _triggerProperties.Stay2D)
                {
                    _triggerProperties.Stay2D = false;
                    Object.Destroy(_triggerStay2D);
                    _triggerStay2D = null;
                }
            }
        }

        public bool EnableTriggerExit
        {
            get => _triggerProperties.Exit;
            set
            {
                if (value && !_triggerProperties.Exit)
                {
                    _triggerProperties.Exit = true;
                    _triggerExit = _gameObject.AddComponent<TriggerExitDispatcher>();
                }
                else if (!value && _triggerProperties.Exit)
                {
                    _triggerProperties.Exit = false;
                    Object.Destroy(_triggerExit);
                    _triggerExit = null;
                }
            }
        }

        public bool EnableTriggerExit2D
        {
            get => _triggerProperties.Exit2D;
            set
            {
                if (value && !_triggerProperties.Exit2D)
                {
                    _triggerProperties.Exit2D = true;
                    _triggerExit2D = _gameObject.AddComponent<TriggerExit2DDispatcher>();
                }
                else if (!value && _triggerProperties.Exit2D)
                {
                    _triggerProperties.Exit2D = false;
                    Object.Destroy(_triggerExit2D);
                    _triggerExit2D = null;
                }
            }
        }

        public bool EnableCollisionEnter
        {
            get => _collisionProperties.Enter;
            set
            {
                if (value && !_collisionProperties.Enter)
                {
                    _collisionProperties.Enter = true;
                    _collisionEnter = _gameObject.AddComponent<CollisionEnterDispatcher>();
                }
                else if (!value && _collisionProperties.Enter)
                {
                    _collisionProperties.Enter = false;
                    Object.Destroy(_collisionEnter);
                    _collisionEnter = null;
                }
            }
        }

        public bool EnableCollisionEnter2D
        {
            get => _collisionProperties.Enter2D;
            set
            {
                if (value && !_collisionProperties.Enter2D)
                {
                    _collisionProperties.Enter2D = true;
                    _collisionEnter2D = _gameObject.AddComponent<CollisionEnter2DDispatcher>();
                }
                else if (!value && _collisionProperties.Enter2D)
                {
                    _collisionProperties.Enter2D = false;
                    Object.Destroy(_collisionEnter2D);
                    _collisionEnter2D = null;
                }
            }
        }

        public bool EnableCollisionStay
        {
            get => _collisionProperties.Stay;
            set
            {
                if (value && !_collisionProperties.Stay)
                {
                    _collisionProperties.Stay = true;
                    _collisionStay = _gameObject.AddComponent<CollisionStayDispatcher>();
                }
                else if (!value && _collisionProperties.Stay)
                {
                    _collisionProperties.Stay = false;
                    Object.Destroy(_collisionStay);
                    _collisionStay = null;
                }
            }
        }

        public bool EnableCollisionStay2D
        {
            get => _collisionProperties.Stay2D;
            set
            {
                if (value && !_collisionProperties.Stay2D)
                {
                    _collisionProperties.Stay2D = true;
                    _collisionStay2D = _gameObject.AddComponent<CollisionStay2DDispatcher>();
                }
                else if (!value && _collisionProperties.Stay2D)
                {
                    _collisionProperties.Stay2D = false;
                    Object.Destroy(_collisionStay2D);
                    _collisionStay2D = null;
                }
            }
        }

        public bool EnableCollisionExit
        {
            get => _collisionProperties.Exit;
            set
            {
                if (value && !_collisionProperties.Exit)
                {
                    _collisionProperties.Exit = true;
                    _collisionExit = _gameObject.AddComponent<CollisionExitDispatcher>();
                }
                else if (!value && _collisionProperties.Exit)
                {
                    _collisionProperties.Exit = false;
                    Object.Destroy(_collisionExit);
                    _collisionExit = null;
                }
            }
        }

        public bool EnableCollisionExit2D
        {
            get => _collisionProperties.Exit2D;
            set
            {
                if (value && !_collisionProperties.Exit2D)
                {
                    _collisionProperties.Exit2D = true;
                    _collisionExit2D = _gameObject.AddComponent<CollisionExit2DDispatcher>();
                }
                else if (!value && _collisionProperties.Exit2D)
                {
                    _collisionProperties.Exit2D = false;
                    Object.Destroy(_collisionExit2D);
                    _collisionExit2D = null;
                }
            }
        }

        public void Dispose()
        {
            EnableTriggerEnter = false;
            EnableTriggerEnter2D = false;
            EnableTriggerStay = false;
            EnableTriggerStay2D = false;
            EnableTriggerExit = false;
            EnableTriggerExit2D = false;

            EnableCollisionEnter = false;
            EnableCollisionEnter2D = false;
            EnableCollisionStay = false;
            EnableCollisionStay2D = false;
            EnableCollisionExit = false;
            EnableCollisionExit2D = false;
            
            _triggerProperties = null;
            _collisionProperties = null;
        }
    }
}