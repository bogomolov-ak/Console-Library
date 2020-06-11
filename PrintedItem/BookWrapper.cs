namespace PrintedItem
{
    public class BookWrapper
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int Amount { get; set; }

        public BookWrapper(Book book, int amount)
        {
            BookId = BookFactory.BookId;
            Book = book;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"\nID:{BookId}\n{Book}Количество:{Amount}\n";
        }
    }
}
