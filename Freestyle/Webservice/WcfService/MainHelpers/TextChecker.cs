namespace WcfService.MainHelpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class TextChecker
    {
        static public string CheckText(string input)
        {
            if (input == null)
                return null;

            StringBuilder resultBuilder = new StringBuilder(input.Length);
            StringBuilder wordBuilder = new StringBuilder(input.Length);
            Match isLetter = null;
            for (int i = 0; i < input.Length; i++)
            {
                isLetter = Regex.Match(input[i].ToString(), @"^[A-Za-zęóąśłżźćńĘÓĄŚŁŻŹĆŃ]");
                //add letter to word
                if(isLetter.Success){
                    wordBuilder.Append(input[i]);
                }
                else{
                    //add word if exsist
                    addWord(ref wordBuilder, ref resultBuilder);
                    //add char if is not wrong
                    if(!wrongWhiteChars.Contains(input[i]))
                        resultBuilder.Append(input[i]);
                }
            }
            //add word if exsist
            addWord(ref wordBuilder, ref resultBuilder);
            return resultBuilder.ToString();
        }

        static private void addWord(ref StringBuilder wordBuilder, ref StringBuilder resultBuilder)
        {
            if (wordBuilder.Length > 0)
            {
                resultBuilder.Append(wordBuilder.ToString().removeVulgarismes());
                wordBuilder.Clear();
            }
        }

        static private string removeVulgarismes(this string word)
        {
            string toCheckWord = word.prepareToCheck();
            if (Swear.PolishSwears.Contains(toCheckWord))
            {
                StringBuilder builder = new StringBuilder(5);
                builder.Append(word.First());
                builder.Append("***");
                builder.Append(word.Last());
                word = builder.ToString();
            }
            return word;
        }

        private static string prepareToCheck(this string input)
        {
            StringBuilder sb = new StringBuilder(input.Length);
            for (int i = 0; i < input.Length; i++)
            {
                char c = Char.ToLower(input[i]);
                char newValue = ' ';
                polishCharsReplace.TryGetValue(c, out newValue);
                if (newValue != '\0')
                    c = newValue;
                sb.Append(c);
            }
            return sb.ToString();
        }

        private static HashSet<char> wrongWhiteChars = new HashSet<char> { 
            '\t', 
            '\n', 
            '\r' 
        };

        private static Dictionary<char, char> polishCharsReplace = new Dictionary<char, char>(){
            {'ą', 'a'},
            {'ę', 'e'},
            {'ó', 'o'},
            {'ś', 's'},
            {'ł', 'l'},
            {'ż', 'z'},
            {'ź', 'z'},
            {'ć', 'c'},
            {'ń', 'n'}
        };

        
    }
}