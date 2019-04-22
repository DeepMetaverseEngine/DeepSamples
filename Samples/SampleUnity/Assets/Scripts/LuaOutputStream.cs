using DeepCore.IO;
using System;
using System.IO;


public class LuaOutputStream : IDisposable
{
    private OutputStream stream;
	public MemoryStream Buffer
	{
		get
		{
			return memStream;
		}
	}
    private MemoryStream memStream;
    public LuaOutputStream()
    {
        memStream = new MemoryStream();
        stream = new OutputStream(memStream, null);
    }

    public void PutU8(byte v)
    {
        stream.PutU8(v);
    }

    public void PutS8(sbyte v)
    {
        stream.PutS8(v);
    }

    public void PutS16(short v)
    {
        stream.PutS16(v);
    }

    public void PutU16(ushort v)
    {
        stream.PutU16(v);
    }

    public void PutS32(int v)
    {
        stream.PutS32(v);
    }

    public void PutU32(uint v)
    {
        stream.PutU32(v);
    }


    public void PutS64(long v)
    {
        stream.PutS64(v);
    }

    public void PutU64(ulong v)
    {
        stream.PutU64(v);
    }

    public void PutF32(float v)
    {
        stream.PutF32(v);
    }

    public void PutF64(double v)
    {
        stream.PutF64(v);
    }

    public void PutBool(bool v)
    {
        stream.PutBool(v);
    }

    public void PutUnicode(char v)
    {
        stream.PutUnicode(v);
    }

    public void PutUTF(string v)
    {
        stream.PutUTF(v);
    }

    public void PutEnum8(ValueType v)
    {
        stream.PutU8(Convert.ToByte(v));
    }

    public void PutEnum32(ValueType v)
    {
        stream.PutS32(Convert.ToInt32(v));
    }
    public void PutDateTime(DateTime v)
    {
        stream.PutDateTime(v);
    }

    public void PutTimeSpan(TimeSpan v)
    {
        stream.PutTimeSpan(v);
    }

    public void PutBytes(byte[] v)
    {
        stream.PutBytes(v);
    }

    public void PutVS32(int v)
    {
        stream.PutVS32(v);
    }

    public void Dispose()
    {
        memStream.Dispose();
        stream.Dispose();
    }

    public byte[] ToArray()
    {
        return memStream.ToArray();
    }
}