using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Pidgin.Diagnostics;
using Pidgin.Diagnostics.Events;

namespace Pidgin.Visualiser.Models
{
    internal class VisualiserEventHandler : ParserEventHandler<char>
    {
        private readonly BlockingCollection<int> _output;
        private readonly BlockingCollection<object> _input;

        public VisualiserEventHandler(BlockingCollection<int> output, BlockingCollection<object> input)
        {
            _output = output;
            _input = input;
        }

        public override void OnStartParse(StartParseEvent evt)
        {
            _output.Add(0);
            _input.Take();
        }

        public override void OnConsumeToken(ConsumeTokenEvent<char> evt)
        {
            _output.Add(evt.NewPos);
            _input.Take();
        }

        public override void OnBacktrack(BacktrackEvent evt)
        {
            _output.Add(evt.BookmarkPos);
            _input.Take();
        }
    }
}