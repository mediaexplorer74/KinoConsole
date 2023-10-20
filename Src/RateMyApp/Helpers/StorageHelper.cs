// Decompiled with JetBrains decompiler
// Type: RateMyApp.Helpers.StorageHelper
// Assembly: RateMyApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 24764F99-C481-4414-9E6E-223BAD44A77B
// Assembly location: C:\Users\Admin\Desktop\re\KC\RateMyApp.dll

using System.IO.IsolatedStorage;

namespace RateMyApp.Helpers
{
  public class StorageHelper
  {
    public static bool StoreSetting(string key, object value, bool overwrite)
    {
      if (!overwrite && IsolatedStorageSettings.ApplicationSettings.Contains(key))
        return false;
      IsolatedStorageSettings.ApplicationSettings[key] = value;
      return true;
    }

    public static T GetSetting<T>(string key) => IsolatedStorageSettings.ApplicationSettings.Contains(key) ? (T) IsolatedStorageSettings.ApplicationSettings[key] : default (T);

    public static T GetSetting<T>(string key, T defaultVal) => IsolatedStorageSettings.ApplicationSettings.Contains(key) ? (T) IsolatedStorageSettings.ApplicationSettings[key] : defaultVal;

    public static void RemoveSetting(string key) => IsolatedStorageSettings.ApplicationSettings.Remove(key);
  }
}
