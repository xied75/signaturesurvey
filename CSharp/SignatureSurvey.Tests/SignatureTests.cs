using NUnit.Framework;
using Should.Fluent;

namespace SignatureSurvey.Tests
{
    [TestFixture]
    public class SignatureTests
    {
       public const string CodeFileName = "code.cs";

        public const string Code =
            @"
            public static void Main(string[] args) 
            {
                Console.WriteLine(""Hello, World"");
            }
        ";

        [Test]
        public void ShouldReturnTheCompleteSignature()
        {
            var codefile = new CodeFile(CodeFileName, Code);
            string signature = Signature.For(codefile);
            signature.Should().Equal("code.cs (5): {;}");
        }

        [Test]
        public void TheSignatureOfACodefileShouldContain_TheNameOfTheFile()
        {
            var codefile = new CodeFile(CodeFileName, Code);
            string signature = Signature.For(codefile);
            signature.Should().Contain("{;}");
        }

        [Test]
        public void TheSignatureOfACodefileShouldContain_InterestingCharacters()
        {
            var codefile = new CodeFile(CodeFileName, Code);
            string signature = Signature.For(codefile);
            signature.Should().Contain("{;}");
        }

        [Test]
        public void TheSignatureOfACodeFileShouldContain_TheLinesOfCodeCount()
        {
            var codefile = new CodeFile(CodeFileName, Code);
            var linesOfCodeSignature = string.Format("(5)");
            string signature = Signature.For(codefile);
            signature.Should().Contain(linesOfCodeSignature);
        }
    }
}
