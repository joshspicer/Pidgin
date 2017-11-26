using System;
using Pidgin.Diagnostics.Events;

namespace Pidgin.Diagnostics
{
    public interface IParserEventHandler<TToken>
    {
        void OnStartParse(StartParseEvent evt);
        void OnConsumeToken(ConsumeTokenEvent<TToken> evt);
        void OnBookmark(BookmarkEvent evt);
        void OnDiscardBookmark(DiscardBookmarkEvent evt);
        void OnBacktrack(BacktrackEvent evt);
    }
}
