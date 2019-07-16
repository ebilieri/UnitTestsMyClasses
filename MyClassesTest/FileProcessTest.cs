using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessTest
    {
        [TestMethod]
        public void FileNameDoesExists()
        {
            // A = Arrange (Inicializar Variaveis)
            FileProcess fp = new FileProcess();
            // A = Act     (Invocar o método para testar)
            bool fromCall = fp.FileExists(@"C:\Windows\Regedit.exe");
            // A = Assert  (Verificar a ação)
            Assert.IsTrue(fromCall);
        }

        [TestMethod]
        public void FileNameDoesNotExists()
        {
            FileProcess fp = new FileProcess();

            bool fromCall = fp.FileExists(@"C:Regedit.exe");

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
