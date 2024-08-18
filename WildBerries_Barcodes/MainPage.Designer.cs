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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainPage));
            BarcodeIMG = new PictureBox();
            ImagePanel = new Panel();
            Country = new Label();
            Brand = new Label();
            EAC = new PictureBox();
            Type = new Label();
            About = new Label();
            BarcodeDigits = new Label();
            Size = new Label();
            Articul = new Label();
            FolderButton = new Button();
            WbButton = new Button();
            DropDownTimer = new System.Windows.Forms.Timer(components);
            TokenButton = new Button();
            InformationButton = new Button();
            progressBar1 = new ProgressBar();
            button1 = new Button();
            OzonButton = new Button();
            ((System.ComponentModel.ISupportInitialize)BarcodeIMG).BeginInit();
            ImagePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)EAC).BeginInit();
            SuspendLayout();
            // 
            // BarcodeIMG
            // 
            BarcodeIMG.ErrorImage = null;
            BarcodeIMG.InitialImage = null;
            BarcodeIMG.Location = new Point(10, 23);
            BarcodeIMG.Name = "BarcodeIMG";
            BarcodeIMG.Size = new Size(415, 49);
            BarcodeIMG.SizeMode = PictureBoxSizeMode.StretchImage;
            BarcodeIMG.TabIndex = 3;
            BarcodeIMG.TabStop = false;
            // 
            // ImagePanel
            // 
            ImagePanel.BackColor = Color.White;
            ImagePanel.BorderStyle = BorderStyle.FixedSingle;
            ImagePanel.Controls.Add(Country);
            ImagePanel.Controls.Add(Brand);
            ImagePanel.Controls.Add(EAC);
            ImagePanel.Controls.Add(Type);
            ImagePanel.Controls.Add(About);
            ImagePanel.Controls.Add(BarcodeDigits);
            ImagePanel.Controls.Add(BarcodeIMG);
            ImagePanel.Controls.Add(Size);
            ImagePanel.Controls.Add(Articul);
            ImagePanel.ForeColor = SystemColors.ControlText;
            ImagePanel.Location = new Point(24, 165);
            ImagePanel.MaximumSize = new Size(435, 275);
            ImagePanel.MinimumSize = new Size(435, 200);
            ImagePanel.Name = "ImagePanel";
            ImagePanel.Size = new Size(435, 200);
            ImagePanel.TabIndex = 6;
            ImagePanel.Paint += ImagePanel_Paint;
            // 
            // Country
            // 
            Country.AutoSize = true;
            Country.BackColor = Color.Transparent;
            Country.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            Country.Location = new Point(234, 158);
            Country.Name = "Country";
            Country.Size = new Size(197, 38);
            Country.TabIndex = 10;
            Country.Text = "Срок годности не ограничен.\r\nСтрана производства: Страна";
            // 
            // Brand
            // 
            Brand.AutoSize = true;
            Brand.BackColor = Color.Transparent;
            Brand.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            Brand.Location = new Point(10, 0);
            Brand.Margin = new Padding(0);
            Brand.Name = "Brand";
            Brand.Size = new Size(45, 19);
            Brand.TabIndex = 9;
            Brand.Text = "Brand";
            // 
            // EAC
            // 
            EAC.Image = WBBarcodes.Properties.Resources.EAC;
            EAC.Location = new Point(390, 78);
            EAC.Name = "EAC";
            EAC.Size = new Size(35, 35);
            EAC.SizeMode = PictureBoxSizeMode.StretchImage;
            EAC.TabIndex = 7;
            EAC.TabStop = false;
            // 
            // Type
            // 
            Type.AutoSize = true;
            Type.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            Type.Location = new Point(181, 0);
            Type.Margin = new Padding(0);
            Type.Name = "Type";
            Type.Size = new Size(32, 19);
            Type.TabIndex = 8;
            Type.Text = "Тип";
            // 
            // About
            // 
            About.AutoSize = true;
            About.BackColor = Color.Transparent;
            About.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point);
            About.Location = new Point(2, 158);
            About.Name = "About";
            About.Size = new Size(196, 38);
            About.TabIndex = 5;
            About.Text = "Поставщик: ИП Фамилия И.О\r\nг. Москва\r\n";
            // 
            // BarcodeDigits
            // 
            BarcodeDigits.AutoSize = true;
            BarcodeDigits.BackColor = Color.Transparent;
            BarcodeDigits.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point);
            BarcodeDigits.Location = new Point(154, 75);
            BarcodeDigits.Name = "BarcodeDigits";
            BarcodeDigits.Size = new Size(118, 21);
            BarcodeDigits.TabIndex = 4;
            BarcodeDigits.Text = "000000000000";
            // 
            // Size
            // 
            Size.AutoSize = true;
            Size.BackColor = Color.Transparent;
            Size.Font = new Font("Segoe UI Black", 16F, FontStyle.Bold, GraphicsUnit.Point);
            Size.Location = new Point(2, 98);
            Size.Name = "Size";
            Size.Size = new Size(107, 30);
            Size.TabIndex = 2;
            Size.Text = "Размер: ";
            // 
            // Articul
            // 
            Articul.AutoSize = true;
            Articul.BackColor = Color.Transparent;
            Articul.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point);
            Articul.Location = new Point(2, 127);
            Articul.Name = "Articul";
            Articul.Size = new Size(107, 30);
            Articul.TabIndex = 0;
            Articul.Text = "Артикул: ";
            // 
            // FolderButton
            // 
            FolderButton.BackColor = SystemColors.Control;
            FolderButton.FlatStyle = FlatStyle.System;
            FolderButton.Location = new Point(63, 127);
            FolderButton.Margin = new Padding(4, 5, 4, 5);
            FolderButton.Name = "FolderButton";
            FolderButton.Size = new Size(360, 30);
            FolderButton.TabIndex = 5;
            FolderButton.Text = "Папка";
            FolderButton.UseVisualStyleBackColor = true;
            FolderButton.Click += FolderOpenButton_Click;
            // 
            // WbButton
            // 
            WbButton.BackColor = SystemColors.ButtonShadow;
            WbButton.FlatStyle = FlatStyle.System;
            WbButton.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            WbButton.ForeColor = SystemColors.Desktop;
            WbButton.Location = new Point(63, 8);
            WbButton.Name = "WbButton";
            WbButton.Size = new Size(175, 40);
            WbButton.TabIndex = 1;
            WbButton.Text = "WildBerries";
            WbButton.UseVisualStyleBackColor = false;
            WbButton.Click += ImportExcelButton_Click;
            // 
            // TokenButton
            // 
            TokenButton.Location = new Point(248, 92);
            TokenButton.Name = "TokenButton";
            TokenButton.Size = new Size(175, 30);
            TokenButton.TabIndex = 3;
            TokenButton.Text = "Токен";
            TokenButton.UseVisualStyleBackColor = true;
            TokenButton.Click += TokenButton_Click;
            // 
            // InformationButton
            // 
            InformationButton.Location = new Point(63, 92);
            InformationButton.Name = "InformationButton";
            InformationButton.Size = new Size(175, 30);
            InformationButton.TabIndex = 2;
            InformationButton.Text = "Инфо";
            InformationButton.UseVisualStyleBackColor = true;
            InformationButton.Click += InformationButton_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 371);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(460, 20);
            progressBar1.Step = 1;
            progressBar1.TabIndex = 19;
            // 
            // button1
            // 
            button1.BackColor = SystemColors.Control;
            button1.FlatStyle = FlatStyle.System;
            button1.Location = new Point(63, 56);
            button1.Margin = new Padding(4, 5, 4, 5);
            button1.Name = "button1";
            button1.Size = new Size(360, 30);
            button1.TabIndex = 20;
            button1.Text = "Маркировка";
            button1.UseVisualStyleBackColor = true;
            button1.Click += CombinePdfButton_Click;
            // 
            // OzonButton
            // 
            OzonButton.BackColor = SystemColors.ButtonShadow;
            OzonButton.FlatStyle = FlatStyle.System;
            OzonButton.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            OzonButton.ForeColor = SystemColors.Desktop;
            OzonButton.Location = new Point(248, 8);
            OzonButton.Name = "OzonButton";
            OzonButton.Size = new Size(175, 40);
            OzonButton.TabIndex = 22;
            OzonButton.Text = "Ozon";
            OzonButton.UseVisualStyleBackColor = false;
            OzonButton.Click += ExcelOzonButton_Click;
            // 
            // MainPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 401);
            Controls.Add(OzonButton);
            Controls.Add(button1);
            Controls.Add(progressBar1);
            Controls.Add(InformationButton);
            Controls.Add(TokenButton);
            Controls.Add(WbButton);
            Controls.Add(FolderButton);
            Controls.Add(ImagePanel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainPage";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "WB Barcodes";
            Load += FormLoad;
            ((System.ComponentModel.ISupportInitialize)BarcodeIMG).EndInit();
            ImagePanel.ResumeLayout(false);
            ImagePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)EAC).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private Panel ImagePanel;
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
        private Button WbButton;
        private System.Windows.Forms.Timer DropDownTimer;
        private Button TokenButton;
        private Button InformationButton;
        private ProgressBar progressBar1;
        private Button button1;
        private Button OzonButton;
    }
}