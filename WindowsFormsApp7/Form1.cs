using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;
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
        byte[] encryptedData;
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
                MessageBox.Show("Не найдены файлы ключей RSA");
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
                MessageBox.Show("Ключи AES не найдены или используется недействительный ключ RSA");
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
                        string dataToEncrypt = File.ReadAllText(name);
                        //Зашифровываем данные с помощью ключа AES и сохраняем зашифрованные данные в файл
                        byte[] encryptedData = EncryptAES(dataToEncrypt, myAes.Key, myAes.IV);
                        File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\encryptedData", encryptedData);
                    }
                    catch
                    {
                        MessageBox.Show("Файл по текущему расположению не найден, возможно он был удален или перемещен, если вы уверены, что это не так, переместите его в папку с программой и попробуйте снова");
                    }
                }
            }
        }

        private void Decrypt_Click(object sender, EventArgs e)
        {
            try
            {
                encryptedData = File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\encryptedData");
            }
            catch
            {
                MessageBox.Show("Не найден файл для расшифровки");
            }
            try
            {
                string decryptedData = DecryptAES(encryptedData, myAes.Key, myAes.IV);
                File.WriteAllText(Directory.GetCurrentDirectory() + "\\Decrypted.txt", decryptedData);
            }
            catch
            {
                MessageBox.Show("Ключи AES недействительны");
            }
        }
        private void DecryptFile_Click(object sender, EventArgs e)
        {
            try
            {
                encryptedData = File.ReadAllBytes(Directory.GetCurrentDirectory() + "\\encryptedData");
            }
            catch
            {
                MessageBox.Show("Не найден файл для расшифровки");
            }
            try
            {
                string decryptedData = DecryptAES(encryptedData, myAes.Key, myAes.IV);
                byte [] decryptedFile = ByteConverter.GetBytes(decryptedData);
                File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\DecryptedFile.txt", decryptedFile);
            }
            catch
            {
                MessageBox.Show("Ключи AES недействительны");
            }
        }

        //=============================================================================================
        //=======================Вспомогательные функции===============================================
        //=============================================================================================

        //Эта функция выполняет шифрование данных
        static byte[] EncryptAES(string plainText, byte[] Key, byte[] IV)
        {
            //Проверяем правильность поданных аргументов
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;
            // Создаем новый объект AES и переназначаем ему ключи
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                // Создаем шифратор для потокового шифрования
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                // Создаем потоки для шифрования
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Записываем все данные в поток
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            return encrypted;
        }
        //Эта функция выполняет расшифровку
        static string DecryptAES(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Проверяем правильность аргументов
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            //Объявлем переменную для хранения расшифрованного текста
            string plaintext = null;
            //Создаем новый объект AES и задаем ему необходимые параметры
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;
                //Создаем дешифратор для потоковой расшифровки
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                //Создаем поток для расшифровки
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            //Считываемрасшифрованные байты из потока и записываем их в переменную
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
            return plaintext;
        }
    }
}
