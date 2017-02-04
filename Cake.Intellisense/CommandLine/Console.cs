using System;
using System.ComponentModel;

namespace Cake.MetadataGenerator.CommandLine
{
    public class ConsoleReader : IConsoleReader, IConsoleWriter
    {
        public string Read()
        {
            return Console.ReadLine();
        }

        public bool TryRead<T>(out T result)
        {
            var line = Read();

            var typeConverter = TypeDescriptor.GetConverter(typeof(T));
            if (typeConverter.CanConvertTo(typeof(T)))
            {
                result = (T)typeConverter.ConvertTo(line, typeof(T));
                return true;
            }

            result = default(T);
            return false;
        }

        public void WriteLine(string value)
        {
            Console.WriteLine(value);
        }
    }
}