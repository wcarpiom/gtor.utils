using System;
using System.Collections.Generic;

namespace Gtor.Utils.MapperUtilities
{
    public interface IMapperUtils
    {
        string GetStringTypeByType(Type dataType);
    }
    public class MapperUtils : IMapperUtils
    {
        private static readonly Dictionary<Type, string> DataTypeRepo = new Dictionary<Type, string>
        {
            [typeof(byte)] = "byte",
            [typeof(sbyte)] = "sbyte",
            [typeof(short)] = "int16",
            [typeof(ushort)] = "ushort",
            [typeof(int)] = "int",
            [typeof(uint)] = "uint",
            [typeof(long)] = "long",
            [typeof(ulong)] = "ulong",
            [typeof(float)] = "float",
            [typeof(double)] = "double",
            [typeof(decimal)] = "decimal",
            [typeof(bool)] = "bool",
            [typeof(string)] = "string",
            [typeof(char)] = "char",
            [typeof(Guid)] = "Guid",
            [typeof(DateTime)] = "DateTime",
            [typeof(DateTimeOffset)] = "DateTime",
            [typeof(byte[])] = "byte[]"
        };

        public string GetStringTypeByType(Type dataType)
        {
            if (!DataTypeRepo.ContainsKey(dataType))
            {
                throw new ArgumentException($"Type \"{dataType}\" not recognized as valid data type.");
            }
            return DataTypeRepo[dataType];
        }
    }
}