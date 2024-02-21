using System;
using System.Linq;
using Facturi.Repository;
using Facturi.Service;

namespace Facturi
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            
            var documentRepository = new DocumentRepository("C:\\Users\\adria\\RiderProjects\\Facturi\\Facturi\\Data\\documente.txt");
            var facturaRepository = new FacturaRepository("C:\\Users\\adria\\RiderProjects\\Facturi\\Facturi\\Data\\facturi.txt");
            var achizitieRepository = new AchizitieRepository("C:\\Users\\adria\\RiderProjects\\Facturi\\Facturi\\Data\\achizitii.txt");
            
            var documentService = new DocumentService(documentRepository, facturaRepository, achizitieRepository);
            

            // Cerinta 1: Afișare documente emise în anul 2023
            Console.WriteLine("Cerinta 1: Documente emise in 2023");
            var documentsInYear2023 = documentService.GetDocumentsInYear(2023);
            foreach (var document in documentsInYear2023)
            {
                Console.WriteLine($"Nume: {document.Nume}, Data Emitere: {document.DataEmitere}");
            }
            Console.WriteLine();

            // Cerinta 2: Afișare facturi scadente în luna curentă
            Console.WriteLine("Cerinta 2: Facturi scadente in luna curenta");
            var dueInCurrentMonth = documentService.GetDueInCurrentMonth();
            foreach (var factura in dueInCurrentMonth)
            {
                Console.WriteLine($"Nume: {documentRepository.FindOne(factura.Id).Nume}, Data Scadenta: {factura.DataScadenta}");
            }
            Console.WriteLine();

            // Cerinta 3: Afișare facturi cu cel puțin 3 produse achiziționate
            Console.WriteLine("Cerinta 3: Facturi cu cel putin 3 produse achizitionate");
            var facturiWith3Products = documentService.GetFacturiWithAtLeast3Products();
            foreach (var factura in facturiWith3Products)
            {
                Console.WriteLine($"Nume: {documentRepository.FindOne(factura.Id).Nume}, Numar Produse: {factura.Achizitii.Sum(a => a.Cantitate)}");
            }
            Console.WriteLine();

            // Cerinta 4: Afișare achiziții din categoria Utilities
            Console.WriteLine("Cerinta 4: Achizitii din categoria Utilities");
            var utilitiesAchizitii = documentService.GetUtilitiesAchizitii();
            foreach (var achizitie in utilitiesAchizitii)
            {
                Console.WriteLine($"Produs: {achizitie.Produs}, Nume Factura: {documentRepository.FindOne(achizitie.IdDocument).Nume}");
            }
            Console.WriteLine();

            // Cerinta 5: Afișare categoria de facturi cu cele mai multe cheltuieli
            Console.WriteLine("Cerinta 5: Categoria de facturi cu cele mai multe cheltuieli");
            var mostExpensiveCategory = documentService.GetCategoryWithMostExpenses();
            Console.WriteLine($"Categoria: {mostExpensiveCategory}");
        }
    }
}