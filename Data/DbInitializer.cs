using BookEater.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BookEater.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Books.Any())
            {
                return;
            }

            LoadBooksFromCsv(context);
        }

        private static void LoadBooksFromCsv(ApplicationDbContext context)
        {
            string csvPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "books.csv");
            
            if (!File.Exists(csvPath)) return;

            var lines = File.ReadAllLines(csvPath, Encoding.UTF8);
            var booksToAdd = new List<Book>();
            var random = new Random();
            string[] genres = { "Fiction", "Fantasy", "Sci-Fi", "Mystery", "History", "Romance", "Thriller", "Classic" };

            foreach (var line in lines.Skip(1).Take(1000)) 
            {
                try
                {
                    var parts = ParseCsvLine(line);
                    if (parts.Length < 11) continue;

                    var title = parts[10].Trim('"');
                    var author = parts[7].Trim('"');

                    if (title.Length > 150) title = title.Substring(0, 147) + "...";

                    booksToAdd.Add(new Book {
                        Title = title,
                        Author = author,
                        Genre = genres[random.Next(genres.Length)], 
                        Rating = 0
                    });
                }
                catch { continue; }
            }

            if (booksToAdd.Any())
            {
                context.Books.AddRange(booksToAdd);
                context.SaveChanges();
            }
        }

        private static string[] ParseCsvLine(string line)
        {
            var result = new List<string>();
            var current = new StringBuilder();
            bool inQuotes = false;
            foreach (char c in line) {
                if (c == '"') inQuotes = !inQuotes;
                else if (c == ',' && !inQuotes) { result.Add(current.ToString()); current.Clear(); }
                else current.Append(c);
            }
            result.Add(current.ToString());
            return result.ToArray();
        }
    }
}