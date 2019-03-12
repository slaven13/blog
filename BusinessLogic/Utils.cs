using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogic
{
    public static class ContentOperations
    {
        public static string RemoveHtmlTags(string content)
        {
            string htmlTags = "<.*?>";

            return Regex.Replace(content, htmlTags, string.Empty);
        }
    }
}
