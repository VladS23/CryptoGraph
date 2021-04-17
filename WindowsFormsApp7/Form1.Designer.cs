using System.Windows.Forms;
using WindowsFormsApp7;
namespace WindowsFormsApp7

{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.CreateRSA = new System.Windows.Forms.Button();
            this.ImportRSA = new System.Windows.Forms.Button();
            this.CreateAES = new System.Windows.Forms.Button();
            this.ImportAes = new System.Windows.Forms.Button();
            this.Encrypt = new System.Windows.Forms.Button();
            this.Decrypt = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // CreateRSA
            // 
            this.CreateRSA.Location = new System.Drawing.Point(12, 52);
            this.CreateRSA.Name = "CreateRSA";
            this.CreateRSA.Size = new System.Drawing.Size(148, 43);
            this.CreateRSA.TabIndex = 0;
            this.CreateRSA.Text = "Создать ключ RSA";
            this.CreateRSA.UseVisualStyleBackColor = true;
            this.CreateRSA.Click += new System.EventHandler(this.CreateRSA_Click);
            // 
            // ImportRSA
            // 
            this.ImportRSA.Location = new System.Drawing.Point(166, 52);
            this.ImportRSA.Name = "ImportRSA";
            this.ImportRSA.Size = new System.Drawing.Size(148, 43);
            this.ImportRSA.TabIndex = 1;
            this.ImportRSA.Text = "Импортировать ключ RSA";
            this.ImportRSA.UseVisualStyleBackColor = true;
            this.ImportRSA.Click += new System.EventHandler(this.ImportRSA_Click);
            // 
            // CreateAES
            // 
            this.CreateAES.Location = new System.Drawing.Point(166, 101);
            this.CreateAES.Name = "CreateAES";
            this.CreateAES.Size = new System.Drawing.Size(148, 43);
            this.CreateAES.TabIndex = 2;
            this.CreateAES.Text = "Создать ключ AES";
            this.CreateAES.UseVisualStyleBackColor = true;
            this.CreateAES.Click += new System.EventHandler(this.CreateAES_Click);
            // 
            // ImportAes
            // 
            this.ImportAes.Location = new System.Drawing.Point(12, 101);
            this.ImportAes.Name = "ImportAes";
            this.ImportAes.Size = new System.Drawing.Size(148, 43);
            this.ImportAes.TabIndex = 3;
            this.ImportAes.Text = "Импортировать ключ AES";
            this.ImportAes.UseVisualStyleBackColor = true;
            this.ImportAes.Click += new System.EventHandler(this.ImportAes_Click);
            // 
            // Encrypt
            // 
            this.Encrypt.Location = new System.Drawing.Point(88, 210);
            this.Encrypt.Name = "Encrypt";
            this.Encrypt.Size = new System.Drawing.Size(148, 43);
            this.Encrypt.TabIndex = 4;
            this.Encrypt.Text = "Зашифровать";
            this.Encrypt.UseVisualStyleBackColor = true;
            this.Encrypt.Click += new System.EventHandler(this.Encrypt_Click);
            // 
            // Decrypt
            // 
            this.Decrypt.Location = new System.Drawing.Point(88, 259);
            this.Decrypt.Name = "Decrypt";
            this.Decrypt.Size = new System.Drawing.Size(148, 43);
            this.Decrypt.TabIndex = 5;
            this.Decrypt.Text = "Расшифровать";
            this.Decrypt.UseVisualStyleBackColor = true;
            this.Decrypt.Click += new System.EventHandler(this.Decrypt_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(397, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(360, 302);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 361);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.Decrypt);
            this.Controls.Add(this.Encrypt);
            this.Controls.Add(this.ImportAes);
            this.Controls.Add(this.CreateAES);
            this.Controls.Add(this.ImportRSA);
            this.Controls.Add(this.CreateRSA);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "CryptoGraph";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CreateRSA;
        private System.Windows.Forms.Button ImportRSA;
        private System.Windows.Forms.Button CreateAES;
        private System.Windows.Forms.Button ImportAes;
        private System.Windows.Forms.Button Encrypt;
        private System.Windows.Forms.Button Decrypt;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

