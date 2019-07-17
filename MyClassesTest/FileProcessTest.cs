using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System;
using System.Configuration;
using System.IO;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessTest
    {
        private const string BAD_FILE_NAME = @"C:\BadFileName.txt";
        private string _GoodFileName;

        public TestContext TestContext { get; set; }

        #region Test Initialize and CleanUp

        [TestInitialize]
        public void TestInitialize()
        {
            if (TestContext.TestName == "FileNameDoesExists")
            {
                if (string.IsNullOrEmpty(_GoodFileName))
                {
                    SetGoodFileName();
                    TestContext.WriteLine($"Creating File: {_GoodFileName}");
                    File.AppendAllText(_GoodFileName, "Some text");
                }
            }
        }

        [TestCleanup]
        public void TestCleanUp()
        {
            if (TestContext.TestName == "FileNameDoesExists")
            {
                if (!string.IsNullOrEmpty(_GoodFileName))
                {
                    TestContext.WriteLine($"Deleting File: {_GoodFileName}");
                    File.Delete(_GoodFileName);
                }

            }
        }

        #endregion

        public void SetGoodFileName()
        {
            _GoodFileName = ConfigurationManager.AppSettings["GoodFileName"];
            if (_GoodFileName.Contains("[AppPath]"))
            {
                _GoodFileName = _GoodFileName.Replace("[AppPath]",
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }

        [TestMethod]
        public void FileNameDoesExists()
        {
            // A = Arrange (Inicializar Variaveis)
            FileProcess fp = new FileProcess();

            // A = Act     (Invocar o método para testar)
            
            TestContext.WriteLine($"Testing File: {_GoodFileName}");
            bool fromCall = fp.FileExists(_GoodFileName);
            
            // A = Assert  (Verificar a ação)
            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        public void FileNameDoesNotExists()
        {
            FileProcess fp = new FileProcess();

            bool fromCall = fp.FileExists(BAD_FILE_NAME);

            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameNullOrWhiteSpace_ThrowsArgumentNullException()
        {
            FileProcess fp = new FileProcess();

            fp.FileExists(string.Empty);
        }

        [TestMethod]
        public void FileNameNullOrWhiteSpace_ThrowsArgumentNullException_UsingTryCatch()
        {
            FileProcess fp = new FileProcess();

            try
            {
                fp.FileExists(string.Empty);
            }
            catch (ArgumentException)
            {
                return;
            }

            Assert.Fail("Fail Expected");
        }
    }
}
