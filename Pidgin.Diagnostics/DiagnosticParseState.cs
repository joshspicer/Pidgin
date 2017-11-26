using System;
using System.Collections.Generic;
using Pidgin.Diagnostics.Events;
using Pidgin.ParseStates;

namespace Pidgin.Diagnostics
{
    internal sealed class DiagnosticParseState<TToken> : IParseState<TToken>
    {
        private readonly Stack<Positioned<int>> _bookmarks = new Stack<Positioned<int>>();
        private readonly IList<TToken> _input;
        private readonly IParserEventHandler<TToken> _eventHandler;
        private readonly Func<TToken, SourcePos, SourcePos> _posCalculator;
        private int _pos = -1;
        public SourcePos SourcePos { get; private set; } = new SourcePos(1,1);
        public ParseError<TToken> Error { get; set; }

        public DiagnosticParseState(
            IList<TToken> input,
            IParserEventHandler<TToken> eventHandler,
            Func<TToken, SourcePos, SourcePos> posCalculator
        )
        {
            _input = input;
            _eventHandler = eventHandler;
            _posCalculator = posCalculator;
        }

        public void Advance()
        {
            if (_pos >= _input.Count)
            {
                return;
            }
            if (_pos == -1)
            {
                _pos++;
                return;
            }
            var consumedToken = GetCurrent();
            var oldPos = _pos;
            var oldSourcePos = SourcePos;
            _pos++;
            SourcePos = _posCalculator(consumedToken, SourcePos);

            _eventHandler.OnConsumeToken(new ConsumeTokenEvent<TToken>(consumedToken, oldPos, _pos, oldSourcePos, SourcePos));
        }

        public Maybe<TToken> Peek()
        {
            if (_pos >= _input.Count || _pos < 0)
            {
                return Maybe.Nothing<TToken>();
            }
            return Maybe.Just(GetCurrent());
        }

        public void PushBookmark()
        {
            _bookmarks.Push(new Positioned<int>(_pos, SourcePos));
            _eventHandler.OnBookmark(new BookmarkEvent());
        }
        public void PopBookmark()
        {
            _bookmarks.Pop();
            _eventHandler.OnDiscardBookmark(new DiscardBookmarkEvent());
        }
        public void Rewind()
        {
            var bookmark = _bookmarks.Pop();
            _pos = bookmark.Value;
            SourcePos = bookmark.Pos;
            _eventHandler.OnBacktrack(new BacktrackEvent());
        }

        public void Dispose()
        {
        }

        private TToken GetCurrent() => _input[_pos];
    }
}