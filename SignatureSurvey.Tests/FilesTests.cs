using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Should.Fluent;

namespace SignatureSurvey.Tests
{
    [TestFixture]
    public class FileRepositoryTests
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            const string testDirectory = "test";
            Directory.CreateDirectory(testDirectory);
            File.WriteAllText(Path.Combine(testDirectory, SampleCodeFile.CodeFileName), SampleCodeFile.Code);
            File.WriteAllText(Path.Combine(testDirectory, "ignore.txt"), "");
        }
        
        [TestFixtureTearDown]
        public void Teardown()
        {
            const string testDirectory = "test";
            Directory.CreateDirectory(testDirectory);
            File.WriteAllText(Path.Combine(testDirectory, SampleCodeFile.CodeFileName), SampleCodeFile.Code);
            File.WriteAllText(Path.Combine(testDirectory, "ignore.txt"), "");
        }

        [Test]
        public void ShouldReturnFiles()
        {
            const string directoryName = "test";
            var files = new CodeFiles(directoryName);
            files.Count().Should().Equal(1);
        }
    }

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