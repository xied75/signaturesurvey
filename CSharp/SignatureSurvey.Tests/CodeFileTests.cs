using NUnit.Framework;
using Should.Fluent;

namespace SignatureSurvey.Tests
{
    [TestFixture]
    public class CodeFileTests
    {

        [Test]
        public void ACodeFileShouldContainAName()
        {
            var codefile = new CodeFile("Name", SampleCodeFile.Code);
            codefile.Name.Should().Equal("Name");
        }

        [Test]
        public void ShouldGetTheLinesOfCode()
        {
            var codeFile = new CodeFile(SampleCodeFile.CodeFileName, SampleCodeFile.Code);
            codeFile.LinesOfCode.Should().Equal(5);
        }

        [Test]
        public void ShouldFindInterestingCharacters()
        {
            var codeFile = new CodeFile(SampleCodeFile.CodeFileName, SampleCodeFile.Code);
            codeFile.InterestingCharacters.Should().Contain.One(';');
            codeFile.InterestingCharacters.Should().Contain.One('{');
            codeFile.InterestingCharacters.Should().Contain.One('}');
        }
    }
}