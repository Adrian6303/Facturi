using System;
using System.Collections.Generic;
using System.IO;
using Facturi.Domain;

namespace Facturi.Repository
{
    public class DocumentRepository : IRepository<string, Document>
    {
        private List<Document> documents;

        public DocumentRepository(string filePath)
        {
            documents = new List<Document>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');

                        if (parts.Length == 3)
                        {
 
                            string idDocument = parts[0].Trim();
                            string nume = parts[1].Trim();
                            DateTime dataEmitere = DateTime.Parse(parts[2].Trim());
                            
                            Document document = new Document
                            {
                                Id = idDocument,
                                Nume = nume,
                                DataEmitere = dataEmitere,
                            };

                            documents.Add(document);
                        }
                        else
                        {
                            Console.WriteLine($"Linie invalida in fisierul {filePath}: {line}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la citirea din fisierul {filePath}: {ex.Message}");
            }
        }

        public IEnumerable<Document> FindAll()
        {
            return documents;
        }
        
        public Document FindOne(string idDocument)
        {
            foreach (var d in documents)
            {
                if (d.Id.Equals(idDocument))
                    return d;
            }

            return null;
        }
    }
}