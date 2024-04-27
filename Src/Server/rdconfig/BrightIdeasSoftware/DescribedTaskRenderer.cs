// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.DescribedTaskRenderer
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.ComponentModel;
using System.Drawing;


namespace BrightIdeasSoftware
{
  public class DescribedTaskRenderer : BaseRenderer
  {
    private Font titleFont;
    private Color titleColor;
    private Font descriptionFont;
    private Color descriptionColor = Color.DimGray;
    private int imageTextSpace = 4;
    private string descriptionAspectName;
    private Munger descriptionGetter;

    [Category("ObjectListView")]
    [Description("The font that will be used to draw the title of the task")]
    [DefaultValue(null)]
    public Font TitleFont
    {
      get => this.titleFont;
      set => this.titleFont = value;
    }

    [Browsable(false)]
    public Font TitleFontOrDefault => this.TitleFont ?? this.ListView.Font;

    [Category("ObjectListView")]
    [DefaultValue(typeof (Color), "")]
    [Description("The color of the title")]
    public Color TitleColor
    {
      get => this.titleColor;
      set => this.titleColor = value;
    }

    [Browsable(false)]
    public Color TitleColorOrDefault
    {
      get
      {
        return this.IsItemSelected || this.TitleColor.IsEmpty ? this.GetForegroundColor() : this.TitleColor;
      }
    }

    [Description("The font that will be used to draw the description of the task")]
    [Category("ObjectListView")]
    [DefaultValue(null)]
    public Font DescriptionFont
    {
      get => this.descriptionFont;
      set => this.descriptionFont = value;
    }

    [Browsable(false)]
    public Font DescriptionFontOrDefault => this.DescriptionFont ?? this.ListView.Font;

    [Category("ObjectListView")]
    [DefaultValue(typeof (Color), "DimGray")]
    [Description("The color of the description")]
    public Color DescriptionColor
    {
      get => this.descriptionColor;
      set => this.descriptionColor = value;
    }

    [Browsable(false)]
    public Color DescriptionColorOrDefault
    {
      get
      {
        return this.DescriptionColor.IsEmpty || this.IsItemSelected && !this.ListView.UseTranslucentSelection ? this.GetForegroundColor() : this.DescriptionColor;
      }
    }

    [DefaultValue(4)]
    [Category("ObjectListView")]
    [Description("The number of pixels that that will be left between the image and the text")]
    public int ImageTextSpace
    {
      get => this.imageTextSpace;
      set => this.imageTextSpace = value;
    }

    [Category("ObjectListView")]
    [DefaultValue(null)]
    [Description("The name of the aspect of the model object that contains the task description")]
    public string DescriptionAspectName
    {
      get => this.descriptionAspectName;
      set => this.descriptionAspectName = value;
    }

    protected virtual string GetDescription()
    {
      if (string.IsNullOrEmpty(this.DescriptionAspectName))
        return string.Empty;
      if (this.descriptionGetter == null)
        this.descriptionGetter = new Munger(this.DescriptionAspectName);
      return this.descriptionGetter.GetValue(this.RowObject) as string;
    }

    public override void Render(Graphics g, Rectangle r)
    {
      this.DrawBackground(g, r);
      r = this.ApplyCellPadding(r);
      this.DrawDescribedTask(g, r, this.Aspect as string, this.GetDescription(), this.GetImage());
    }

    protected virtual void DrawDescribedTask(
      Graphics g,
      Rectangle r,
      string title,
      string description,
      Image image)
    {
      Rectangle rectangle1 = this.ApplyCellPadding(r);
      Rectangle rectangle2 = rectangle1;
      if (image != null)
      {
        g.DrawImage(image, rectangle1.Location);
        int num = image.Width + this.ImageTextSpace;
        rectangle2.X += num;
        rectangle2.Width -= num;
      }
      if (this.IsItemSelected && !this.ListView.UseTranslucentSelection)
      {
        using (SolidBrush solidBrush = new SolidBrush(this.GetTextBackgroundColor()))
          g.FillRectangle((Brush) solidBrush, rectangle2);
      }
      if (!string.IsNullOrEmpty(title))
      {
        using (StringFormat format = new StringFormat(StringFormatFlags.NoWrap))
        {
          format.Trimming = StringTrimming.EllipsisCharacter;
          format.Alignment = StringAlignment.Near;
          format.LineAlignment = StringAlignment.Near;
          Font titleFontOrDefault = this.TitleFontOrDefault;
          using (SolidBrush solidBrush = new SolidBrush(this.TitleColorOrDefault))
            g.DrawString(title, titleFontOrDefault, (Brush) solidBrush, (RectangleF) rectangle2, format);
          SizeF sizeF = g.MeasureString(title, titleFontOrDefault, rectangle2.Width, format);
          rectangle2.Y += (int) sizeF.Height;
          rectangle2.Height -= (int) sizeF.Height;
        }
      }
      if (string.IsNullOrEmpty(description))
        return;
      using (StringFormat format = new StringFormat())
      {
        format.Trimming = StringTrimming.EllipsisCharacter;
        using (SolidBrush solidBrush = new SolidBrush(this.DescriptionColorOrDefault))
          g.DrawString(description, this.DescriptionFontOrDefault, (Brush) solidBrush, (RectangleF) rectangle2, format);
      }
    }

    protected override void HandleHitTest(Graphics g, OlvListViewHitTestInfo hti, int x, int y)
    {
      if (!this.Bounds.Contains(x, y))
        return;
      hti.HitTestLocation = HitTestLocation.Text;
    }
  }
}
