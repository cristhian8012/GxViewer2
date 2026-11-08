using System;

namespace gxViewer
{
    public class FastStringBuilder
    {
        private char[] chars;

        private int charsLength;

        public char this[int index]
        {
            get
            {
                return chars[index];
            }
            set
            {
                chars[index] = value;
            }
        }

        public int Length
        {
            get
            {
                return charsLength;
            }
            set
            {
                charsLength = value;
            }
        }

        public char[] Chars => chars;

        public FastStringBuilder()
            : this(0)
        {
        }

        public FastStringBuilder(int capacity)
        {
            chars = new char[capacity];
            charsLength = 0;
        }

        public FastStringBuilder(string value)
            : this(value.Length)
        {
            Append(value);
        }

        public void Append(char c)
        {
            if (charsLength >= chars.Length)
            {
                char[] destinationArray = new char[GetExpandSize(1)];
                Array.Copy(chars, 0, destinationArray, 0, chars.Length);
                chars = destinationArray;
            }

            chars[charsLength] = c;
            charsLength++;
        }

        public void Append(char c, int count)
        {
            if (count > 0)
            {
                int num = charsLength;
                if (charsLength + count > chars.Length)
                {
                    char[] destinationArray = new char[GetExpandSize(count)];
                    Array.Copy(chars, 0, destinationArray, 0, chars.Length);
                    chars = destinationArray;
                }

                for (int i = 0; i < count; i++)
                {
                    chars[num] = c;
                    num++;
                }

                charsLength += count;
            }
        }

        public void Append(string s)
        {
            Append(s, 0, s.Length);
        }

        public void Append(string s, int startIndex)
        {
            Append(s, startIndex, s.Length - startIndex);
        }

        public void Append(string s, int startIndex, int count)
        {
            int destinationIndex = charsLength;
            if (charsLength + count > chars.Length)
            {
                char[] destinationArray = new char[GetExpandSize(count)];
                Array.Copy(chars, 0, destinationArray, 0, chars.Length);
                chars = destinationArray;
            }

            s.CopyTo(startIndex, chars, destinationIndex, count);
            charsLength += count;
        }

        public void Append(char[] value, int startIndex, int count)
        {
            int destinationIndex = charsLength;
            if (charsLength + count > chars.Length)
            {
                char[] destinationArray = new char[GetExpandSize(count)];
                Array.Copy(chars, 0, destinationArray, 0, chars.Length);
                chars = destinationArray;
            }

            Array.Copy(value, startIndex, chars, destinationIndex, count);
            charsLength += count;
        }

        public void AppendIndent(int column, int tabSize)
        {
            int count = column / tabSize;
            int count2 = column % tabSize;
            Append('\t', count);
            Append(' ', count2);
        }

        public void Insert(int index, char c)
        {
            if (charsLength >= chars.Length)
            {
                char[] destinationArray = new char[GetExpandSize(1)];
                Array.Copy(chars, 0, destinationArray, 0, chars.Length);
                chars = destinationArray;
            }

            Array.Copy(chars, index, chars, index + 1, charsLength - index);
            chars[index] = c;
            charsLength++;
        }

        public void Insert(int index, string s)
        {
            Insert(index, s, 0, s.Length);
        }

        public void Insert(int index, string s, int startIndex, int count)
        {
            if (charsLength + count > chars.Length)
            {
                char[] destinationArray = new char[GetExpandSize(count)];
                Array.Copy(chars, 0, destinationArray, 0, chars.Length);
                chars = destinationArray;
            }

            Array.Copy(chars, index, chars, index + count, charsLength - index);
            s.CopyTo(startIndex, chars, index, count);
            charsLength += count;
        }

        public void Remove(int startIndex, int length)
        {
            if (length > 0)
            {
                Array.Copy(chars, startIndex + length, chars, startIndex, charsLength - startIndex - length);
                charsLength -= length;
            }
        }

        public override string ToString()
        {
            return new string(chars, 0, charsLength);
        }

        public string ToString(int start, int count)
        {
            return new string(chars, start, count);
        }

        public string ToStringTrimmingLines()
        {
            int num = 0;
            int num2 = charsLength - 1;
            int num3 = 0;
            while (num < num2)
            {
                switch (chars[num3])
                {
                    case '\n':
                        num = num3 + 1;
                        num3++;
                        continue;
                    case '\t':
                    case '\r':
                    case ' ':
                        num3++;
                        continue;
                }

                break;
            }

            while (num2 > num)
            {
                char c = chars[num2];
                if (c != '\n' && c != ' ' && c != '\t' && c != '\r')
                {
                    break;
                }

                num2--;
            }

            if (num <= num2)
            {
                return new string(chars, num, num2 - num + 1);
            }

            return string.Empty;
        }

        private int GetExpandSize(int addCount)
        {
            if (chars.Length == 0)
            {
                if (12 <= addCount)
                {
                    return addCount;
                }

                return 12;
            }

            if (addCount <= chars.Length)
            {
                return chars.Length * 2;
            }

            return chars.Length + addCount;
        }
    }

}