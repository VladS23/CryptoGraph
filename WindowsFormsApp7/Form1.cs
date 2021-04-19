using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApp7
{
    public partial class Form1 : Form
    {
        //=================================================================================
        //=========================Инициализация компонентов===============================
        //=================================================================================

        public Form1()
        {
            InitializeComponent();
        }
        //Создаем новый экземпляр класса, чтобы кодировать символы в баты и наоборот
        public UnicodeEncoding ByteConverter = new UnicodeEncoding();
        //Создаем экземпляры криптографических классов 
        public static RSACryptoServiceProvider RSA = new RSACryptoServiceProvider(2048);
        public RSAParameters RSAParam = RSA.ExportParameters(false);
        Aes myAes = Aes.Create();
        //Ну и немного переменных
        byte[] encryptedAESKey;
        byte[] encryptedAESIV;

        //==============================================================================
        //========================Обработчики нажатий===================================
        //==============================================================================

        private void CreateRSA_Click(object sender, EventArgs e)
        {
            //Записываем публичный код в файлы и сохраняем их в папке с программой
            File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\RSAKey1", RSAParam.Modulus);
            File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\RSAKey2", RSAParam.Exponent);
        }

        private void ImportRSA_Click(object sender, EventArgs e)
        {
            //Читаем из файлов открытый ключ RSA и заменяем текущий ключ прочитанным
            try
            {
                RSAParam.Modulus = File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\RSAKey1");
                RSAParam.Exponent = File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\RSAKey2");
                RSA.ImportParameters(RSAParam);
            }
            catch
            {
                MessageBox.Show("Не найдены файлы ключей RSA", "", MessageBoxButtons.OK);
            }
        }

        private void CreateAES_Click(object sender, EventArgs e)
        {
            //Шифруем AES ключи с помощью RSA, а затем записываем их в файлы
            encryptedAESKey = RSA.Encrypt(myAes.Key, true);
            encryptedAESIV = RSA.Encrypt(myAes.IV, true);
            File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\AESKey1", encryptedAESKey);
            File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\AESKey2", encryptedAESIV);
        }

        private void ImportAes_Click(object sender, EventArgs e)
        {
            //Заменяем текущие ключи AES другими, прочитанными  из файла
            try
            {
                myAes.Key = RSA.Decrypt(File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\AESKey1"), true);
                myAes.IV = RSA.Decrypt(File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\AESKey2"), true);
            }
            catch
            {
                MessageBox.Show("Ключи AES не найдены или используется недействительный ключ RSA", "", MessageBoxButtons.OK);
            }
        }

        private void Encrypt_Click(object sender, EventArgs e)
        {
            //Открываем диалог выбора файлов
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            //Если пользватель выбрал файл
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Сохраняем в переменныу имя файла
                string fName = openFileDialog1.FileName;
                //Проверяем, что пользователь выбрал файл
                if (fName != null)
                {
                    try
                    {
                        //Получаем путь к файлу
                        FileInfo fInfo = new FileInfo(fName);
                        string name = fInfo.FullName;
                        //Считываем данные из файла
                        byte[] dataToEncrypt = File.ReadAllBytes(name);
                        //Зашифровываем данные с помощью ключа AES и сохраняем зашифрованные данные в файл
                        EncryptAES(name, myAes.Key, myAes.IV);

                    }
                    catch
                    {
                        MessageBox.Show("Файл слишком большой", "", MessageBoxButtons.OK);
                    }
                }
            }
        }

        private void Decrypt_Click(object sender, EventArgs e)
        {
            //Открываем диалог выбора файлов
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
            //Если пользватель выбрал файл
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Сохраняем в переменныу имя файла
                string fName = openFileDialog1.FileName;
                //Проверяем, что пользователь выбрал файл
                if (fName != null)
                {
                    try
                    {
                        //Получаем путь к файлу
                        FileInfo fInfo = new FileInfo(fName);
                        string name = fInfo.FullName;
                        //Считываем данные из файла
                        //Зашифровываем данные с помощью ключа AES и сохраняем зашифрованные данные в файл
                        DecryptAES(name, myAes.Key, myAes.IV);
                    }
                    catch
                    {
                        MessageBox.Show("Файл слишком большой", "", MessageBoxButtons.OK);
                    }
                }
            }
        }

        //=============================================================================================
        //=======================Вспомогательные функции===============================================
        //=============================================================================================

        //Эта функция выполняет шифрование данных
        public static void EncryptAES(string inputFile, byte[] Key, byte[] IV)
        {
            using (Aes aesAlg = Aes.Create())
            {
                //Используем три потока для шифрования, один считывает данные из файла и передает их во второй для расшифровки, расшифрованные даннные передаются в третий, который передает их в первый, чтобы он записал их в файл
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (FileStream inputStream = new FileStream(inputFile, FileMode.Open, FileAccess.ReadWrite))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        using (CryptoStream cryptoStream = new CryptoStream(inputStream, encryptor, CryptoStreamMode.Write))
                        {
                            inputStream.CopyTo(memoryStream);
                            inputStream.SetLength(0);
                            inputStream.Position = 0;
                            memoryStream.WriteTo(cryptoStream);
                        }
                    }
                }
                MessageBox.Show("Файл успешно зашифрован", "", MessageBoxButtons.OK);
            }
        }
        //Эта функция расшифровывает
        public static void DecryptAES(string inputFile, byte[] Key, byte[] IV)
        {
            //Здесь для расшифровки используются три потока, первый считывает данные из файла и передает их во второй, который непосредственно расшифровывает и передает в третий, который снова передает их в первый, чтобы он записал в файл
            using (FileStream inputStream = new FileStream(inputFile, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = Key;
                    aesAlg.IV = IV;
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (CryptoStream cryptoStream = new CryptoStream(inputStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            inputStream.SetLength(0);

                            memoryStream.Position = 0;
                            
                            cryptoStream.CopyTo(inputStream);
                        }
                    }
                }
            }
            MessageBox.Show("Файл успешно расшифрован", "", MessageBoxButtons.OK);
        }
    }
}
