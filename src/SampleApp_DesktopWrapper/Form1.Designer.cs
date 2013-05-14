using Gekkota;

namespace SampleApp
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
			this._gekkotaControl = new Gekkota.GekkotaControl();
			this.SuspendLayout();
			// 
			// geckoRSharpControl1
			// 
			this._gekkotaControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this._gekkotaControl.Location = new System.Drawing.Point(0, 0);
			this._gekkotaControl.Name = "_gekkotaControl";
			this._gekkotaControl.Size = new System.Drawing.Size(566, 437);
			this._gekkotaControl.TabIndex = 0;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(566, 437);
			this.Controls.Add(this._gekkotaControl);
			this.Name = "Form1";
			this.Text = "GeckoRSharp Sample App";
			this.ResumeLayout(false);

		}

		#endregion

		private GekkotaControl _gekkotaControl;

	}
}

