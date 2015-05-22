namespace DungeonDelve_LevelChecker
{
    partial class LevelChecker
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.picLevel = new System.Windows.Forms.PictureBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.lblLevelFile = new System.Windows.Forms.Label();
            this.txtLevelFile = new System.Windows.Forms.TextBox();
            this.btnLoad = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.lblLevelName = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.lblLevelNo = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblLevelSize = new System.Windows.Forms.Label();
            this.lblLevelEntrance = new System.Windows.Forms.Label();
            this.lblEntrance = new System.Windows.Forms.Label();
            this.lblLevelErrors = new System.Windows.Forms.Label();
            this.lblErrors = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // picLevel
            // 
            this.picLevel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picLevel.Location = new System.Drawing.Point(12, 123);
            this.picLevel.Name = "picLevel";
            this.picLevel.Size = new System.Drawing.Size(500, 500);
            this.picLevel.TabIndex = 0;
            this.picLevel.TabStop = false;
            this.picLevel.Paint += new System.Windows.Forms.PaintEventHandler(this.picLevel_Paint);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(416, 638);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 24);
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lblLevelFile
            // 
            this.lblLevelFile.AutoSize = true;
            this.lblLevelFile.Location = new System.Drawing.Point(19, 15);
            this.lblLevelFile.Name = "lblLevelFile";
            this.lblLevelFile.Size = new System.Drawing.Size(55, 13);
            this.lblLevelFile.TabIndex = 0;
            this.lblLevelFile.Text = "Level &File:";
            // 
            // txtLevelFile
            // 
            this.txtLevelFile.Location = new System.Drawing.Point(91, 12);
            this.txtLevelFile.Name = "txtLevelFile";
            this.txtLevelFile.Size = new System.Drawing.Size(360, 20);
            this.txtLevelFile.TabIndex = 1;
            this.txtLevelFile.Text = "C:\\Dev\\DungeonDelve_LevelCreator\\Executables\\DD_Dark Warehouse_1.lvl.xml";
            this.txtLevelFile.TextChanged += new System.EventHandler(this.txtLevelFile_TextChanged);
            // 
            // btnLoad
            // 
            this.btnLoad.Enabled = false;
            this.btnLoad.Location = new System.Drawing.Point(462, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(50, 20);
            this.btnLoad.TabIndex = 2;
            this.btnLoad.Text = "&Load";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(19, 43);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(67, 13);
            this.lblName.TabIndex = 3;
            this.lblName.Text = "Level Name:";
            // 
            // lblLevelName
            // 
            this.lblLevelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevelName.Location = new System.Drawing.Point(93, 43);
            this.lblLevelName.Name = "lblLevelName";
            this.lblLevelName.Size = new System.Drawing.Size(233, 13);
            this.lblLevelName.TabIndex = 4;
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(345, 43);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(53, 13);
            this.lblLevel.TabIndex = 5;
            this.lblLevel.Text = "Level No:";
            // 
            // lblLevelNo
            // 
            this.lblLevelNo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevelNo.Location = new System.Drawing.Point(404, 43);
            this.lblLevelNo.Name = "lblLevelNo";
            this.lblLevelNo.Size = new System.Drawing.Size(47, 13);
            this.lblLevelNo.TabIndex = 6;
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(19, 68);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(59, 13);
            this.lblSize.TabIndex = 8;
            this.lblSize.Text = "Level Size:";
            // 
            // lblLevelSize
            // 
            this.lblLevelSize.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevelSize.Location = new System.Drawing.Point(93, 68);
            this.lblLevelSize.Name = "lblLevelSize";
            this.lblLevelSize.Size = new System.Drawing.Size(83, 13);
            this.lblLevelSize.TabIndex = 9;
            // 
            // lblLevelEntrance
            // 
            this.lblLevelEntrance.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevelEntrance.Location = new System.Drawing.Point(93, 91);
            this.lblLevelEntrance.Name = "lblLevelEntrance";
            this.lblLevelEntrance.Size = new System.Drawing.Size(83, 13);
            this.lblLevelEntrance.TabIndex = 11;
            // 
            // lblEntrance
            // 
            this.lblEntrance.AutoSize = true;
            this.lblEntrance.Location = new System.Drawing.Point(19, 91);
            this.lblEntrance.Name = "lblEntrance";
            this.lblEntrance.Size = new System.Drawing.Size(53, 13);
            this.lblEntrance.TabIndex = 10;
            this.lblEntrance.Text = "Entrance:";
            // 
            // lblLevelErrors
            // 
            this.lblLevelErrors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLevelErrors.Location = new System.Drawing.Point(408, 91);
            this.lblLevelErrors.Name = "lblLevelErrors";
            this.lblLevelErrors.Size = new System.Drawing.Size(43, 13);
            this.lblLevelErrors.TabIndex = 13;
            // 
            // lblErrors
            // 
            this.lblErrors.AutoSize = true;
            this.lblErrors.Location = new System.Drawing.Point(325, 91);
            this.lblErrors.Name = "lblErrors";
            this.lblErrors.Size = new System.Drawing.Size(77, 13);
            this.lblErrors.TabIndex = 12;
            this.lblErrors.Text = "Errors in Level:";
            // 
            // LevelChecker
            // 
            this.AcceptButton = this.btnLoad;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(527, 674);
            this.Controls.Add(this.lblLevelErrors);
            this.Controls.Add(this.lblErrors);
            this.Controls.Add(this.lblLevelEntrance);
            this.Controls.Add(this.lblEntrance);
            this.Controls.Add(this.lblLevelSize);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblLevelNo);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.lblLevelName);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.txtLevelFile);
            this.Controls.Add(this.lblLevelFile);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.picLevel);
            this.Name = "LevelChecker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dungeon Delve - Level Checker";
            this.Load += new System.EventHandler(this.LevelChecker_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picLevel;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lblLevelFile;
        private System.Windows.Forms.TextBox txtLevelFile;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label lblLevelName;
        private System.Windows.Forms.Label lblLevel;
        private System.Windows.Forms.Label lblLevelNo;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblLevelSize;
        private System.Windows.Forms.Label lblLevelEntrance;
        private System.Windows.Forms.Label lblEntrance;
        private System.Windows.Forms.Label lblLevelErrors;
        private System.Windows.Forms.Label lblErrors;
    }
}

