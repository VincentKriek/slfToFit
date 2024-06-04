﻿namespace SlfToFit
{
	partial class Form1
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
			inputContainer = new FlowLayoutPanel();
			inputLabel = new Label();
			inputFilePathTextBox = new TextBox();
			browseInputButton = new Button();
			inputFileDialog = new OpenFileDialog();
			outputContainer = new FlowLayoutPanel();
			outputLabel = new Label();
			outputFilePathTextBox = new TextBox();
			browseOutputButton = new Button();
			outputFileDialog = new SaveFileDialog();
			startButton = new Button();
			inputContainer.SuspendLayout();
			outputContainer.SuspendLayout();
			SuspendLayout();
			// 
			// inputContainer
			// 
			inputContainer.Controls.Add(inputLabel);
			inputContainer.Controls.Add(inputFilePathTextBox);
			inputContainer.Controls.Add(browseInputButton);
			inputContainer.Location = new Point(12, 12);
			inputContainer.Name = "inputContainer";
			inputContainer.Size = new Size(718, 31);
			inputContainer.TabIndex = 0;
			// 
			// inputLabel
			// 
			inputLabel.AutoSize = true;
			inputLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
			inputLabel.Location = new Point(5, 5);
			inputLabel.Margin = new Padding(5, 5, 16, 5);
			inputLabel.Name = "inputLabel";
			inputLabel.Size = new Size(129, 17);
			inputLabel.TabIndex = 1;
			inputLabel.Text = "Input file destination:";
			inputLabel.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// inputFilePathTextBox
			// 
			inputFilePathTextBox.Location = new Point(153, 3);
			inputFilePathTextBox.Name = "inputFilePathTextBox";
			inputFilePathTextBox.PlaceholderText = "System file path";
			inputFilePathTextBox.Size = new Size(472, 23);
			inputFilePathTextBox.TabIndex = 2;
			// 
			// browseInputButton
			// 
			browseInputButton.Location = new Point(631, 3);
			browseInputButton.Name = "browseInputButton";
			browseInputButton.Size = new Size(75, 23);
			browseInputButton.TabIndex = 3;
			browseInputButton.Text = "Browse";
			browseInputButton.UseVisualStyleBackColor = true;
			browseInputButton.Click += BrowseInputButton_Click;
			// 
			// inputFileDialog
			// 
			inputFileDialog.DefaultExt = "slf";
			inputFileDialog.FileName = "InputFileDialog";
			inputFileDialog.Filter = "Sigma Log Files (*.slf) | *.slf";
			inputFileDialog.InitialDirectory = "C:\\";
			inputFileDialog.Title = "Load Activity";
			// 
			// outputContainer
			// 
			outputContainer.Controls.Add(outputLabel);
			outputContainer.Controls.Add(outputFilePathTextBox);
			outputContainer.Controls.Add(browseOutputButton);
			outputContainer.Location = new Point(12, 49);
			outputContainer.Name = "outputContainer";
			outputContainer.Size = new Size(718, 31);
			outputContainer.TabIndex = 4;
			// 
			// outputLabel
			// 
			outputLabel.AutoSize = true;
			outputLabel.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
			outputLabel.Location = new Point(5, 5);
			outputLabel.Margin = new Padding(5);
			outputLabel.Name = "outputLabel";
			outputLabel.Size = new Size(140, 17);
			outputLabel.TabIndex = 1;
			outputLabel.Text = "Output file destination:";
			outputLabel.TextAlign = ContentAlignment.MiddleCenter;
			// 
			// outputFilePathTextBox
			// 
			outputFilePathTextBox.Location = new Point(153, 3);
			outputFilePathTextBox.Name = "outputFilePathTextBox";
			outputFilePathTextBox.PlaceholderText = "System file path";
			outputFilePathTextBox.Size = new Size(472, 23);
			outputFilePathTextBox.TabIndex = 2;
			// 
			// browseOutputButton
			// 
			browseOutputButton.Location = new Point(631, 3);
			browseOutputButton.Name = "browseOutputButton";
			browseOutputButton.Size = new Size(75, 23);
			browseOutputButton.TabIndex = 3;
			browseOutputButton.Text = "Browse";
			browseOutputButton.UseVisualStyleBackColor = true;
			browseOutputButton.Click += BrowseOutputButton_Click;
			// 
			// outputFileDialog
			// 
			outputFileDialog.DefaultExt = "fit";
			outputFileDialog.Filter = "Flexible and Interoperable Data Transfer (*.fit) | *.fit";
			outputFileDialog.InitialDirectory = "C:\\";
			outputFileDialog.Title = "Export fit file";
			// 
			// startButton
			// 
			startButton.Location = new Point(12, 86);
			startButton.Name = "startButton";
			startButton.Size = new Size(105, 39);
			startButton.TabIndex = 5;
			startButton.Text = "Start conversion";
			startButton.UseVisualStyleBackColor = true;
			startButton.Click += StartButton_Click;
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(800, 450);
			Controls.Add(startButton);
			Controls.Add(inputContainer);
			Controls.Add(outputContainer);
			Name = "Form1";
			Text = "Form1";
			inputContainer.ResumeLayout(false);
			inputContainer.PerformLayout();
			outputContainer.ResumeLayout(false);
			outputContainer.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private FlowLayoutPanel inputContainer;
		private Label inputLabel;
		private OpenFileDialog inputFileDialog;
		private TextBox inputFilePathTextBox;
		private Button browseInputButton;
		private FlowLayoutPanel outputContainer;
		private Label outputLabel;
		private TextBox outputFilePathTextBox;
		private Button browseOutputButton;
		private SaveFileDialog outputFileDialog;
		private Button startButton;
	}
}
