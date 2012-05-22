namespace SignatureSurvey
{
    public class Signature
    {
        public static string For(CodeFile codeFile)
        {           
            var signature = CodeSignature(codeFile);
            return string.Format("{0} ({1}): {2}", codeFile.Name, codeFile.LinesOfCode, signature);
        }

        private static string CodeSignature(CodeFile codeFile)
        {
            return string.Join(string.Empty, codeFile.InterestingCharacters);
        }
    }
}