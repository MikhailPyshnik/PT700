using ConsoleApp1;
using NUnit.Framework;

namespace NUnitTest
{
    [TestFixture]
    class FileTest
    {
        [TestCase("Hello", "asdasd", "Hello")]
        [TestCase("TEST!", "asdaassd", "TEST!")]
        [TestCase("AbraGadabra", "asdasasdfd", "AbraGadabra")]
        [TestCase("!qwert@3er", "asda3", "!qwert@3er")]
        [TestCase("#257(", "asda3", "#257(")]
        [TestCase("", "asda3", "")]

        public void Get_Correct_File_Name_Test(string fileNameTest, string contextTest, string expected)
        {
            var fileTest = new File(fileNameTest, contextTest);

            Assert.AreEqual(expected, fileTest.getFilename(), $"When calling the method getFilename() class's File with the 'filename' parameter in the constructor the result expected {expected} byt was: {fileTest.getFilename()}");
        }

        [TestCase("Hello", "asdasd", 3)]
        [TestCase("TEST!", "asdaassd", 4)]
        [TestCase("AbraGadabra", "asdasasdfd", 5)]
        [TestCase("!qwert@3er", "asda3", 2)]
        [TestCase("#257(", "", 0)]
        [TestCase("", "a", 0)]

        public void Get_Correct_File_Size_Test(string fileNameTest, string contextTest, double expected)
        {
            var fileTest = new File(fileNameTest, contextTest);

            Assert.AreEqual(expected, fileTest.getSize(), $"When calling the method getSize() class's File with the 'context' parameter in the constructor the result expected {expected} byt was: {fileTest.getSize()}");
        }
    }
}
