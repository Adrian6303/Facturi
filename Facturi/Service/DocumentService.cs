using System;
using System.Collections.Generic;
using System.Linq;
using Facturi.Domain;
using Facturi.Repository;

namespace Facturi.Service
{
    public class DocumentService
    {
        private readonly IRepository<string, Document> documentRepository;
        private readonly IRepository<string, Factura> facturaRepository;
        private readonly IRepository<string, Achizitie> achizitieRepository;
        

        public DocumentService(IRepository<string, Document> documentRepository,
            IRepository<string, Factura> facturaRepository,
            IRepository<string, Achizitie> achizitieRepository)
        {
            this.documentRepository = documentRepository;
            this.facturaRepository = facturaRepository;
            this.achizitieRepository = achizitieRepository;

            foreach (var f in this.facturaRepository.FindAll())
            {
                foreach (var a in this.achizitieRepository.FindAll())
                {
                    if(a.IdDocument == f.Id)
                        f.Achizitii.Add(a);
                }
            }
           
        }
        
        
        
        

        // Cerinta functionala 1
        public IEnumerable<Document> GetDocumentsInYear(int year)
        {
            return documentRepository.FindAll().Where(d => d.DataEmitere.Year == year);
        }

        // Cerinta functionala 2
        public IEnumerable<Factura> GetDueInCurrentMonth()
        {
            var currentMonth = DateTime.Now.Month;
            return facturaRepository.FindAll().Where(f => f.DataScadenta.Month == currentMonth);
        }

        // Cerinta functionala 3
        public IEnumerable<Factura> GetFacturiWithAtLeast3Products()
        {
            return facturaRepository.FindAll().Where(f => f.Achizitii.Sum(a => a.Cantitate) >= 3);
        }

        // Cerinta functionala 4
        public IEnumerable<Achizitie> GetUtilitiesAchizitii()
        {
            return achizitieRepository.FindAll()
            .Where(a => facturaRepository.FindOne(a.IdDocument).Categorie == Categoria.Utilities);
        }


        // Cerinta functionala 5
        public Enum GetCategoryWithMostExpenses()
        {
            var categoryExpenses = facturaRepository.FindAll()
                .GroupBy(f => f.Categorie)
                .Select(g => new { Category = g.Key, TotalExpenses = g.Sum(f => f.Achizitii.Sum(a => a.Cantitate * a.PretProdus)) })
                .OrderByDescending(x => x.TotalExpenses)
                .FirstOrDefault();

            return categoryExpenses?.Category;
        }
    }
}