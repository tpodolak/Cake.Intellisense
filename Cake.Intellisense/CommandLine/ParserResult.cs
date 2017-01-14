using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cake.MetadataGenerator.CommandLine
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
