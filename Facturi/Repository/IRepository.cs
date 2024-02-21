﻿using System.Collections.Generic;
using Facturi.Domain;

namespace Facturi.Repository
{
    public interface IRepository<ID, E> where E : Entity<ID>
    {
        IEnumerable<E> FindAll();
        E FindOne(ID idFactura);
    }
}