namespace Pidgin.Examples.Json
{
    public class JsonNumber : IJson
    {
        public int Value { get; }

        public JsonNumber(int value)
        {
            Value = value;
        }
    }
}