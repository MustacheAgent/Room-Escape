using System;
using System.Collections.Generic;

public class ServiceLocator
{
    private readonly Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

    private ServiceLocator() { }

    public static ServiceLocator Current;

    public static void Init()
    {
        Current = new ServiceLocator();
    }

    public T GetService<T>() where T : IService
    {
        var key = typeof(T);

        if (!_services.TryGetValue(key, out var service))
        {
            throw new InvalidOperationException($"Unable to get service of type {key}");
        }

        return (T)service;
    }

    public void Register<T>(T service) where T : IService
    {
        var key = typeof(T);

        if (!_services.TryAdd(key, service))
        {
            throw new InvalidOperationException($"Unable to add service {service}");
        }
    }

    public void Unregister<T>() where T : IService
    {
        var key = typeof(T);

        if (_services.Remove(key))
        {
            throw new InvalidOperationException($"Unable to delete service of type {key}");
        }
    }
}