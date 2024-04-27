// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.TextAdornment
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace BrightIdeasSoftware
{
  public class TextAdornment : GraphicAdornment
  {
    private Color backColor = Color.Empty;
    private Color borderColor = Color.Empty;
    private float borderWidth;
    private float cornerRounding = 16f;
    private Font font;
    private int maximumTextWidth;
    private StringFormat stringFormat;
    private string text;
    private Color textColor = Color.DarkBlue;
    private bool wrap = true;
    private int workingTransparency;

    [DefaultValue(typeof (Color), "")]
    [Description("The background color of the text")]
    [Category("ObjectListView")]
    public Color BackColor
    {
      get => this.backColor;
      set => this.backColor = value;
    }

    [Browsable(false)]
    public Brush BackgroundBrush
    {
      get => (Brush) new SolidBrush(Color.FromArgb(this.workingTransparency, this.BackColor));
    }

    [Category("ObjectListView")]
    [Description("The color of the border around the text")]
    [DefaultValue(typeof (Color), "")]
    public Color BorderColor
    {
      get => this.borderColor;
      set => this.borderColor = value;
    }

    [Browsable(false)]
    public Pen BorderPen
    {
      get => new Pen(Color.FromArgb(this.workingTransparency, this.BorderColor), this.BorderWidth);
    }

    [Description("The width of the border around the text")]
    [Category("ObjectListView")]
    [DefaultValue(0.0f)]
    public float BorderWidth
    {
      get => this.borderWidth;
      set => this.borderWidth = value;
    }

    [Category("ObjectListView")]
    [Description("How rounded should the corners of the border be? 0 means no rounding.")]
    [DefaultValue(16f)]
    [NotifyParentProperty(true)]
    public float CornerRounding
    {
      get => this.cornerRounding;
      set => this.cornerRounding = value;
    }

    [Description("The font that will be used to draw the text")]
    [Category("ObjectListView")]
    [DefaultValue(null)]
    [NotifyParentProperty(true)]
    public Font Font
    {
      get => this.font;
      set => this.font = value;
    }

    [Browsable(false)]
    public Font FontOrDefault => this.Font ?? new Font("Tahoma", 16f);

    [Browsable(false)]
    public bool HasBackground => this.BackColor != Color.Empty;

    [Browsable(false)]
    public bool HasBorder => this.BorderColor != Color.Empty && (double) this.BorderWidth > 0.0;

    [Category("ObjectListView")]
    [Description("The maximum width the text (0 means no maximum). Text longer than this will wrap")]
    [DefaultValue(0)]
    public int MaximumTextWidth
    {
      get => this.maximumTextWidth;
      set => this.maximumTextWidth = value;
    }

    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public virtual StringFormat StringFormat
    {
      get
      {
        if (this.stringFormat == null)
        {
          this.stringFormat = new StringFormat();
          this.stringFormat.Alignment = StringAlignment.Center;
          this.stringFormat.LineAlignment = StringAlignment.Center;
          this.stringFormat.Trimming = StringTrimming.EllipsisCharacter;
          if (!this.Wrap)
            this.stringFormat.FormatFlags = StringFormatFlags.NoWrap;
        }
        return this.stringFormat;
      }
      set => this.stringFormat = value;
    }

    [Description("The text that will be drawn over the top of the ListView")]
    [Category("ObjectListView")]
    [DefaultValue(null)]
    [NotifyParentProperty(true)]
    [Localizable(true)]
    public string Text
    {
      get => this.text;
      set => this.text = value;
    }

    [Browsable(false)]
    public Brush TextBrush
    {
      get => (Brush) new SolidBrush(Color.FromArgb(this.workingTransparency, this.TextColor));
    }

    [Category("ObjectListView")]
    [NotifyParentProperty(true)]
    [Description("The color of the text")]
    [DefaultValue(typeof (Color), "DarkBlue")]
    public Color TextColor
    {
      get => this.textColor;
      set => this.textColor = value;
    }

    [Category("ObjectListView")]
    [DefaultValue(true)]
    [Description("Will the text wrap?")]
    public bool Wrap
    {
      get => this.wrap;
      set => this.wrap = value;
    }

    public virtual void DrawText(Graphics g, Rectangle r)
    {
      this.DrawText(g, r, this.Text, this.Transparency);
    }

    public virtual void DrawText(Graphics g, Rectangle r, string s, int transparency)
    {
      if (string.IsNullOrEmpty(s))
        return;
      Rectangle textBounds = this.CalculateTextBounds(g, r, s);
      this.DrawBorderedText(g, textBounds, s, transparency);
    }

    protected virtual void DrawBorderedText(
      Graphics g,
      Rectangle textRect,
      string text,
      int transparency)
    {
      Rectangle rect = textRect;
      rect.Inflate((int) this.BorderWidth / 2, (int) this.BorderWidth / 2);
      --rect.Y;
      try
      {
        this.ApplyRotation(g, textRect);
        using (GraphicsPath roundedRect = this.GetRoundedRect(rect, this.CornerRounding))
        {
          this.workingTransparency = transparency;
          if (this.HasBackground)
          {
            using (Brush backgroundBrush = this.BackgroundBrush)
              g.FillPath(backgroundBrush, roundedRect);
          }
          using (Brush textBrush = this.TextBrush)
            g.DrawString(text, this.FontOrDefault, textBrush, (RectangleF) textRect, this.StringFormat);
          if (!this.HasBorder)
            return;
          using (Pen borderPen = this.BorderPen)
            g.DrawPath(borderPen, roundedRect);
        }
      }
      finally
      {
        this.UnapplyRotation(g);
      }
    }

    protected virtual Rectangle CalculateTextBounds(Graphics g, Rectangle r, string s)
    {
      int width = this.MaximumTextWidth <= 0 ? r.Width : this.MaximumTextWidth;
      SizeF sizeF = g.MeasureString(s, this.FontOrDefault, width, this.StringFormat);
      Size sz = new Size(1 + (int) sizeF.Width, 1 + (int) sizeF.Height);
      return this.CreateAlignedRectangle(r, sz);
    }

    protected virtual GraphicsPath GetRoundedRect(Rectangle rect, float diameter)
    {
      GraphicsPath roundedRect = new GraphicsPath();
      if ((double) diameter > 0.0)
      {
        RectangleF rect1 = new RectangleF((float) rect.X, (float) rect.Y, diameter, diameter);
        roundedRect.AddArc(rect1, 180f, 90f);
        rect1.X = (float) rect.Right - diameter;
        roundedRect.AddArc(rect1, 270f, 90f);
        rect1.Y = (float) rect.Bottom - diameter;
        roundedRect.AddArc(rect1, 0.0f, 90f);
        rect1.X = (float) rect.Left;
        roundedRect.AddArc(rect1, 90f, 90f);
        roundedRect.CloseFigure();
      }
      else
        roundedRect.AddRectangle(rect);
      return roundedRect;
    }
  }
}
