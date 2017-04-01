using System.Collections.Generic;

namespace Cake.Intellisense.CommandLine
{
    public class ParserResult<T> where T : class
    {
        public T Result { get; }

        public IList<ParserError> Errors { get; }

        public ParserResult(T result, IList<ParserError> errors)
        {
            Result = result;
            Errors = errors;
        }
    }
}
