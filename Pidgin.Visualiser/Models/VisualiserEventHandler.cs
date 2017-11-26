using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Pidgin.Diagnostics;
using Pidgin.Diagnostics.Events;

namespace Pidgin.Visualiser.Models
{
    internal class VisualiserEventHandler : ParserEventHandler<char>
    {
        private readonly BlockingCollection<ConsumeTokenEvent<char>> _output;
        private readonly BlockingCollection<object> _input;

        public VisualiserEventHandler(BlockingCollection<ConsumeTokenEvent<char>> output, BlockingCollection<object> input)
        {
            _output = output;
            _input = input;
        }

        public override void OnConsumeToken(ConsumeTokenEvent<char> evt)
        {
            _output.Add(evt);
            _input.Take();
        }
    }
}