namespace Pidgin.Diagnostics.Events
{
    public sealed class DiscardBookmarkEvent
    {
        public int BookmarkPos { get; }
        public SourcePos BookmarkSourcePos { get; }

        internal DiscardBookmarkEvent(int bookmarkPos, SourcePos bookmarkSourcePos)
        {
            BookmarkPos = bookmarkPos;
            BookmarkSourcePos = bookmarkSourcePos;
        }
    }
}