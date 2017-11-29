namespace Pidgin.Diagnostics.Events
{
    public sealed class BookmarkEvent
    {
        public int BookmarkPos { get; }
        public SourcePos BookmarkSourcePos { get; }

        internal BookmarkEvent(int bookmarkPos, SourcePos bookmarkSourcePos)
        {
            BookmarkPos = bookmarkPos;
            BookmarkSourcePos = bookmarkSourcePos;
        }
    }
}