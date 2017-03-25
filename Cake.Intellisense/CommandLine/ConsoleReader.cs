using System;
using System.ComponentModel;
using Cake.Intellisense.CommandLine.Interfaces;

namespace Cake.Intellisense.CommandLine
{
    public class ConsoleReader : IConsoleReader
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
    }
}