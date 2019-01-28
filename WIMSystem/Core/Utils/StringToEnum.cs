using System;

namespace WIMSystem.Core.Utils
{
    public static class StringToEnum<T> where T : struct
    {
        public static T Convert(string enumToParse)
        {
            T resultInputType = default(T);
            bool enumParseResult = false;
            enumParseResult = Enum.TryParse(enumToParse, true, out resultInputType);
            if (enumParseResult == false)
            {
                throw new ArgumentOutOfRangeException($"Invalid {nameof(T)} type");
            }
            return resultInputType;
        }
    }
}
