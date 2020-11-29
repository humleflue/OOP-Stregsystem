using System;
using System.Collections.Generic;

namespace DashSystem.Controllers
{
    public interface IController<T>
    {
        List<T> Fetch();
        void Update(T data);
        void Add(T data);
        void Delete(T data);
        uint GetNextUID();
    }
}