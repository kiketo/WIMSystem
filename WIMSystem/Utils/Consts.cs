using System;
using System.Collections.Generic;
using System.Text;

namespace Utils
{
    internal static class Consts
    {
        internal const int PRODUCT_NAME_MIN_LENGTH = 3;
        internal const int PRODUCT_NAME_MAX_LENGTH = 10;
        internal const int PRODUCT_BRAND_MIN_LENGTH = 2;
        internal const int PRODUCT_BRAND_MAX_LENGTH = 10;

        internal const int PRODUCT_CREAM_NAME_MIN_LENGTH = 3;
        internal const int PRODUCT_CREAM_NAME_MAX_LENGTH = 15;
        internal const int PRODUCT_CREAM_BRAND_MIN_LENGTH = 3;
        internal const int PRODUCT_CREAM_BRAND_MAX_LENGTH = 15;

        internal const int CATEGORY_NAME_MIN_LENGTH = 2;
        internal const int CATEGORY_NAME_MAX_LENGTH = 15;
        internal const int INGREDIENTS_MIN = 1;

        internal const string INVALID_PRODUCT_PRICE = "{0} price cannot be negative! It was passed {1}";
        internal const string INVALID_QUANTITY = "{0} {1} have to be more than {2}! It was passed {3}";
        internal const string INVALID_PRODUCT_QUANTITY = "{0} quantity cannot be negative! It was passed {1}";
        internal static string INVALID_PROPERTY_LENGTH = "{0} {1} can not be less than {2} or more than {3} chars! It was passed {4}";
        internal const string NULL_PROPERTY_NAME = "{0} {1} can not be null!";
        internal const string NULL_OBJECT = "Passed {0} can not be null!";
        internal const string NOT_FOUND_OBJECT = "Passed {0} can not be found!";
        internal const string EMPTY_PRODUCT_LIST = "#No product in this category";


    }
}