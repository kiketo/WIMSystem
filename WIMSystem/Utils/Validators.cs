using System.Collections;

namespace Utils
{
    public static class Validators
    {
        public static bool IsLengthValid(string stringForChecking, int min, int max)
        {
            if (stringForChecking.Length < min || stringForChecking.Length > max)
            {
                return false;
            }
            return true;
        }

        public static bool IsValidQuantityOfObjects(ICollection list, int min)
        {
            if (list.Count < min)
            {
                return false;
            }
            return true;
        }

        public static bool IsNullValue(object objectForChecking)
        {
            if (objectForChecking == null)
            {
                return true;
            }
            return false;
        }
    }
}

