using System.IO;
using System.Linq;
using NUnit.Framework;
using Should.Fluent;

namespace SignatureSurvey.Tests
{
    [TestFixture]
    public class FileRepositoryTests
    {
        private const string TestDirectory = "test";

        [TestFixtureSetUp]
        public void Setup()
        {
            CreateDirectory();
            CreateSubDirectory();
        }

        private static void CreateDirectory()
        {
            Directory.CreateDirectory(TestDirectory);
            File.WriteAllText(Path.Combine(TestDirectory, SampleCodeFile.CodeFileName), SampleCodeFile.Code);
            File.WriteAllText(Path.Combine(TestDirectory, "ignore.txt"), "");
        }
        private static void CreateSubDirectory()
        {
            Directory.CreateDirectory(Path.Combine(TestDirectory, TestDirectory));
            File.WriteAllText(Path.Combine(TestDirectory, TestDirectory, SampleCodeFile.CodeFileName), SampleCodeFile.Code);
        }

        [TestFixtureTearDown]
        public void Teardown()
        {
            Directory.Delete(TestDirectory, recursive:true);
        }

        [Test]
        public void ShouldReturnAllFiles()
        {
            var files = new CodeFiles(TestDirectory);
            files.Count().Should().Equal(2);
        }
    }
}