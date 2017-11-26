using System;
using System.Collections.Generic;

namespace Pidgin.Diagnostics
{
    public static class ParserExtensions
    {
        public static Result<char, T> ParseDebug<T>(
            this Parser<char, T> parser,
            string input,
            IParserEventHandler<char> eventHandler,
            Func<char, SourcePos, SourcePos> calculatePos = null
        ) 
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            return parser.ParseDebug(input.ToCharArray(), eventHandler, calculatePos);
        }

        public static Result<TToken, T> ParseDebug<TToken, T>(
            this Parser<TToken, T> parser,
            IList<TToken> input,
            IParserEventHandler<TToken> eventHandler,
            Func<TToken, SourcePos, SourcePos> calculatePos = null
        ) => Pidgin.ParserExtensions.DoParse(
            parser,
            new DiagnosticParseState<TToken>(
                input,
                eventHandler,
                calculatePos ?? Parser.GetDefaultPosCalculator<TToken>()
            )
        );
    }
}