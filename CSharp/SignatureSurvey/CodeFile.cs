using System.Collections.Generic;
using System.Linq;

namespace SignatureSurvey
{
    public class CodeFile
    {
        public static readonly IEnumerable<char> CLanguageFamily = new[] {';', '{', '}'};
        private readonly string _code;

        public string Name { get; private set; }

        public CodeFile(string name, string code)
        {
            Name = name;
            _code = code;
        }

        public int LinesOfCode
        {
            get { return _code.Count(character => character == '\n'); }
        }

        public IEnumerable<char> InterestingCharacters
        {
            get { return _code.Where(CLanguageFamily.Contains); }
        }
    }
}