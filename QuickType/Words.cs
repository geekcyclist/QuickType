using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QuickType
{
    /// <summary>
    /// Store a word list and return a random word
    /// </summary>
    public class Words
    {
        //TODO: Modify to read from a dictionary file
        string[] _keyWords = { "bool", "byte", "char", "decimal", "double", "enum", "float", 
                                 "int", "long", "sbyte", "short", "struct", "uint", "ulong", "ushort",
                                 "class", "delegate", "dynamic", "interface", "object", "string",
                                 "var", "void", "params", "ref", "out", "namespace", "using", "extern", "alias",
                                 "abstract", "async", "const", "event", "extern", "in", "out", "override", 
                                 "readonly", "sealed", "static", "unsafe", "virtual", "volitile",
                                 "if", "else", "switch", "case", "do", "for", "foreach", "while", "break", 
                                 "continue", "default", "goto", "return", "yield", "throw", "try", "catch",
                                 "finally", "checked", "unchecked", "fixed", "lock", "from", "where", 
                                 "select", "group", "into", "orderby", "join", "let", "ascending", "descending",
                                 "on", "equals", "by" };


        public string GetRandomWord()
        {
            Random random = new Random();
            return _keyWords[random.Next(_keyWords.Length - 1)];
        }
    
    }
}
