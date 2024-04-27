// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.Design.OverlayConverter
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System;
using System.ComponentModel;
using System.Globalization;


namespace BrightIdeasSoftware.Design
{
  internal class OverlayConverter : ExpandableObjectConverter
  {
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
    {
      return (object) destinationType == (object) typeof (string) || base.CanConvertTo(context, destinationType);
    }

    public override object ConvertTo(
      ITypeDescriptorContext context,
      CultureInfo culture,
      object value,
      Type destinationType)
    {
      if ((object) destinationType == (object) typeof (string))
      {
        switch (value)
        {
          case ImageOverlay imageOverlay:
            return imageOverlay.Image != null ? (object) "(set)" : (object) "(none)";
          case TextOverlay textOverlay:
            return !string.IsNullOrEmpty(textOverlay.Text) ? (object) "(set)" : (object) "(none)";
        }
      }
      return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}
