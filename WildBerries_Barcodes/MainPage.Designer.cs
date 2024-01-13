namespace WildBerries_Barcodes
{
    partial class MainPage
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPage));
            this.BarcodeIMG = new System.Windows.Forms.PictureBox();
            this.ImagePanel = new System.Windows.Forms.Panel();
            this.Country = new System.Windows.Forms.Label();
            this.Brand = new System.Windows.Forms.Label();
            this.EAC = new System.Windows.Forms.PictureBox();
            this.Type = new System.Windows.Forms.Label();
            this.About = new System.Windows.Forms.Label();
            this.BarcodeDigits = new System.Windows.Forms.Label();
            this.Size = new System.Windows.Forms.Label();
            this.Color = new System.Windows.Forms.Label();
            this.Articul = new System.Windows.Forms.Label();
            this.FolderButton = new System.Windows.Forms.Button();
            this.ExcelButton = new System.Windows.Forms.Button();
            this.TicketSizeContainer = new System.Windows.Forms.FlowLayoutPanel();
            this.SizeButton = new System.Windows.Forms.Button();
            this.Size58x40 = new System.Windows.Forms.Button();
            this.Size58x30 = new System.Windows.Forms.Button();
            this.DropDownTimer = new System.Windows.Forms.Timer(this.components);
            this.TokenButton = new System.Windows.Forms.Button();
            this.InformationButton = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.BarcodeIMG)).BeginInit();
            this.ImagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EAC)).BeginInit();
            this.TicketSizeContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // BarcodeIMG
            // 
            this.BarcodeIMG.ErrorImage = null;
            this.BarcodeIMG.InitialImage = null;
            this.BarcodeIMG.Location = new System.Drawing.Point(10, 23);
            this.BarcodeIMG.Name = "BarcodeIMG";
            this.BarcodeIMG.Size = new System.Drawing.Size(415, 49);
            this.BarcodeIMG.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BarcodeIMG.TabIndex = 3;
            this.BarcodeIMG.TabStop = false;
            // 
            // ImagePanel
            // 
            this.ImagePanel.BackColor = System.Drawing.Color.White;
            this.ImagePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ImagePanel.Controls.Add(this.Country);
            this.ImagePanel.Controls.Add(this.Brand);
            this.ImagePanel.Controls.Add(this.EAC);
            this.ImagePanel.Controls.Add(this.Type);
            this.ImagePanel.Controls.Add(this.About);
            this.ImagePanel.Controls.Add(this.BarcodeDigits);
            this.ImagePanel.Controls.Add(this.BarcodeIMG);
            this.ImagePanel.Controls.Add(this.Size);
            this.ImagePanel.Controls.Add(this.Color);
            this.ImagePanel.Controls.Add(this.Articul);
            this.ImagePanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.ImagePanel.Location = new System.Drawing.Point(281, 12);
            this.ImagePanel.MaximumSize = new System.Drawing.Size(435, 300);
            this.ImagePanel.MinimumSize = new System.Drawing.Size(435, 225);
            this.ImagePanel.Name = "ImagePanel";
            this.ImagePanel.Size = new System.Drawing.Size(435, 225);
            this.ImagePanel.TabIndex = 6;
            // 
            // Country
            // 
            this.Country.AutoSize = true;
            this.Country.BackColor = System.Drawing.Color.Transparent;
            this.Country.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Country.Location = new System.Drawing.Point(237, 185);
            this.Country.Name = "Country";
            this.Country.Size = new System.Drawing.Size(197, 38);
            this.Country.TabIndex = 10;
            this.Country.Text = "Срок годности не ограничен.\r\nСтрана производства: Страна";
            // 
            // Brand
            // 
            this.Brand.AutoSize = true;
            this.Brand.BackColor = System.Drawing.Color.Transparent;
            this.Brand.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Brand.Location = new System.Drawing.Point(10, 0);
            this.Brand.Margin = new System.Windows.Forms.Padding(0);
            this.Brand.Name = "Brand";
            this.Brand.Size = new System.Drawing.Size(45, 19);
            this.Brand.TabIndex = 9;
            this.Brand.Text = "Brand";
            // 
            // EAC
            // 
            this.EAC.Image = ((System.Drawing.Image)(resources.GetObject("EAC.Image")));
            this.EAC.Location = new System.Drawing.Point(390, 78);
            this.EAC.Name = "EAC";
            this.EAC.Size = new System.Drawing.Size(35, 35);
            this.EAC.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.EAC.TabIndex = 7;
            this.EAC.TabStop = false;
            // 
            // Type
            // 
            this.Type.AutoSize = true;
            this.Type.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Type.Location = new System.Drawing.Point(181, 0);
            this.Type.Margin = new System.Windows.Forms.Padding(0);
            this.Type.Name = "Type";
            this.Type.Size = new System.Drawing.Size(32, 19);
            this.Type.TabIndex = 8;
            this.Type.Text = "Тип";
            // 
            // About
            // 
            this.About.AutoSize = true;
            this.About.BackColor = System.Drawing.Color.Transparent;
            this.About.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.About.Location = new System.Drawing.Point(2, 185);
            this.About.Name = "About";
            this.About.Size = new System.Drawing.Size(196, 38);
            this.About.TabIndex = 5;
            this.About.Text = "Поставщик: ИП Фамилия И.О\r\nг. Москва\r\n";
            // 
            // BarcodeDigits
            // 
            this.BarcodeDigits.AutoSize = true;
            this.BarcodeDigits.BackColor = System.Drawing.Color.Transparent;
            this.BarcodeDigits.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BarcodeDigits.Location = new System.Drawing.Point(154, 75);
            this.BarcodeDigits.Name = "BarcodeDigits";
            this.BarcodeDigits.Size = new System.Drawing.Size(118, 21);
            this.BarcodeDigits.TabIndex = 4;
            this.BarcodeDigits.Text = "000000000000";
            // 
            // Size
            // 
            this.Size.AutoSize = true;
            this.Size.BackColor = System.Drawing.Color.Transparent;
            this.Size.Font = new System.Drawing.Font("Segoe UI Black", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.Size.Location = new System.Drawing.Point(2, 98);
            this.Size.Name = "Size";
            this.Size.Size = new System.Drawing.Size(107, 30);
            this.Size.TabIndex = 2;
            this.Size.Text = "Размер: ";
            // 
            // Color
            // 
            this.Color.AutoSize = true;
            this.Color.BackColor = System.Drawing.Color.Transparent;
            this.Color.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Color.Location = new System.Drawing.Point(2, 164);
            this.Color.Name = "Color";
            this.Color.Size = new System.Drawing.Size(52, 21);
            this.Color.TabIndex = 1;
            this.Color.Text = "Цвет: ";
            // 
            // Articul
            // 
            this.Articul.AutoSize = true;
            this.Articul.BackColor = System.Drawing.Color.Transparent;
            this.Articul.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Articul.Location = new System.Drawing.Point(2, 130);
            this.Articul.Name = "Articul";
            this.Articul.Size = new System.Drawing.Size(107, 30);
            this.Articul.TabIndex = 0;
            this.Articul.Text = "Артикул: ";
            // 
            // FolderButton
            // 
            this.FolderButton.BackColor = System.Drawing.SystemColors.Control;
            this.FolderButton.Location = new System.Drawing.Point(134, 15);
            this.FolderButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.FolderButton.Name = "FolderButton";
            this.FolderButton.Size = new System.Drawing.Size(100, 40);
            this.FolderButton.TabIndex = 14;
            this.FolderButton.Text = "Папка";
            this.FolderButton.UseVisualStyleBackColor = true;
            this.FolderButton.Click += new System.EventHandler(this.FolderOpenButton_Click);
            // 
            // ExcelButton
            // 
            this.ExcelButton.BackColor = System.Drawing.SystemColors.Control;
            this.ExcelButton.Location = new System.Drawing.Point(9, 15);
            this.ExcelButton.Name = "ExcelButton";
            this.ExcelButton.Size = new System.Drawing.Size(100, 40);
            this.ExcelButton.TabIndex = 15;
            this.ExcelButton.Text = "Excel";
            this.ExcelButton.UseVisualStyleBackColor = true;
            this.ExcelButton.Click += new System.EventHandler(this.ImportExcelButton_Click);
            // 
            // TicketSizeContainer
            // 
            this.TicketSizeContainer.Controls.Add(this.SizeButton);
            this.TicketSizeContainer.Controls.Add(this.Size58x40);
            this.TicketSizeContainer.Controls.Add(this.Size58x30);
            this.TicketSizeContainer.Location = new System.Drawing.Point(9, 69);
            this.TicketSizeContainer.Margin = new System.Windows.Forms.Padding(0);
            this.TicketSizeContainer.MaximumSize = new System.Drawing.Size(100, 120);
            this.TicketSizeContainer.MinimumSize = new System.Drawing.Size(100, 40);
            this.TicketSizeContainer.Name = "TicketSizeContainer";
            this.TicketSizeContainer.Size = new System.Drawing.Size(100, 40);
            this.TicketSizeContainer.TabIndex = 16;
            // 
            // SizeButton
            // 
            this.SizeButton.BackColor = System.Drawing.SystemColors.Control;
            this.SizeButton.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SizeButton.Location = new System.Drawing.Point(0, 0);
            this.SizeButton.Margin = new System.Windows.Forms.Padding(0);
            this.SizeButton.Name = "SizeButton";
            this.SizeButton.Size = new System.Drawing.Size(100, 40);
            this.SizeButton.TabIndex = 17;
            this.SizeButton.Text = "Размеры";
            this.SizeButton.UseVisualStyleBackColor = true;
            this.SizeButton.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Size58x40
            // 
            this.Size58x40.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Size58x40.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Size58x40.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Size58x40.Location = new System.Drawing.Point(0, 40);
            this.Size58x40.Margin = new System.Windows.Forms.Padding(0);
            this.Size58x40.Name = "Size58x40";
            this.Size58x40.Size = new System.Drawing.Size(100, 40);
            this.Size58x40.TabIndex = 17;
            this.Size58x40.Text = "58x40";
            this.Size58x40.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Size58x40.UseVisualStyleBackColor = false;
            this.Size58x40.Click += new System.EventHandler(this.Size58x40_Click);
            // 
            // Size58x30
            // 
            this.Size58x30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.Size58x30.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Size58x30.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Size58x30.Location = new System.Drawing.Point(0, 80);
            this.Size58x30.Margin = new System.Windows.Forms.Padding(0);
            this.Size58x30.Name = "Size58x30";
            this.Size58x30.Size = new System.Drawing.Size(100, 40);
            this.Size58x30.TabIndex = 17;
            this.Size58x30.Text = "58x30";
            this.Size58x30.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.Size58x30.UseVisualStyleBackColor = false;
            this.Size58x30.Click += new System.EventHandler(this.Size58x30_Click);
            // 
            // DropDownTimer
            // 
            this.DropDownTimer.Interval = 1;
            this.DropDownTimer.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // TokenButton
            // 
            this.TokenButton.Location = new System.Drawing.Point(134, 69);
            this.TokenButton.Name = "TokenButton";
            this.TokenButton.Size = new System.Drawing.Size(100, 40);
            this.TokenButton.TabIndex = 17;
            this.TokenButton.Text = "Токен";
            this.TokenButton.UseVisualStyleBackColor = true;
            this.TokenButton.Click += new System.EventHandler(this.TokenButton_Click);
            // 
            // InformationButton
            // 
            this.InformationButton.Location = new System.Drawing.Point(134, 121);
            this.InformationButton.Name = "InformationButton";
            this.InformationButton.Size = new System.Drawing.Size(100, 40);
            this.InformationButton.TabIndex = 18;
            this.InformationButton.Text = "Инфо";
            this.InformationButton.UseVisualStyleBackColor = true;
            this.InformationButton.Click += new System.EventHandler(this.InformationButton_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(16, 329);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(700, 20);
            this.progressBar1.TabIndex = 19;
            // 
            // MainPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 361);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.InformationButton);
            this.Controls.Add(this.TokenButton);
            this.Controls.Add(this.TicketSizeContainer);
            this.Controls.Add(this.ExcelButton);
            this.Controls.Add(this.FolderButton);
            this.Controls.Add(this.ImagePanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MainPage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Barcodes Generator";
            this.Load += new System.EventHandler(this.FormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.BarcodeIMG)).EndInit();
            this.ImagePanel.ResumeLayout(false);
            this.ImagePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EAC)).EndInit();
            this.TicketSizeContainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Panel ImagePanel;
        private Label Color;
        private Label Articul;
        private Label Size;
        private PictureBox BarcodeIMG;
        private Label BarcodeDigits;
        private Label About;
        private PictureBox EAC;
        private Label Brand;
        private Label Type;
        private Button FolderButton;
        private Label Country;
        private Button ExcelButton;
        private FlowLayoutPanel TicketSizeContainer;
        private Button SizeButton;
        private Button Size58x40;
        private Button Size58x30;
        private System.Windows.Forms.Timer DropDownTimer;
        private Button TokenButton;
        private Button InformationButton;
        private ProgressBar progressBar1;
    }
}