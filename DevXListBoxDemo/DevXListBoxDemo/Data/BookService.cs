using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevXListBoxDemo.Data
{
    public class BookService
    {
        private List<Book> books = new List<Book>();

        public List<Book> Books
        {
            get 
            {
                if (books.Count == 0)
                {
                    books.Add(new Book()
                    {
                        Id = 1,
                        Title = "Practical Snow Melting for Fun and Profit",
                        Description = "Ever wanted to be in the snow melting business? Well, now you can learn how to make tens of dollars a year melting snow the RIGHT way!"
                    });

                    books.Add(new Book()
                    {
                        Id = 2,
                        Title = "How to Shave a Cat",
                        Description = "Give fluffy a treat by shaving her spear bald! She'll LOVE you for it!"
                    });

                    books.Add(new Book()
                    {
                        Id = 3,
                        Title = "Winning at Tic-Tac-Toe",
                        Description = "Show your bratty kids who's boss! Learn the expert techniques to win every time!"
                    });
                }

                return books;
            }
        }

    }
}
