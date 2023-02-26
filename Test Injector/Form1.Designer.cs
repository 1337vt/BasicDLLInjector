namespace Test_Injector
{
    partial class DLLInjector
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
            this.BlackTopPanel = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.DLLPathTextBox = new System.Windows.Forms.TextBox();
            this.BrowseToDLLButton = new System.Windows.Forms.Button();
            this.ProcessListComboBox = new System.Windows.Forms.ComboBox();
            this.InjectButton = new System.Windows.Forms.Button();
            this.RefreshProcessListButton = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.BlackTopPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // BlackTopPanel
            // 
            this.BlackTopPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.BlackTopPanel.Controls.Add(this.label4);
            this.BlackTopPanel.Controls.Add(this.label2);
            this.BlackTopPanel.Controls.Add(this.linkLabel1);
            this.BlackTopPanel.Controls.Add(this.label1);
            this.BlackTopPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.BlackTopPanel.ForeColor = System.Drawing.SystemColors.Control;
            this.BlackTopPanel.Location = new System.Drawing.Point(0, 0);
            this.BlackTopPanel.Name = "BlackTopPanel";
            this.BlackTopPanel.Size = new System.Drawing.Size(790, 85);
            this.BlackTopPanel.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Consolas", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(220, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(379, 41);
            this.label1.TabIndex = 0;
            this.label1.Text = "#BASIC DLL Injector";
            // 
            // DLLPathTextBox
            // 
            this.DLLPathTextBox.Location = new System.Drawing.Point(12, 91);
            this.DLLPathTextBox.Name = "DLLPathTextBox";
            this.DLLPathTextBox.Size = new System.Drawing.Size(646, 20);
            this.DLLPathTextBox.TabIndex = 1;
            this.DLLPathTextBox.Text = "Path to DLL";
            // 
            // BrowseToDLLButton
            // 
            this.BrowseToDLLButton.Location = new System.Drawing.Point(664, 91);
            this.BrowseToDLLButton.Name = "BrowseToDLLButton";
            this.BrowseToDLLButton.Size = new System.Drawing.Size(124, 20);
            this.BrowseToDLLButton.TabIndex = 2;
            this.BrowseToDLLButton.Text = "Browse to DLL";
            this.BrowseToDLLButton.UseVisualStyleBackColor = true;
            this.BrowseToDLLButton.Click += new System.EventHandler(this.BrowseToDLLButton_Click);
            // 
            // ProcessListComboBox
            // 
            this.ProcessListComboBox.FormattingEnabled = true;
            this.ProcessListComboBox.Location = new System.Drawing.Point(12, 117);
            this.ProcessListComboBox.Name = "ProcessListComboBox";
            this.ProcessListComboBox.Size = new System.Drawing.Size(776, 21);
            this.ProcessListComboBox.TabIndex = 3;
            // 
            // InjectButton
            // 
            this.InjectButton.Location = new System.Drawing.Point(404, 144);
            this.InjectButton.Name = "InjectButton";
            this.InjectButton.Size = new System.Drawing.Size(384, 23);
            this.InjectButton.TabIndex = 4;
            this.InjectButton.Text = "Inject DLL";
            this.InjectButton.UseVisualStyleBackColor = true;
            this.InjectButton.Click += new System.EventHandler(this.InjectButton_Click);
            // 
            // RefreshProcessListButton
            // 
            this.RefreshProcessListButton.Location = new System.Drawing.Point(12, 144);
            this.RefreshProcessListButton.Name = "RefreshProcessListButton";
            this.RefreshProcessListButton.Size = new System.Drawing.Size(386, 23);
            this.RefreshProcessListButton.TabIndex = 5;
            this.RefreshProcessListButton.Text = "Refresh Process List";
            this.RefreshProcessListButton.UseVisualStyleBackColor = true;
            this.RefreshProcessListButton.Click += new System.EventHandler(this.RefreshProcessListButton_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.LinkColor = System.Drawing.Color.PaleTurquoise;
            this.linkLabel1.Location = new System.Drawing.Point(648, 52);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(121, 17);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "claytonvantol.com";
            this.linkLabel1.VisitedLinkColor = System.Drawing.Color.DarkTurquoise;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(248, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "(Hashtag BASIC)";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(639, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Created by:  Clayton VanTol";
            // 
            // DLLInjector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(790, 191);
            this.Controls.Add(this.RefreshProcessListButton);
            this.Controls.Add(this.InjectButton);
            this.Controls.Add(this.ProcessListComboBox);
            this.Controls.Add(this.BrowseToDLLButton);
            this.Controls.Add(this.DLLPathTextBox);
            this.Controls.Add(this.BlackTopPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new System.Drawing.Size(790, 100);
            this.Name = "DLLInjector";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Educational Purposes Only";
            this.BlackTopPanel.ResumeLayout(false);
            this.BlackTopPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel BlackTopPanel;
        private System.Windows.Forms.TextBox DLLPathTextBox;
        private System.Windows.Forms.Button BrowseToDLLButton;
        private System.Windows.Forms.ComboBox ProcessListComboBox;
        private System.Windows.Forms.Button InjectButton;
        private System.Windows.Forms.Button RefreshProcessListButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
    }
}

