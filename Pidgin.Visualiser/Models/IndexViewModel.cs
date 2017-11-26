using System.Collections.Generic;

namespace Pidgin.Visualiser.Models
{
    public class IndexViewModel
    {
        public IEnumerable<string> LoadedAssemblies { get; }
        public string ParserName { get; }
        public string Input { get; }
        public int? Pos { get; }

        public IndexViewModel(IEnumerable<string> loadedAssemblies, string parserName, string input, int? pos)
        {
            LoadedAssemblies = loadedAssemblies;
            ParserName = parserName;
            Input = input;
            Pos = pos;
        }
    }
}