﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
using Cake.Intellisense.CommandLine.Interfaces;
using CommandLine;

namespace Cake.Intellisense.CommandLine
{
    public class ArgumentParser : IArgumentParser
    {
        private readonly Parser _parser;

        public ArgumentParser(TextWriter textWriter)
        {
            _parser = new Parser(settings => settings.HelpWriter = textWriter);
        }

        public ParserResult<T> Parse<T>(string[] arguments) where T : class, new()
        {
            T parsedResult = null;
            var errors = new List<ParserError>(0);
            var result = _parser.ParseArguments<T>(arguments);

            result.WithParsed(obj => parsedResult = obj)
                  .WithNotParsed(err => errors = err.Select(val => new ParserError { Type = val.Tag.ToString(), Value = val.ToString() }).ToList());

            return new ParserResult<T>(parsedResult, errors);
        }
    }
}