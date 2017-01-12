﻿using System.Collections.Generic;
using System.Linq;
using CommandLine;

namespace Cake.MetadataGenerator.CommandLine
{
    public class CommandLineArgumentParser : IArgumentParser
    {
        private readonly Parser _parser;

        public CommandLineArgumentParser(Parser parser)
        {
            _parser = parser;
        }

        public ParserResult<T> Parse<T>(string[] arguments) where T : class, new()
        {
            T parsedResult = null;
            List<ParserError> errors = new List<ParserError>(0);

            var result = _parser.ParseArguments<T>(arguments);
            result.WithParsed(obj => parsedResult = obj)
                  .WithNotParsed(err => errors = err.Select(val => new ParserError { Type = val.Tag.ToString(), Value = val.ToString() }).ToList());

            return new ParserResult<T>(parsedResult, errors);
        }
    }
}