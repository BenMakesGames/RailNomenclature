using System;
using System.Collections.Generic;
using Fare;

namespace RailNomenclature
{
    public static class StringUtil
    {
        public static string XegerGenerate(this String s)
        {
            var xeger = new Xeger(s);
            return xeger.Generate();
        }

        public static int NumberOf(this String s, char needle)
        {
            int count = 0;
            
            foreach (char c in s)
            {
                if (c == needle) count++;
            }

            return count;
        }

        public static int Height(this String s)
        {
            int count = 1;

            foreach (char c in s)
            {
                if (c == '\n')
                    count++;
            }

            return count;
        }

        public static int Width(this String s)
        {
            int maxLength = 0;
            int currentLength = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '\n')
                {
                    if (currentLength > maxLength)
                        maxLength = currentLength;

                    currentLength = 0;
                }
                else
                    currentLength++;
            }

            if (currentLength > maxLength)
                return currentLength;
            else
                return maxLength;
        }
        
        public static string UppercaseFirst(this string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            char[] a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        public static String MaxWidth(this String s, int width)
        {
            if (s.Trim() == "") return s;

            List<String> lines = new List<String>();
            String[] words = s.Trim().Replace("\n", "\n ").Replace("  ", " ").Split(' ');
            String line = "";

            foreach (String word in words)
            {
                if (word.Length == 0) continue;

                if (word[word.Length - 1] == '\n')
                {
                    if (line == "")
                    {
                        lines.Add(word.Trim());
                    }
                    else if ((line + " " + word.Trim()).Length >= width)
                    {
                        lines.Add(line);
                        lines.Add(word.Trim());
                        line = "";
                    }
                    else
                    {
                        lines.Add(line + " " + word.Trim());
                        line = "";
                    }
                }
                else if (line == "")
                {
                    if (word.Length >= width)
                        lines.Add(word);
                    else
                        line = word;
                }
                else // line != ""
                {
                    if (word.Length >= width)
                    {
                        lines.Add(line);
                        lines.Add(word);
                        line = "";
                    }
                    else if ((line + " " + word).Length >= width)
                    {
                        lines.Add(line);
                        line = word;
                    }
                    else
                        line += " " + word;
                }
            }

            if (line != "")
                lines.Add(line);

            return string.Join("\n", lines);
        }
    }
}
