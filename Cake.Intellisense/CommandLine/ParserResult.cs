using System.Collections.Generic;

namespace Cake.Intellisense.CommandLine
{
    public class ParserResult<T> where T : class
    {
        public ParserResult(T result, IList<ParserError> errors)
        {
            Result = result;
            Errors = errors;
        }

        public T Result { get; private set; }

        public IList<ParserError> Errors { get; private set; }
    }
}
