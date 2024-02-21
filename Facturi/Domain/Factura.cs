using System;
using System.Collections.Generic;

namespace Facturi.Domain
{
    public class Factura : Document
    {
        public DateTime DataScadenta { get; set; }
        public List<Achizitie> Achizitii { get; set; }
        public Categoria Categorie { get; set; }
        
    }
}