// Decompiled with JetBrains decompiler
// Type: BrightIdeasSoftware.IVirtualListDataSource
// Assembly: rdconfig, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: D72BB1D7-1CC3-4208-97DD-547FCD4348CA
// Assembly location: C:\Users\Admin\Desktop\RE\Kinoni\rdconfig.exe

using System.Collections;
using System.Windows.Forms;


namespace BrightIdeasSoftware
{
  public interface IVirtualListDataSource
  {
    object GetNthObject(int n);

    int GetObjectCount();

    int GetObjectIndex(object model);

    void PrepareCache(int first, int last);

    int SearchText(string value, int first, int last, OLVColumn column);

    void Sort(OLVColumn column, SortOrder order);

    void AddObjects(ICollection modelObjects);

    void RemoveObjects(ICollection modelObjects);

    void SetObjects(IEnumerable collection);
  }
}
