using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using System.Reflection;
using UnitTestEx;
using Assert = NUnit.Framework.Assert;

namespace UnitTestProject
{
    /// <summary>
    /// Summary description for FileStorageTest
    /// </summary>
    [TestClass]
    public class FileStorageTest
    {
        public const string MAX_SIZE_EXCEPTION = "DIFFERENT MAX SIZE";
        public const string NULL_FILE_EXCEPTION = "NULL FILE";
        public const string NO_EXPECTED_EXCEPTION_EXCEPTION = "There is no expected exception";

        public const string SPACE_STRING = " ";
        public const string FILE_PATH_STRING = "@D:\\JDK-intellij-downloader-info.txt";
        public const string CONTENT_STRING = "Some text";
        public const string REPEATED_STRING = "AA";
        public const string WRONG_SIZE_CONTENT_STRING = "TEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtextTEXTtext";
        public const string TIC_TOC_TOE_STRING = "tictoctoe.game";

        public const int NEW_SIZE = 5;

        public FileStorage storage = new FileStorage(NEW_SIZE);

        ///* Тестирование записи файла */
        [TestMethod]
        public void WriteTest()
        {
            File file = new File("","");
            Assert.True(storage.Write(file));
            storage.DeleteAllFiles();
        }

        ///* Тестирование удаления нескольких файлов */
        [TestMethod]
        public void DeleteAllFiles()
        {
            File file_1 = new File("1", "123");
            File file_2 = new File("2", "213");
            File file_3 = new File("3", "312");

            storage.Write(file_1);
            storage.Write(file_2);
            storage.Write(file_3);

            Assert.IsFalse(storage.DeleteAllFiles());
        }


        ///* Тестирование записи дублирующегося файла */
        [TestMethod]
        public void WriteExceptionTest()
        {
            File file = new File("", "");
            bool isException = false;
            try
            {
                storage.Write(file);
                Assert.False(storage.Write(file));
                storage.DeleteAllFiles();
            }
            catch (FileNameAlreadyExistsException)
            {
                isException = true;
            }
            Assert.True(isException, NO_EXPECTED_EXCEPTION_EXCEPTION);
        }

        ///* Тестирование проверки существования файла */
        [TestMethod]
        public void IsExistsTest()
        {
            File file = new File("","");
            String name = file.GetFilename();
            Assert.False(storage.IsExists(name));
            try
            {
                storage.Write(file);
            }
            catch (FileNameAlreadyExistsException e)
            {
                Console.WriteLine(String.Format("Exception {0} in method {1}", e.GetBaseException(), MethodBase.GetCurrentMethod().Name));
            }
            Assert.True(storage.IsExists(name));
            storage.DeleteAllFiles();
        }

        /* Тестирование копирования файла */
        [TestMethod]
        public void CreateCopyTest()
        {
            File file = new File("", "");
            String name = file.GetFilename();
            Assert.False(storage.CreateCopy(name));
            try
            {
                storage.Write(file);
            }
            catch (FileNameAlreadyExistsException e)
            {
                Console.WriteLine(String.Format("Exception {0} in method {1}", e.GetBaseException(), MethodBase.GetCurrentMethod().Name));
            }
            Assert.True(storage.CreateCopy(name));
            storage.DeleteAllFiles();
        }



        /* Тестирование удаления файла */
        [TestMethod]
        public void DeleteTest() {
            String fileName = "";
            File file = new File("", "");
            storage.Write(file);
            Assert.True(storage.Delete(fileName));
        }

        /* Тестирование получения файлов */
        [TestMethod]
        public void GetFilesTest()
        {
            foreach (File getFile in storage.GetFiles()) 
            {
                Assert.NotNull(getFile);
            }
        }

        /* Тестирование получения файла */
        [TestMethod]
        public void GetFileTest()
        {
            File expectedFile = new File("", "Test");

            storage.Write(expectedFile);

            File actualfile = storage.GetFile(expectedFile.GetFilename());
            bool difference = actualfile.GetFilename().Equals(expectedFile.GetFilename()) && actualfile.GetSize().Equals(expectedFile.GetSize());

            Assert.IsTrue(difference, string.Format("There is some differences in {0} or {1}", expectedFile.GetFilename(), expectedFile.GetSize()));
        }
    }
}
