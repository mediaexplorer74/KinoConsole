// Decompiled with JetBrains decompiler
// Type: ConfigDialogForm.AlertForm
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;


namespace ConfigDialogForm
{
  public class AlertForm : Form
  {
    private IContainer components;
    public Label labelMessage;
    public ProgressBar progressBar1;
    public Button buttonCancel;
    public Button okButton;

    public string Message
    {
      set => this.labelMessage.Text = value;
    }

    public int ProgressValue
    {
      set => this.progressBar1.Value = value;
    }

    public AlertForm() => this.InitializeComponent();

    public event EventHandler<EventArgs> Canceled;

    private void OKbutton_Click(object sender, EventArgs e) => this.Close();

    private void buttonCancel_Click(object sender, EventArgs e)
    {
      EventHandler<EventArgs> canceled = this.Canceled;
      if (canceled == null)
        return;
      canceled((object) this, e);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.labelMessage = new Label();
      this.progressBar1 = new ProgressBar();
      this.buttonCancel = new Button();
      this.okButton = new Button();
      this.SuspendLayout();
      this.labelMessage.AutoSize = true;
      this.labelMessage.Font = new Font("Segoe UI", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.labelMessage.Location = new Point(32, 9);
      this.labelMessage.Name = "labelMessage";
      this.labelMessage.Size = new Size(52, 21);
      this.labelMessage.TabIndex = 0;
      this.labelMessage.Text = "Starting..";
      this.progressBar1.Location = new Point(21, 44);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new Size(237, 23);
      this.progressBar1.TabIndex = 1;
      this.buttonCancel.Location = new Point(197, 82);
      this.buttonCancel.Name = "buttonCancel";
      this.buttonCancel.Size = new Size(75, 23);
      this.buttonCancel.TabIndex = 2;
      this.buttonCancel.Text = "Cancel";
      this.buttonCancel.UseVisualStyleBackColor = true;
      this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
      this.okButton.Location = new Point(197, 82);
      this.okButton.Name = "okButton";
      this.okButton.Size = new Size(75, 23);
      this.okButton.TabIndex = 3;
      this.okButton.Text = "OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new EventHandler(this.OKbutton_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(284, 117);
      this.Controls.Add((Control) this.okButton);
      this.Controls.Add((Control) this.buttonCancel);
      this.Controls.Add((Control) this.progressBar1);
      this.Controls.Add((Control) this.labelMessage);
      this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
      this.Name = nameof (AlertForm);
      this.Text = "Scanning games";
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
