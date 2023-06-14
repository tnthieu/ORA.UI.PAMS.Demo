using System.Diagnostics.Metrics;

namespace ORA.UI.PAMS.Demo.Library
{
    public class Library
    {
        public static bool RandomBool()
        {
            var random = new Random().Next(0, 999);
            return (random % 2) == 0;
        }

        public static string RemoveLast(string content, string endText)
        {
            if (content != null)
            {
                if (content.EndsWith(endText))
                {
                    content = content.Remove(content.Length - 1, 1);
                }
            }
            return content;
        }
    }
}