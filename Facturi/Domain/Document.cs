using System;

namespace Facturi.Domain
{
    public class Document : Entity<string>
    {
        public string Nume { get; set; }
        public DateTime DataEmitere { get; set; }
    }
}