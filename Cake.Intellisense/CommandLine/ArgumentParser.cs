using System.Collections.Generic;
using System.IO;
using Cake.Intellisense.CommandLine.Interfaces;
using CommandLine;

namespace Cake.Intellisense.CommandLine
{
    public class ArgumentParser : IArgumentParser
    {
        private readonly Parser _parser;

        public ArgumentParser(TextWriter textWriter)
        {
            _parser = new Parser();
        }

        public ParserResult<T> Parse<T>(string[] arguments) where T : class, new()
        {
            var parsedResult = new T();
            var errors = new List<ParserError>();

            if (!_parser.ParseArguments(arguments, parsedResult))
            {
                errors.Add(new ParserError { Value = "Unable to parse arguments" });
                return new ParserResult<T>(null, errors);
            }

            return new ParserResult<T>(parsedResult, errors);
        }
    }
}