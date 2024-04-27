// Decompiled with JetBrains decompiler
// Type: ConfigDialogForm.Updateform
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;


namespace ConfigDialogForm
{
  public class Updateform : Form
  {
    public string link = "http://www.kinoni.com/rdrivers";
    private IContainer components;
    private TextBox textBox1;
    private Button button1;
    public LinkLabel linkLabel1;

    public Updateform() => this.InitializeComponent();

    private void button1_Click(object sender, EventArgs e) => this.Close();

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Process.Start(this.link);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.textBox1 = new TextBox();
      this.linkLabel1 = new LinkLabel();
      this.button1 = new Button();
      this.SuspendLayout();
      this.textBox1.BackColor = SystemColors.Control;
      this.textBox1.BorderStyle = BorderStyle.None;
      this.textBox1.Location = new Point(18, 39);
      this.textBox1.Margin = new Padding(4, 5, 4, 5);
      this.textBox1.Multiline = true;
      this.textBox1.Name = "textBox1";
      this.textBox1.Size = new Size(390, 92);
      this.textBox1.TabIndex = 0;
      this.textBox1.Text = "Update is available. Please update to latest version for stability, best performance and latest features.";
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Location = new Point(98, 114);
      this.linkLabel1.Margin = new Padding(4, 0, 4, 0);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new Size(228, 21);
      this.linkLabel1.TabIndex = 1;
      ((Label) this.linkLabel1).TabStop = true;
      this.linkLabel1.Text = "http://www.kinoni.com/rdrivers";
      this.linkLabel1.TextAlign = ContentAlignment.MiddleCenter;
      this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      this.button1.Location = new Point(158, 169);
      this.button1.Margin = new Padding(4, 5, 4, 5);
      this.button1.Name = "button1";
      this.button1.Size = new Size(112, 40);
      this.button1.TabIndex = 2;
      this.button1.Text = "OK";
      this.button1.UseVisualStyleBackColor = true;
      this.button1.Click += new EventHandler(this.button1_Click);
      this.AutoScaleDimensions = new SizeF(9f, 21f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(426, 246);
      this.Controls.Add((Control) this.button1);
      this.Controls.Add((Control) this.linkLabel1);
      this.Controls.Add((Control) this.textBox1);
      this.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.Margin = new Padding(4, 5, 4, 5);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (Updateform);
      this.ShowIcon = false;
      this.Text = "Kinoni Remote Desktop";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
