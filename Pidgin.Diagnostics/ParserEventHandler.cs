using Pidgin.Diagnostics.Events;

namespace Pidgin.Diagnostics
{
    public class ParserEventHandler<TToken> : IParserEventHandler<TToken>
    {
        public virtual void OnBacktrack(BacktrackEvent evt)
        {
        }

        public virtual void OnBookmark(BookmarkEvent evt)
        {
        }

        public virtual void OnConsumeToken(ConsumeTokenEvent<TToken> evt)
        {
        }

        public virtual void OnDiscardBookmark(DiscardBookmarkEvent evt)
        {
        }

        public virtual void OnStartParse(StartParseEvent evt)
        {
        }
    }
}
