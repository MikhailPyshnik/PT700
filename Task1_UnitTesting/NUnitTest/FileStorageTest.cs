using ConsoleApp1;
using FileSystem.exception;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTest
{
    [TestFixture]
    class FileStorageTest
    {
        private string alphabet = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm1234567890";

        [TestCase("Hello", "asdasddsf", true, 1)]
        [TestCase("TEST!", "asdaassdvsd", true, 1)]
        [TestCase("AbraGadabra", "asdasdv ssdfsdfsd sdfsdfd sasdfd", true, 1)]
        [TestCase("!qwert@3er", "asdscssd sc sdcs d sd a3", true, 1)]
        [TestCase("#257(", "asda3fsdvscdsdc", true, 1)]
        [TestCase("", "asdsdsdsdcsdfa3", true, 1)]
        [TestCase("BigSize", "qwertyuiop[]asdfghjkl;'zxcvbn,.qazwsxedcrfvtgbyhnumj,qazwsxcderfvbgtyhnmjuik,zxcvbnmasdfghjkllqwertyiuoasdhfgadfjikol.;poiuytreqazxswedcvfrtgbnhyuwefvfvdsvdfvhohrgivenrigvejsrvotueiyrng osluhfngeurgyviehkkcegnhcgekhgikermdhg", false, 0)]
        public void Check_Method_write_FileStorage_Test(string filename, string content, bool expected, int countExpected)
        {
            var fileStorage = new FileStorage(10);
            var boolResult = fileStorage.write(new File(filename, content));
            var countFileInFileStorage = fileStorage.getFiles().Count;
            Assert.AreEqual(expected, boolResult, $"When writing the file FileStorage expected to get {expected} but was {boolResult}");
            Assert.AreEqual(countExpected, countFileInFileStorage, $"When writing the file FileStorage expected to get count of files '{countExpected}' but was '{countFileInFileStorage}'.");
        }

        private static IEnumerable<TestCaseData> FileStorageTempDataForIsExistMethod
        {
            get
            {
                yield return new TestCaseData(new[] { new File("Acasa", "assas"), new File("Acasa1", "assas") }, "Acasa", true);
                yield return new TestCaseData(new[] { new File("AbraGadabra", "assas"), new File("!qwert@3er", "assas"), new File("Hello", "asdasd") }, "Hello", true);
                yield return new TestCaseData(new[] { new File("Acasa", "assas"), new File("azxcf", "assas"), new File("Hello", "asdasd") }, "Qwer" , false);
                yield return new TestCaseData(new[] { new File("#257(", "assas"), new File("Acasa1", "assas"), new File("Grodno", "asdasd") }, "qwerty" , false);
            }
        }

        [Test, TestCaseSource(nameof(FileStorageTempDataForIsExistMethod))]
        public void Check_method_isExist_FileStorage_Test(File[] array, string filename, bool expected)
        {
            FileStorage fileStorageTemp = new FileStorage();

            for (int i = 0; i < array.Length; i++)
            {
                fileStorageTemp.write(array[i]);
            }

            var boolResult = fileStorageTemp.isExists(filename);

            Assert.AreEqual(expected, boolResult, $"When call method isExist of FileStorage expected to get file status '{expected}' but was '{boolResult}'.");
        }

        private static IEnumerable<TestCaseData> FileStorageTempDataGetFile
        {
            get
            {
                yield return new TestCaseData(new[] { new File("Acasa", "assas"), new File("Acasa1", "assas") }, "Acasa");
                yield return new TestCaseData(new[] { new File("AbraGadabra", "assas"), new File("!qwert@3er", "assas"), new File("Hello", "asdasd") }, "Hello");
                yield return new TestCaseData(new[] { new File("Acasa", "assas"), new File("azxcf", "assas"), new File("Hello", "asdasd"), new File("Qwer", "asdadasdacsd") }, "Qwer");
                yield return new TestCaseData(new[] { new File("#257(", "assas"), new File("Acasa1", "assas"), new File("Grodno", "asdasd") }, "qwerty");
            }
        }

        [Test, TestCaseSource(nameof(FileStorageTempDataGetFile))]
        public void Check_method_getFile_FileStorage_Test(File[] array, string filename)
        {
            FileStorage fileStorageTemp = new FileStorage();

            for (int i = 0; i < array.Length; i++)
            {
                fileStorageTemp.write(array[i]);
            }

            var fileResult = fileStorageTemp.getFile(filename);

            Assert.NotNull(fileResult, $"The file with filename - '{filename}' is missing in FileStorage.");
        }


        private static IEnumerable<TestCaseData> FileStorageTempDataGetFiles
        {
            get
            {
                yield return new TestCaseData(new[] { new File("Acasa", "assas"), new File("Acasa1", "assas") }, 2);
                yield return new TestCaseData(new[] { new File("AbraGadabra", "assas"), new File("!qwert@3er", "assas"), new File("Hello", "asdasd") }, 3);
                yield return new TestCaseData(new[] { new File("Acasa", "assas"), new File("azxcf", "assas"), new File("Hello", "asdasd"), new File("Qwer", "asdadasdacsd") }, 4);
            }
        }

        [Test, TestCaseSource(nameof(FileStorageTempDataGetFiles))]
        public void Check_method_getFiles_FileStorage_Test(File[] array, int countExpected)
        {
            FileStorage fileStorageTemp = new FileStorage();

            for (int i = 0; i < array.Length; i++)
            {
                fileStorageTemp.write(array[i]);
            }

            var filesCountResult = fileStorageTemp.getFiles().Count;

            Assert.AreEqual(countExpected, filesCountResult, $"The count of files in FileStorage expected -'{countExpected}' but was '{filesCountResult}'.");
        }



        [TestCase(20, true)]
        [TestCase(30, true)]
        [TestCase(200, true)]
        [TestCase(6, true)]
        public void Check_Correct_Work_Method_isExist_and_write_With_Constructor_FileStorage_Test(int size, bool expected)
        {
            List<string> fileNames = new List<string>();

            var fileStorage = new FileStorage(size);

            for (int i = 0; i < size; i++)
            {
                var fileName = GenRandomFileName(alphabet, GenRandomNumber(10));

                foreach (string fileNamesTemp in fileNames)
                {
                    if (fileStorage.isExists(fileNamesTemp))
                    {
                        while (fileStorage.isExists(fileName))
                        {
                            fileName = GenRandomFileName(alphabet, GenRandomNumber(10));
                        }
                    }
                }

                fileNames.Add(fileName);

                var content = GenRandomString(alphabet, GenRandomNumber(5));

                var fileTest = new File(fileName, content);

                fileStorage.write(fileTest);
            }

            bool isExistResultBool = false;

            if (fileStorage.getFiles().Count == fileNames.Count)
            {
                isExistResultBool = true;
            }
  
            Assert.AreEqual(expected, isExistResultBool, $"Expect to record all unique File - '{expected}' but was '{isExistResultBool}'.");
        }

        [TestCase(0, false)]
        [TestCase(-10, false)]
        [TestCase(-100, false)]

        public void Result_FileStorage_write_File_To_FileStorage_With_Negative_sizeTest(int size, bool expected)
        {
            FileStorage fileStorageTemp = new FileStorage(size);

            File fileFirst = new File("AbraGadabra", "asdasasdfd");

            var canIWriteWhenSizeLessZero =  fileStorageTemp.write(fileFirst);

            Assert.AreEqual(expected, canIWriteWhenSizeLessZero, $"We can't write files to FileStorage with a negative size - '{size}'.");

        }

        private static IEnumerable<TestCaseData> FileStorageTempData
        {
            get
            {
                yield return new TestCaseData(new[] { new File("Acasa", "assas"), new File("Acasa1", "assas") }, new[] { "Acasa", "Acasa1" }, 0);
                yield return new TestCaseData(new[] { new File("Acasa", "assas"), new File("Acasa1", "assas"), new File("Hello", "asdasd") }, new[] { "Acasa", "Acasa1" }, 1);
                yield return new TestCaseData(new[] { new File("Acasa", "assas"), new File("Acasa1", "assas"), new File("Hello", "asdasd") }, new[] { "Qwer" }, 3);
                yield return new TestCaseData(new[] { new File("Acasa", "assas"), new File("Acasa1", "assas"), new File("Hello", "asdasd") }, new[] {""}, 3);
            }
        }

        [Test, TestCaseSource(nameof(FileStorageTempData))]
        public void Result_Methods_FileStorage_write_and_delete_Test(File[] array, string[] array2, int expected)
        {
            FileStorage fileStorageTemp = new FileStorage();

            for (int i = 0; i < array.Length; i++)
            {
                fileStorageTemp.write(array[i]);
            }

            for (int i = 0; i < array2.Length; i++)
            {
                fileStorageTemp.delete(array2[i]);
            }

            var result = fileStorageTemp.getFiles().Count;

            Assert.AreEqual(expected, result, $"After delete file(s), we expect -'{expected}' files in FileStorage.But was -'{result}'.");
        }

        [TestCase("AbraGadabra", "asdasasdfd")]
        public void Result_FileStorage_write_FileNameAlreadyExistsException_By_Method_IsExist_Test(string fileNameTest, string contextTest)
        {
            FileStorage fileStorageTemp = new FileStorage();

            File fileFirst = new File(fileNameTest, contextTest);

            fileStorageTemp.write(fileFirst);

            var ex = Assert.Throws<FileNameAlreadyExistsException>(() => fileStorageTemp.write(fileFirst), "The error type is incorrect.Expected FileNameAlreadyExistsException.");

            Assert.AreEqual("Exception of type 'FileSystem.exception.FileNameAlreadyExistsException' was thrown.", ex.Message, $"The error type is incorrect.Expected FileNameAlreadyExistsException with message -Exception of type 'FileSystem.exception.FileNameAlreadyExistsException' was thrown. But was -{ex.Message}");
        }

        #region Private Methods
        private string GenRandomFileName(string Alphabet, int Length)
        {
            string [] extention = new string[] {".exe", ".csv", ".exl", ".doc", ".txt" };        
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder(Length - 1);
            int Position = 0;

            for (int i = 0; i < Length; i++)
            {
                Position = rnd.Next(0, Alphabet.Length - 1);
                sb.Append(Alphabet[Position]);
            }
            sb.Append(extention[rnd.Next(0, extention.Length - 1)]);

            return sb.ToString();
        }

        private string GenRandomString(string Alphabet, int Length)
        {
            Random rnd = new Random();
            StringBuilder sb = new StringBuilder(Length - 1);
            int Position = 0;

            for (int i = 0; i < Length; i++)
            {
                Position = rnd.Next(0, Alphabet.Length - 1);
                sb.Append(Alphabet[Position]);
            }

            return sb.ToString();
        }

        private int GenRandomNumber(int number)
        {
            Random rnd = new Random();
            int returnNumber = 2;
            return returnNumber = rnd.Next(1, number);
        }
        #endregion
    }
}
