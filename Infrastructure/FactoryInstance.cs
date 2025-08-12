using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Infrastructure
{
    //public static class FactoryInstance
    //{
    //    private static ContextRegistry contextRegistry;
    //    private static XmlDocument xmlDocument;
         

    //}
    public class RegisteredType
    {
        public Type Type { get; set; }
        public Lifetime Lifetime { get; set; }
        public object SingletonInstance { get; set; }
    }

    public sealed class FactoryInstance
    {
        private static readonly Dictionary<string, RegisteredType> _types = new Dictionary<string, RegisteredType>();
        [ThreadStatic] private static Dictionary<string, object> _scopedInstances;
        //static FactoryInstance()
        //{
        //    FactoryInstance.LoadFromXml();
        //}
        public static void LoadFromXml(string filePath)
        {
            var doc = new XmlDocument();
            doc.Load(filePath);
            if (doc != null)
            {
                foreach (XmlNode node in doc.SelectNodes("/objects/object"))
                {
                    string name = node.Attributes["name"].Value;
                    string typeName = node.Attributes["type"].Value;
                    string lifetimeStr = node.Attributes["lifetime"]?.Value ?? "transient";

                    Type type = Type.GetType(typeName);
                    if (type == null)
                        throw new Exception($"Type not found: {typeName}");

                    Lifetime lifetime = lifetimeStr switch
                    {
                        "Singleton" => Lifetime.Singleton,
                        "Scoped" => Lifetime.Scoped,
                        _ => Lifetime.Transient
                    };

                    _types[name] = new RegisteredType
                    {
                        Type = type,
                        Lifetime = lifetime
                    };
                }
            }

        }

        public static T GetObject<T>(string name, params object[] args)
        {
            if (!_types.TryGetValue(name, out var reg))
                throw new Exception($"Type not registered: {name}");

            switch (reg.Lifetime)
            {
                case Lifetime.Singleton:
                    if (reg.SingletonInstance == null)
                        reg.SingletonInstance = Activator.CreateInstance(reg.Type, args);
                    return (T)reg.SingletonInstance;

                case Lifetime.Scoped:
                    _scopedInstances ??= new Dictionary<string, object>();
                    if (!_scopedInstances.ContainsKey(name))
                        _scopedInstances[name] = Activator.CreateInstance(reg.Type, args);
                    return (T)_scopedInstances[name];

                case Lifetime.Transient:
                default:
                    return (T)Activator.CreateInstance(reg.Type, args);
            }
        }

        public static void ClearScope()
        {
            _scopedInstances?.Clear();
        }
    }
}
