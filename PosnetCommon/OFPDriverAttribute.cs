// Decompiled with JetBrains decompiler
// Type: OnlineFPCommon.OFPDriverAttribute
// Assembly: OnlineFPCommon, Version=2018.0.1.0, Culture=neutral, PublicKeyToken=8b54098295e79123
// MVID: 96CC98E8-1BF6-4466-B7FE-A0A74E8FCBEF
// Assembly location: C:\Program Files (x86)\Comarch OPT!MA Detal\OnlineFPCommon.dll

using System;

namespace OnlineFPCommon
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
  public sealed class OFPDriverAttribute : Attribute
  {
    public string Name { get; set; }

    public string FriendlyName { get; set; }

    public bool IsNetworkDriver { get; set; }
  }
}
