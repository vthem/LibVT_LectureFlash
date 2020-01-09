using System.Collections.Generic;
using System.IO;


namespace LectureFlash
{
    public class WordFileReader
    {
        public static List<string> GetWords(string filePath)
        {
            var words = new List<string>(File.ReadAllLines(filePath));
            for (int i = 0; i < words.Count; ++i)
            {
                words[i] = words[i].Trim();
            }
            return words;
        }
    }
}