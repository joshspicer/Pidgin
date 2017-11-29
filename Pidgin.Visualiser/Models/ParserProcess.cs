using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using Pidgin.Diagnostics.Events;

namespace Pidgin.Visualiser.Models
{
    class ParserProcess
    {
        private readonly object _parser;
        public string Name { get; }
        private readonly BlockingCollection<int> _input;
        private readonly BlockingCollection<object> _output;
        public string Input { get; private set; }
        public int? Pos { get; private set; }
        public bool IsRunning { get; private set; }

        public ParserProcess(object parser, string name)
        {
            _parser = parser;
            Name = name;
            _input = new BlockingCollection<int>(1);
            _output = new BlockingCollection<object>(1);
        }

        public static void Initialise(string parserExpr)
        {
            var pieces = parserExpr.Split('.');
            var propertyName = pieces[pieces.Length - 1];
            var typeName = string.Join(".", pieces.Take(pieces.Length - 1));

            var type = AssemblyList
                .LoadedAssemblies
                .Select(a => a.GetType(typeName))
                .First(x => x != null);

            Instance = new ParserProcess(type.GetProperty(propertyName).GetValue(null), parserExpr);
        }

        public void SetInput(string input)
        {
            Input = input;
        }

        public void Run()
        {
            var thread = new Thread(
                () =>
                {
                    typeof(Pidgin.Diagnostics.ParserExtensions)
                        .GetMethods()
                        .Single(m => m.Name == "ParseDebug" && m.GetGenericArguments().Length == 1)
                        .MakeGenericMethod(_parser.GetType().GenericTypeArguments[1])
                        .Invoke(null, new object[] { _parser, Input, new VisualiserEventHandler(_input, _output), null });
                }
            )
            {
                IsBackground = true
            };
            thread.Start();
            IsRunning = true;
            var newPos = _input.Take();
            Pos = newPos;
        }

        public void Continue()
        {
            _output.Add(new object());
            var newPos = _input.Take();
            Pos = newPos;
        }

        public static ParserProcess Instance { get; private set; }
    }
}