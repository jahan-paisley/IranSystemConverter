using System;

namespace IranSystemConvertor
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public static partial class ConvertToIranSystem
    {
        public static IEnumerable<byte> IranSystem(string windows1256EncodedString)
        {
            Encoding arabic = Encoding.GetEncoding(1256);
            var stringBytes = arabic.GetBytes(windows1256EncodedString.Trim());
            var newStringBytes = new List<byte>();

            if (IsNumber(windows1256EncodedString))
                stringBytes = stringBytes.Reverse().ToArray();

            for (var i = 0; i < stringBytes.Length; ++i)
            {
                byte charByte = stringBytes[i];
                if (charByte < 128)
                    newStringBytes.Add(charByte);
                else
                {
                    if (charByte == 225 && stringBytes[i + 1] == 199)
                    {
                        newStringBytes.Add(242);
                        i++;
                    }
                    else if (charByte == 229 )
                    {
                        switch (GetForm(stringBytes, charByte, i))
                        {
                            case GlyphForm.Final:
                            case GlyphForm.Isolated:
                                newStringBytes.Add(249);
                                break;
                            case GlyphForm.Medial:
                                newStringBytes.Add(250);
                                break;
                            case GlyphForm.Initial:
                                newStringBytes.Add(251);
                                break;
                        }
                    }
                    else if (charByte == 236 || charByte == 237)
                    {
                        switch (GetForm(stringBytes, charByte, i))
                        {
                            case GlyphForm.Final:
                                newStringBytes.Add(252);
                                break;
                            case GlyphForm.Isolated:
                                newStringBytes.Add(253);
                                break;
                            case GlyphForm.Medial:
                            case GlyphForm.Initial:
                                newStringBytes.Add(254);
                                break;
                        }
                    }
                    else if (MultipleFormCharacters.Contains(charByte))
                    {
                        GlyphForm glyphForm = GetForm(stringBytes, charByte, i);
                        byte equivalentChar;
                        if (charByte == 225 || charByte == 229)
                            equivalentChar = (byte)(IranSystemCharactersMapper[charByte] + glyphForm);
                        else
                            equivalentChar = (byte)(IranSystemCharactersMapper[charByte] + 1);
                        newStringBytes.Add(equivalentChar);
                    }
                    else if (IranSystemCharactersMapper.ContainsKey(charByte))
                    {
                        newStringBytes.Add(IranSystemCharactersMapper[charByte]);
                    }
                    else
                    {
                        if (charByte != '\r')
                            newStringBytes.Add(charByte);
                    }
                }
            }
            newStringBytes.Reverse();
            return newStringBytes;
        }

        
        private static bool IsNumber(string str)
        {
            int i;
            return int.TryParse(str, out i);
        }

        private static GlyphForm GetForm(byte[] s, byte ch, int pos)
        {
            if (MultipleFormCharacters.Contains(ch))
            {
                if (pos == 0)
                {
                    return GlyphForm.Initial;
                }
                else if (pos == s.Length - 1)
                {
                    byte previous = s[pos - 1];
                    if (MultipleFormCharacters.Contains(previous))
                    {
                        return GlyphForm.Final;
                    }
                    else
                    {
                        return GlyphForm.Isolated;
                    }
                }
                else
                {
                    byte previous = s[pos - 1];
                    byte next = s[pos + 1];
                    if (MultipleFormCharacters.Contains(previous) && MultipleFormCharacters.Contains(next))
                    {
                        return GlyphForm.Medial;
                    }
                    else if (MultipleFormCharacters.Contains(previous))
                    {
                        return GlyphForm.Final;
                    }
                    else if (MultipleFormCharacters.Contains(next))
                    {
                        return GlyphForm.Initial;
                    }
                    else
                    {
                        return GlyphForm.Isolated;
                    }
                }

            }
            else
            {
                return GlyphForm.Isolated;
            }
        }

        enum GlyphForm
        {
            Isolated = 0, // گ‎
            Final = 1, // ـگ‎
            Medial = 2, // ـگـ‎
            Initial = 3, // گـ‎
        }
    }
}
