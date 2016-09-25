namespace NugetPublisher.Desktop.UI
{
    partial class Form1
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
            this.ArtifactsListView = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.SelectArtifactsButton = new System.Windows.Forms.Button();
            this.CreatePackageButton = new System.Windows.Forms.Button();
            this.CreateNuspecButton = new System.Windows.Forms.Button();
            this.ServerUrlTextBox = new System.Windows.Forms.TextBox();
            this.PushToServerButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.StatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.ApiKeyTextBox = new System.Windows.Forms.TextBox();
            this.StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // ArtifactsListView
            // 
            this.ArtifactsListView.Alignment = System.Windows.Forms.ListViewAlignment.Left;
            this.ArtifactsListView.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ArtifactsListView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ArtifactsListView.CheckBoxes = true;
            this.ArtifactsListView.GridLines = true;
            this.ArtifactsListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.ArtifactsListView.Location = new System.Drawing.Point(27, 51);
            this.ArtifactsListView.Name = "ArtifactsListView";
            this.ArtifactsListView.Size = new System.Drawing.Size(792, 331);
            this.ArtifactsListView.TabIndex = 0;
            this.ArtifactsListView.UseCompatibleStateImageBehavior = false;
            this.ArtifactsListView.View = System.Windows.Forms.View.List;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(24, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(122, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Artifacts (DLLs)";
            // 
            // SelectArtifactsButton
            // 
            this.SelectArtifactsButton.Location = new System.Drawing.Point(840, 51);
            this.SelectArtifactsButton.Name = "SelectArtifactsButton";
            this.SelectArtifactsButton.Size = new System.Drawing.Size(142, 43);
            this.SelectArtifactsButton.TabIndex = 2;
            this.SelectArtifactsButton.Text = "Select Artifacts";
            this.SelectArtifactsButton.UseVisualStyleBackColor = true;
            this.SelectArtifactsButton.Click += new System.EventHandler(this.SelectArtifactsButton_Click);
            // 
            // CreatePackageButton
            // 
            this.CreatePackageButton.Enabled = false;
            this.CreatePackageButton.Location = new System.Drawing.Point(840, 194);
            this.CreatePackageButton.Name = "CreatePackageButton";
            this.CreatePackageButton.Size = new System.Drawing.Size(142, 43);
            this.CreatePackageButton.TabIndex = 2;
            this.CreatePackageButton.Text = "Create Package";
            this.CreatePackageButton.UseVisualStyleBackColor = true;
            this.CreatePackageButton.Click += new System.EventHandler(this.CreatePackageButton_Click);
            // 
            // CreateNuspecButton
            // 
            this.CreateNuspecButton.Enabled = false;
            this.CreateNuspecButton.Location = new System.Drawing.Point(840, 119);
            this.CreateNuspecButton.Name = "CreateNuspecButton";
            this.CreateNuspecButton.Size = new System.Drawing.Size(142, 43);
            this.CreateNuspecButton.TabIndex = 2;
            this.CreateNuspecButton.Text = "Create NuSpec";
            this.CreateNuspecButton.UseVisualStyleBackColor = true;
            this.CreateNuspecButton.Click += new System.EventHandler(this.CreateNuspecButton_Click);
            // 
            // ServerUrlTextBox
            // 
            this.ServerUrlTextBox.AccessibleName = "";
            this.ServerUrlTextBox.Font = new System.Drawing.Font("Lucida Console", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ServerUrlTextBox.Location = new System.Drawing.Point(27, 404);
            this.ServerUrlTextBox.Margin = new System.Windows.Forms.Padding(3, 8, 3, 3);
            this.ServerUrlTextBox.MaximumSize = new System.Drawing.Size(0, 35);
            this.ServerUrlTextBox.MinimumSize = new System.Drawing.Size(564, 35);
            this.ServerUrlTextBox.Name = "ServerUrlTextBox";
            this.ServerUrlTextBox.Size = new System.Drawing.Size(564, 35);
            this.ServerUrlTextBox.TabIndex = 3;
            this.ServerUrlTextBox.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // PushToServerButton
            // 
            this.PushToServerButton.Enabled = false;
            this.PushToServerButton.Location = new System.Drawing.Point(617, 404);
            this.PushToServerButton.Name = "PushToServerButton";
            this.PushToServerButton.Size = new System.Drawing.Size(202, 43);
            this.PushToServerButton.TabIndex = 4;
            this.PushToServerButton.Text = "Push to Server";
            this.PushToServerButton.UseVisualStyleBackColor = true;
            this.PushToServerButton.Click += new System.EventHandler(this.PushToServerButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // StatusBar
            // 
            this.StatusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusLabel});
            this.StatusBar.Location = new System.Drawing.Point(0, 461);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(1012, 25);
            this.StatusBar.TabIndex = 5;
            this.StatusBar.Text = "statusStrip1";
            // 
            // StatusLabel
            // 
            this.StatusLabel.Name = "StatusLabel";
            this.StatusLabel.Size = new System.Drawing.Size(49, 20);
            this.StatusLabel.Text = "Status";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(878, 282);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "API Key";
            // 
            // ApiKeyTextBox
            // 
            this.ApiKeyTextBox.Location = new System.Drawing.Point(840, 319);
            this.ApiKeyTextBox.Name = "ApiKeyTextBox";
            this.ApiKeyTextBox.Size = new System.Drawing.Size(142, 22);
            this.ApiKeyTextBox.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 486);
            this.Controls.Add(this.ApiKeyTextBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.PushToServerButton);
            this.Controls.Add(this.ServerUrlTextBox);
            this.Controls.Add(this.CreateNuspecButton);
            this.Controls.Add(this.CreatePackageButton);
            this.Controls.Add(this.SelectArtifactsButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ArtifactsListView);
            this.Name = "Form1";
            this.Text = "Nuget Package Publisher";
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView ArtifactsListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button SelectArtifactsButton;
        private System.Windows.Forms.Button CreatePackageButton;
        private System.Windows.Forms.Button CreateNuspecButton;
        private System.Windows.Forms.TextBox ServerUrlTextBox;
        private System.Windows.Forms.Button PushToServerButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolStripStatusLabel StatusLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ApiKeyTextBox;
    }
}

