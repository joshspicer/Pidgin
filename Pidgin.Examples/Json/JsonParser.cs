using System.Collections.Generic;
using System.Collections.Immutable;
using static Pidgin.Parser;
using static Pidgin.Parser<char>;

namespace Pidgin.Examples.Json
{
    public static class JsonParser
    {
        private static Parser<char, char> Tok(char value)
            => Char(value).Before(SkipWhitespaces);

        private static readonly Parser<char, char> LBrace = Tok('{');
        private static readonly Parser<char, char> RBrace = Tok('}');
        private static readonly Parser<char, char> LBracket = Tok('[');
        private static readonly Parser<char, char> RBracket = Tok(']');
        private static readonly Parser<char, char> Quote = Char('"');
        private static readonly Parser<char, char> Colon = Tok(':');
        private static readonly Parser<char, char> Comma = Tok(',');

        private static readonly Parser<char, string> String =
            Token(c => c != '"')
                .ManyString()
                .Between(Quote)
                .Before(SkipWhitespaces);
        private static readonly Parser<char, IJson> JsonString =
            String.Select<IJson>(s => new JsonString(s));

        private static readonly Parser<char, IJson> JsonNumber =
            Num
                .Before(SkipWhitespaces)
                .Select<IJson>(n => new JsonNumber(n));

        public static Parser<char, IJson> Json { get; } =
            JsonString
                .Or(JsonNumber)
                .Or(Rec(() => JsonArray))
                .Or(Rec(() => JsonObject));

        private static readonly Parser<char, IJson> JsonArray = 
            Json.Separated(Comma)
                .Between(LBracket, RBracket)
                .Select<IJson>(els => new JsonArray(els.ToImmutableArray()));
        
        private static readonly Parser<char, KeyValuePair<string, IJson>> JsonMember =
            String
                .Before(Colon)
                .Then(Json, (name, val) => new KeyValuePair<string, IJson>(name, val));

        private static readonly Parser<char, IJson> JsonObject = 
            JsonMember
                .Separated(Comma)
                .Between(LBrace, RBrace)
                .Select<IJson>(kvps => new JsonObject(kvps.ToImmutableDictionary()));
        
        public static Result<char, IJson> Parse(string input) => Json.Parse(input);
    }
}
