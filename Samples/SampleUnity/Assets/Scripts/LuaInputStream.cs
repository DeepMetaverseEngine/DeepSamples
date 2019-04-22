using DeepCore.IO;
using System;
using System.IO;


public class LuaInputStream : IDisposable
{
    private InputStream stream;
    private MemoryStream memStream;
    public LuaInputStream(byte[] bytes)
    {
        memStream = new MemoryStream(bytes);
        stream = new InputStream(memStream, null);
    }

    public object GetU8()
    {
        return stream.GetU8();
    }

    public object GetS8()
    {
        return stream.GetS8();
    }

    public object GetS16()
    {
        return stream.GetS16();
    }

    public object GetU16()
    {
        return stream.GetU16();
    }

    public object GetS32()
    {
        return stream.GetS32();
    }

    public object GetU32()
    {
        return stream.GetU32();
    }


    public object GetS64()
    {
        return stream.GetS64();
    }

    public object GetU64()
    {
        return stream.GetU64();
    }

    public object GetF32()
    {
        return stream.GetF32();
    }

    public object GetF64()
    {
        return stream.GetF64();
    }

    public object GetBool()
    {
        return stream.GetBool();
    }

    public object GetUnicode()
    {
        return stream.GetUnicode();
    }

    public object GetUTF()
    {
        return stream.GetUTF();
    }

    public object GetEnum8()
    {
        return GetU8();
    }

    public object GetEnum32()
    {
        return GetS32();
    }

    public object GetDateTime()
    {
        return stream.GetDateTime();
    }

    public object GetTimeSpan()
    {
        return stream.GetTimeSpan();
    }

    public object GetBytes()
    {
        return stream.GetBytes();
    }


    public object GetVS32()
    {
        return stream.GetVS32();
    }

    public void Dispose()
    {
        memStream.Dispose();
        stream.Dispose();
    }

    
}