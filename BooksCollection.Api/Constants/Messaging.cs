namespace BooksCollection.Api.Constants
{
    public static class Messaging
    {
        // Error message constants to be used by repos and unit tests.
        public static class ErrorMessages
        {
            public const string DuplicateBookExists = "A book with that title already exists.";
            public const string AddBookFailed = "Failed to add book to collection.";
            public const string ModifyBookFailed = "Failed to modify existing book.";
            public const string DeleteBookFailed = "Failed to delete book from collection.";
            public const string BookNotFound = "Book not found.";
            public const string CouldNotFindTitles = "Could not find any titles.";
            public const string UnhandledException = "Unhandled exception has occurred.";
            public const string NoTitlesFound = "Could not find any titles.";
            public const string InvalidBookUid = "Invalid book identifier supplied.";
            public const string InvalidTitleSupplied = "Invalid title supplied.";
        }
    }
}
