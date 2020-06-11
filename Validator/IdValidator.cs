using PrintedItem;

namespace Validator

{
    public static class IdValidator
    {
        public static bool IsValidId(string idString, out int id)
        {        
            return int.TryParse(idString, out id) && id >= 1 && id <= BookFactory.BookId;
        }
    }
}
