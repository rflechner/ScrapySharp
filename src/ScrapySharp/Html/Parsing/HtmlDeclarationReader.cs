using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using ScrapySharp.Extensions;
using ScrapySharp.Html.Dom;
using System.Linq;

namespace ScrapySharp.Html.Parsing
{
    public class HtmlDeclarationReader
    {
        private readonly List<Word> words;
        private int position;

        public HtmlDeclarationReader(CodeReader reader)
        {
            words = new List<Word>();

            SkipSpaces = false;

            while (!reader.End)
            {
                var w = reader.ReadWord();
                words.Add(w);
            }
        }

        public bool End
        {
            get { return position >= words.Count - 1; }
        }

        public TagDeclaration ReadTagDeclaration()
        {
            var w = ReadWord();
            if (w == null)
                return null;

            if (w.IsToken() && (w == Tokens.TagBegin || w == Tokens.CloseTagDeclarator) && !GetNextWord().IsWhiteSpace)
            {
                if (w == Tokens.Doctype)
                {
                    if (char.IsLetterOrDigit(GetNextWord(), 0))
                        return ReadDoctype(w);
                    if (GetNextWord().Value != null && GetNextWord().Value.StartsWith("--"))
                        return ReadComment(w);
                }

                var element = new TagDeclaration
                {
                    Words = new List<Word> {w},
                    Attributes = new NameValueCollection()
                };

                w = ReadWord();
                element.Words.Add(w);
                element.Name = w;

                if (element.Name == Tokens.CloseTag)
                {
                    w = ReadWord();
                    element.Words.Add(w);
                    element.Name = w;
                }

                do
                {
                    SkipSpaces = true;
                    
                    w = ReadWord();
                    element.Words.Add(w);
                    if (IsTagDeclarationEnd(w))
                        break;
                    var attributeName = w.Value;
                    w = ReadWord();
                    element.Words.Add(w);
                    if (IsTagDeclarationEnd(w))
                    {
                        if (!attributeName.IsToken())
                            element.Attributes.Add(attributeName, attributeName);
                        break;
                    }

                    if (w.Value == Tokens.Assign)
                    {
                        w = ReadWord();
                        element.Words.Add(w);
                        if (IsTagDeclarationEnd(w))
                            break;
                        element.Attributes.Add(attributeName, w.Value);
                    }
                    else
                        element.Attributes.Add(attributeName, attributeName);

                } while (!End && w != Tokens.TagBegin && w != Tokens.TagEnd);

                SkipSpaces = false;
                element.Type = GetDeclarationType(element.Words);

                return element;
            }
            
            return ReadTextElement(w);
        }

        private TagDeclaration ReadComment(Word word)
        {
            var wordList = new List<Word>();
            var w = word;

            wordList.Add(w);

            while (!End)
            {
                w = ReadWord();
            
                if (w == "--" && GetNextWord() == Tokens.TagEnd)
                    break;
                
                wordList.Add(w);
            }

            return new TagDeclaration
            {
                InnerText = string.Join(string.Empty, wordList.Skip(2).Select(i => i.QuotedValue)),
                Words = wordList,
                Type = DeclarationType.Comment,
                Name = "--"
            };
        }

        private DeclarationType GetDeclarationType(List<Word> wordList)
        {
            if (wordList.Count < 3)
                return DeclarationType.TextElement;

            if (wordList.Last() != Tokens.TagEnd)
                return DeclarationType.TextElement;

            if (wordList[0] == Tokens.CloseTagDeclarator)
                return DeclarationType.CloseTag;
            
            if (wordList[0] == Tokens.TagBegin)
            {
                if (wordList[1] == Tokens.CloseTag)
                    return DeclarationType.CloseTag;

                if (wordList[wordList.Count - 2] == Tokens.CloseTag)
                    return DeclarationType.SelfClosedTag;

                return DeclarationType.OpenTag;
            }

            return DeclarationType.TextElement;
        }

        private TagDeclaration ReadDoctype(Word word)
        {
            var wordList = new List<Word>();
            var w = word;

            wordList.Add(w);

            SkipSpaces = true;

            while (!End && GetNextWord() != Tokens.TagBegin && w != Tokens.TagEnd)
            {
                w = ReadWord();
                wordList.Add(w);

                if (w.Value.Equals("DOCTYPE", StringComparison.InvariantCultureIgnoreCase))
                    SkipSpaces = false;
            }

            SkipSpaces = false;

            return new TagDeclaration
            {
                InnerText = string.Join(string.Empty, wordList.Select(i => i.QuotedValue)),
                Words = wordList,
                Type = DeclarationType.SelfClosedTag,
                Name = "DOCTYPE"
            };
        }

        private TagDeclaration ReadTextElement(Word word)
        {
            var wordList = new List<Word>();
            var w = word;

            wordList.Add(w);

            while (!End && GetNextWord() != Tokens.TagBegin && GetNextWord() != Tokens.TagEnd && GetNextWord() != Tokens.TagBegin)
            {
                w = ReadWord();
                wordList.Add(w);
            }

            return new TagDeclaration
                       {
                           InnerText = string.Join(string.Empty, wordList.Select(i => i.QuotedValue)),
                           Words = wordList,
                           Type = DeclarationType.TextElement
                       };
        }
        
        private bool IsTagDeclarationEnd(Word w)
        {
            return End || w == Tokens.TagBegin || w == Tokens.TagEnd;
        }

        public Word GetNextWord(int count = 1)
        {
            if ((position + count - 1) >= words.Count)
                return null;
            return words[(position + count - 1)];
        }

        public Word GetPreviousChar()
        {
            if (position <= 1)
                return null;
            return words[position - 2];
        }

        public Word ReadWord()
        {
            if (SkipSpaces)
            {
                while (!End)
                {
                    var w = words[position++];
                    if (!w.IsWhiteSpace)
                        return w;
                }
            }

            return End ? null : words[position++];
        }

        public bool SkipSpaces { get; set; }
    }
}