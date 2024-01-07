using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;

namespace ELO.Utilities
{
    public class Enumeration<TKey> where TKey : IEquatable<TKey>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual TKey ID { get; set; }
        public virtual string Name { get; set; }

        public Enumeration() { }
        public Enumeration(TKey id, string name) { ID = id; Name = name; }

        public static T FromID<T>(TKey id) where T : Enumeration<TKey>, new() => Parse<T>(id, o => o.ID.Equals(id));
        public static T FromName<T>(string name) where T : Enumeration<TKey>, new() => Parse<T>(name, o => o.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        public static T TryFromID<T>(TKey id) where T : Enumeration<TKey>, new() => TryParse<T>(id, o => o.ID.Equals(id));
        public static T TryFromName<T>(string name) where T : Enumeration<TKey>, new() => TryParse<T>(name, o => o.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));

        public static T Parse<T>(object value, Func<T, bool> predicate) where T : Enumeration<TKey>, new() => TryParse<T>(value, predicate) ?? throw new InvalidOperationException($"'{value}' not found on {typeof(T)}");
        public static T TryParse<T>(object value, Func<T, bool> predicate) where T : Enumeration<TKey>, new() => GetAll<T>().FirstOrDefault(predicate);

        public static IEnumerable<T> GetAll<T>() where T : Enumeration<TKey>, new()
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);
            foreach (var info in fields)
            {
                var instance = new T();
                if (info.GetValue(instance) is T locatedValue) yield return locatedValue;
            }
        }

        public override string ToString() => $"{ID} {Name}";

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            return ID.Equals(((Enumeration<TKey>)obj).ID);
        }

        public override int GetHashCode() => ID.GetHashCode();
    }
}
