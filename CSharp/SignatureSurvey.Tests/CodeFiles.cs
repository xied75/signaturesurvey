using System.Collections;
using System.Collections.Generic;

namespace SignatureSurvey.Tests
{
    public class CodeFiles : IEnumerable<CodeFile>
    {
        private readonly string _directoryName;

        public CodeFiles(string directoryName)
        {
            _directoryName = directoryName;
        }

        public IEnumerator<CodeFile> GetEnumerator()
        {
            yield return new CodeFile("a","b");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}