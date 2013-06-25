namespace Ejik
{
    partial class FormSettings
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.fbd = new System.Windows.Forms.FolderBrowserDialog();
            this.comboRules = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSelectMovePath = new System.Windows.Forms.Button();
            this.btnSelectWatchPath = new System.Windows.Forms.Button();
            this.txtFilter = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtWatchPath = new System.Windows.Forms.TextBox();
            this.txtMovePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnAddRule = new System.Windows.Forms.Button();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnRemoveRule = new System.Windows.Forms.Button();
            this.chkStartup = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(163, 156);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(325, 156);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // comboRules
            // 
            this.comboRules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboRules.FormattingEnabled = true;
            this.comboRules.Location = new System.Drawing.Point(56, 12);
            this.comboRules.Name = "comboRules";
            this.comboRules.Size = new System.Drawing.Size(289, 21);
            this.comboRules.TabIndex = 2;
            this.comboRules.SelectedIndexChanged += new System.EventHandler(this.comboRules_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSelectMovePath);
            this.groupBox1.Controls.Add(this.btnSelectWatchPath);
            this.groupBox1.Controls.Add(this.txtFilter);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtWatchPath);
            this.groupBox1.Controls.Add(this.txtMovePath);
            this.groupBox1.Location = new System.Drawing.Point(13, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(387, 91);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            // 
            // btnSelectMovePath
            // 
            this.btnSelectMovePath.Location = new System.Drawing.Point(361, 38);
            this.btnSelectMovePath.Name = "btnSelectMovePath";
            this.btnSelectMovePath.Size = new System.Drawing.Size(24, 22);
            this.btnSelectMovePath.TabIndex = 5;
            this.btnSelectMovePath.Text = "...";
            this.btnSelectMovePath.UseVisualStyleBackColor = true;
            this.btnSelectMovePath.Click += new System.EventHandler(this.btnSelectMovePath_Click);
            // 
            // btnSelectWatchPath
            // 
            this.btnSelectWatchPath.Location = new System.Drawing.Point(361, 12);
            this.btnSelectWatchPath.Name = "btnSelectWatchPath";
            this.btnSelectWatchPath.Size = new System.Drawing.Size(24, 22);
            this.btnSelectWatchPath.TabIndex = 5;
            this.btnSelectWatchPath.Text = "...";
            this.btnSelectWatchPath.UseVisualStyleBackColor = true;
            this.btnSelectWatchPath.Click += new System.EventHandler(this.btnSelectWatchPath_Click);
            // 
            // txtFilter
            // 
            this.txtFilter.Location = new System.Drawing.Point(75, 65);
            this.txtFilter.Name = "txtFilter";
            this.txtFilter.Size = new System.Drawing.Size(309, 20);
            this.txtFilter.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Filter";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Watch path";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Move path";
            // 
            // txtWatchPath
            // 
            this.txtWatchPath.Location = new System.Drawing.Point(75, 13);
            this.txtWatchPath.Name = "txtWatchPath";
            this.txtWatchPath.Size = new System.Drawing.Size(290, 20);
            this.txtWatchPath.TabIndex = 0;
            // 
            // txtMovePath
            // 
            this.txtMovePath.Location = new System.Drawing.Point(75, 39);
            this.txtMovePath.Name = "txtMovePath";
            this.txtMovePath.Size = new System.Drawing.Size(290, 20);
            this.txtMovePath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Rule";
            // 
            // btnAddRule
            // 
            this.btnAddRule.Location = new System.Drawing.Point(348, 12);
            this.btnAddRule.Name = "btnAddRule";
            this.btnAddRule.Size = new System.Drawing.Size(24, 22);
            this.btnAddRule.TabIndex = 5;
            this.btnAddRule.Text = "+";
            this.btnAddRule.UseVisualStyleBackColor = true;
            this.btnAddRule.Click += new System.EventHandler(this.btnAddRule_Click);
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(244, 156);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 23);
            this.btnApply.TabIndex = 0;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnRemoveRule
            // 
            this.btnRemoveRule.Location = new System.Drawing.Point(374, 12);
            this.btnRemoveRule.Name = "btnRemoveRule";
            this.btnRemoveRule.Size = new System.Drawing.Size(24, 22);
            this.btnRemoveRule.TabIndex = 5;
            this.btnRemoveRule.Text = "-";
            this.btnRemoveRule.UseVisualStyleBackColor = true;
            this.btnRemoveRule.Click += new System.EventHandler(this.btnRemoveRule_Click);
            // 
            // chkStartup
            // 
            this.chkStartup.AutoSize = true;
            this.chkStartup.Location = new System.Drawing.Point(22, 136);
            this.chkStartup.Name = "chkStartup";
            this.chkStartup.Size = new System.Drawing.Size(93, 17);
            this.chkStartup.TabIndex = 6;
            this.chkStartup.Text = "Run at startup";
            this.chkStartup.UseVisualStyleBackColor = true;
            // 
            // FormSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(409, 188);
            this.Controls.Add(this.chkStartup);
            this.Controls.Add(this.btnRemoveRule);
            this.Controls.Add(this.btnAddRule);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.comboRules);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormSettings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.FormSettings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.FolderBrowserDialog fbd;
        private System.Windows.Forms.ComboBox comboRules;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMovePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnAddRule;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtWatchPath;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnSelectMovePath;
        private System.Windows.Forms.Button btnSelectWatchPath;
        private System.Windows.Forms.Button btnRemoveRule;
        private System.Windows.Forms.CheckBox chkStartup;
    }
}