namespace Pidgin.Diagnostics.Events
{
    public sealed class BacktrackEvent
    {
        public int BookmarkPos { get; }
        public SourcePos BookmarkSourcePos { get; }

        internal BacktrackEvent(int bookmarkPos, SourcePos bookmarkSourcePos)
        {
            BookmarkPos = bookmarkPos;
            BookmarkSourcePos = bookmarkSourcePos;
        }
    }
}