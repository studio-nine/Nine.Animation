namespace Nine.Animation
{
    using System;
    using System.Collections.Concurrent;
    using System.Reflection;

    static class PropertyAccessor
    {
        private static readonly ConcurrentDictionary<Tuple<Type, string>, Func<object, object>> getters = new ConcurrentDictionary<Tuple<Type, string>, Func<object, object>>();
        private static readonly ConcurrentDictionary<Tuple<Type, string>, Action<object, object>> setters = new ConcurrentDictionary<Tuple<Type, string>, Action<object, object>>();

        public static Func<T> Getter<T>(object target, string name)
        {
            var getter = GetterByType(target.GetType(), name);
            return new Func<T>(() => (T)getter(target));
        }

        public static Action<T> Setter<T>(object target, string name)
        {
            var setter = SetterByType(target.GetType(), name);
            return new Action<T>(x => setter(target, x));
        }

        public static Func<object, object> GetterByType(Type type, string name)
        {
            return getters.GetOrAdd(Tuple.Create(type, name), CreateGetter);
        }

        public static Action<object, object> SetterByType(Type type, string name)
        {
            return setters.GetOrAdd(Tuple.Create(type, name), CreateSetter);
        }

        private static Func<object, object> CreateGetter(Tuple<Type, string> key)
        {
            var type = key.Item1.GetTypeInfo();

            var property = type.GetDeclaredProperty(key.Item2);
            if (property != null && property.CanRead && property.CanWrite && property.GetIndexParameters().Length <= 0)
            {
                return new Func<object, object>(property.GetValue);
            }

            var field = type.GetDeclaredProperty(key.Item2);
            if (field != null)
            {
                return new Func<object, object>(field.GetValue);
            }

            return null;
        }

        private static Action<object, object> CreateSetter(Tuple<Type, string> key)
        {
            var type = key.Item1.GetTypeInfo();

            var property = type.GetDeclaredProperty(key.Item2);
            if (property != null && property.CanRead && property.CanWrite && property.GetIndexParameters().Length <= 0)
            {
                return new Action<object, object>(property.SetValue);
            }

            var field = type.GetDeclaredProperty(key.Item2);
            if (field != null)
            {
                return new Action<object, object>(field.SetValue);
            }

            return null;
        }
    }
}