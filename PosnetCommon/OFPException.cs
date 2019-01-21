// Decompiled with JetBrains decompiler
// Type: OnlineFPCommon.OFPException
// Assembly: OnlineFPCommon, Version=2018.0.1.0, Culture=neutral, PublicKeyToken=8b54098295e79123
// MVID: 96CC98E8-1BF6-4466-B7FE-A0A74E8FCBEF
// Assembly location: C:\Program Files (x86)\Comarch OPT!MA Detal\OnlineFPCommon.dll

using System;

namespace OnlineFPCommon
{
  public class OFPException : Exception
  {
    protected int number;
    protected string message;
    protected OFPExceptionType type;

    internal OFPException()
    {
    }

    public OFPException(string message, int? number, OFPExceptionType? type)
    {
      this.message = message;
      this.number = number.Value;
      this.type = type.Value;
    }

    public OFPException(int num)
    {
      this.number = num;
      this.type = OFPExceptionType.Fatal;
    }

    public OFPException(string msg)
    {
      this.message = msg;
      this.type = OFPExceptionType.Fatal;
    }

    public int Number
    {
      get
      {
        return this.number;
      }
    }

    public override string Message
    {
      get
      {
        return this.message;
      }
    }

    public OFPExceptionType Type
    {
      get
      {
        return this.type;
      }
    }
  }
}
