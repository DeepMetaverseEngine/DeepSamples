﻿using DeepCore.IO;
using DeepCore.Unity3D.Impl;
using System;
using System.IO;
using UnityEngine;

namespace DeepCore.Unity3D.Src.M3Z
{
    //////////////////////////////////////////////////////////////////////////
    // M3Z
    //////////////////////////////////////////////////////////////////////////

    public enum M3ZType
    {
        M3Z_TYPE_PNG = 0x00474e50,
        M3Z_TYPE_JPG = 0x0047504a,
        M3Z_TYPE_BMP = 0x00504d42,

        M3Z_TYPE_PVR4 = 0x34525650,
        M3Z_TYPE_PVR2 = 0x32525650,

        M3Z_TYPE_PVR1 = 0x31525650,
        M3Z_TYPE_PVRA = 0x41525650,

        M3Z_TYPE_PKM1 = 0x314d4b50,
        M3Z_TYPE_PKMA = 0x414d4b50,

        M3Z_TYPE_ATC_E = 0x45435441,
        M3Z_TYPE_ATC_I = 0x49435441,
        M3Z_TYPE_ATC_RGB = 0x33435441,

        M3Z_TYPE_ETC1 = 0x31435445,
        M3Z_TYPE_ETCA = 0x41435445,

        M3Z_TYPE_ETC2 = 0x32435445,
        M3Z_TYPE_ASTC = 0x43545341,

        M3Z_TYPE_RGBA8 = 0x41424752,
        M3Z_TYPE_A8 = 0x00003841,

        M3Z_TYPE_DXT1 = 0x31545844,
        M3Z_TYPE_DXT3 = 0x33545844,
        M3Z_TYPE_DXT5 = 0x35545844,
    }

    public class M3ZHeader
    {
        public const uint M3Z_HEADER = 0x5a33464d; //"MF3Z";

        /**文件头*/
        public uint header { get; private set; }
        /**文件尺寸*/
        public uint version { get; private set; }
        /**原始图片宽*/
        public int srcWidth { get; private set; }
        /**原始图片高*/
        public int srcHeight { get; private set; }
        /**原始图片是否包含半透明*/
        public bool srcHasAlpha { get; private set; }
        /**附加数据*/
        public string extUTFData { get; private set; }
        /**纹理数量*/
        public int trunkCount { get; private set; }
        /**纹理块*/
        public M3ZTrunk[] trunks { get; private set; }

        public M3ZHeader(Stream data)
        {
            this.header = LittleEdian.GetU32(data);
            if (header != M3Z_HEADER)
            {
                throw new Exception("Invalid M3Z data");
            }
            this.version = LittleEdian.GetU32(data);

            const uint v_0100 = 0x00000100;

            if (version == v_0100)
            {
                srcWidth = LittleEdian.GetS32(data);
                srcHeight = LittleEdian.GetS32(data);
                srcHasAlpha = LittleEdian.GetBool(data);
                extUTFData = LittleEdian.GetUTF(data);
                trunkCount = LittleEdian.GetS32(data);

                trunks = new M3ZTrunk[trunkCount];
                for (int i = 0; i < trunkCount; i++)
                {
                    trunks[i] = new M3ZTrunk(data, version);
                }
            }
            else
            {
                // 君王2老M3Z文件格式代码
                srcWidth = LittleEdian.GetS32(data);
                srcHeight = LittleEdian.GetS32(data);
                srcHasAlpha = LittleEdian.GetBool(data);
                trunkCount = LittleEdian.GetS32(data);

                trunks = new M3ZTrunk[trunkCount];
                for (int i = 0; i < trunkCount; i++)
                {
                    trunks[i] = new M3ZTrunk(data, version);
                }
            }
        }

    }

    public class M3ZTrunk
    {
        /// <summary>
        /// 类型
        /// </summary>
        public M3ZType type { get; private set; }
        public uint flags { get; private set; }
        /// <summary>
        /// 是否包含半透明
        /// </summary>
        public bool hasAlpha { get; private set; }
        /// <summary>
        /// 二的冥宽
        /// </summary>
        public int pixelW { get; private set; }
        /// <summary>
        /// 二的冥高
        /// </summary>
        public int pixelH { get; private set; }
        /// <summary>
        /// 实际像素点宽
        /// </summary>
        public int realPixelW { get; private set; }
        /// <summary>
        /// 实际像素点高
        /// </summary>
        public int realPixelH { get; private set; }
        /// <summary>
        /// 扩展数据
        /// </summary>
        public string extUTFData { get; private set; }
        /// <summary>
        /// 数据段尺寸
        /// </summary>
        public int fileSize { get; private set; }

        public ICompressTextureData texData { get; private set; }

        public M3ZTrunk(Stream data, uint version)
        {
            const uint v_0100 = 0x00000100;

            if (version == v_0100)
            {
                this.type = (M3ZType)LittleEdian.GetU32(data);
                this.hasAlpha = LittleEdian.GetBool(data);
                this.pixelW = LittleEdian.GetS32(data);
                this.pixelH = LittleEdian.GetS32(data);
                this.realPixelW = LittleEdian.GetS32(data);
                this.realPixelH = LittleEdian.GetS32(data);
                this.extUTFData = LittleEdian.GetUTF(data);
                this.fileSize = LittleEdian.GetS32(data);
            }
            else
            {
                this.type = (M3ZType)LittleEdian.GetU32(data);
                this.flags = LittleEdian.GetU32(data);
                this.hasAlpha = LittleEdian.GetBool(data);
                this.pixelW = LittleEdian.GetS32(data);
                this.pixelH = LittleEdian.GetS32(data);
                this.realPixelW = pixelW;
                this.realPixelH = pixelH;
                this.fileSize = LittleEdian.GetS32(data);
            }

            switch (type)
            {
                case M3ZType.M3Z_TYPE_PVR2:
                case M3ZType.M3Z_TYPE_PVR4:
                case M3ZType.M3Z_TYPE_PVR1:
                case M3ZType.M3Z_TYPE_PVRA:
#if ASTC
                    this.texData = new ASTCTexHeader();
#else
                    this.texData = new PVRTexHeader();
#endif
                    break;
                case M3ZType.M3Z_TYPE_ETC1:
                case M3ZType.M3Z_TYPE_ETCA:
#if ETC2
                    this.texData = new PKMTexHeader();
#elif ASTC
                    this.texData = new ASTCTexHeader();
#else
                    this.texData = new KTXTexHeader();
#endif
                    break;

                case M3ZType.M3Z_TYPE_ETC2:
                    this.texData = new PKMTexHeader();
                    break;
                case M3ZType.M3Z_TYPE_PKM1:
                case M3ZType.M3Z_TYPE_PKMA:
                    this.texData = new PKMTexHeader();
                    break;
                case M3ZType.M3Z_TYPE_ASTC:
                    this.texData = new ASTCTexHeader();
                    break;
                default:
                    this.texData = new UnknowTexHeader();
                    break;
            }
            if (texData != null)
            {
                texData.Decode(this, data);
            }
        }

        public Texture2D LoadRawTextureData()
        {
            if (texData != null)
            {
                return texData.LoadRawTextureData(this);
            }
            return null;
        }

        public static TextureFormat GetTextureFormat(M3ZType mtype)
        {
            switch (mtype)
            {
                case M3ZType.M3Z_TYPE_PNG:
                case M3ZType.M3Z_TYPE_JPG:
                case M3ZType.M3Z_TYPE_BMP:
                    return TextureFormat.RGBA32;

                case M3ZType.M3Z_TYPE_PVR2:
                    return TextureFormat.PVRTC_RGBA2;
                case M3ZType.M3Z_TYPE_PVR4:
                case M3ZType.M3Z_TYPE_PVR1:
                case M3ZType.M3Z_TYPE_PVRA:

                case M3ZType.M3Z_TYPE_PKM1:
                case M3ZType.M3Z_TYPE_PKMA:
                    return TextureFormat.ETC_RGB4;

                case M3ZType.M3Z_TYPE_ATC_E:
                case M3ZType.M3Z_TYPE_ATC_I:
                case M3ZType.M3Z_TYPE_ATC_RGB:
                    return TextureFormat.ATC_RGB4;
                case M3ZType.M3Z_TYPE_ASTC:
                    return TextureFormat.ASTC_RGBA_4x4;
                case M3ZType.M3Z_TYPE_ETC1:
                case M3ZType.M3Z_TYPE_ETCA:
#if ETC2
                    return TextureFormat.ETC2_RGBA8;
#elif ASTC
                    return TextureFormat.ASTC_RGBA_4x4;
#else
                    return TextureFormat.ETC_RGB4;             
#endif

                case M3ZType.M3Z_TYPE_ETC2:
                    return TextureFormat.ETC2_RGBA8;

                case M3ZType.M3Z_TYPE_RGBA8:
                    return TextureFormat.RGBA32;
                case M3ZType.M3Z_TYPE_A8:
                    return TextureFormat.Alpha8;

                case M3ZType.M3Z_TYPE_DXT1:
                    return TextureFormat.DXT1;
                case M3ZType.M3Z_TYPE_DXT3:
                    return TextureFormat.DXT5;
                case M3ZType.M3Z_TYPE_DXT5:
                    return TextureFormat.DXT5;
            }
            return TextureFormat.RGBA32;
        }
    }

    public interface ICompressTextureData
    {
        byte[] RawData { get; }
        void Decode(M3ZTrunk trunk, Stream stream);
        void Encode(Stream stream);
        Texture2D LoadRawTextureData(M3ZTrunk trunk);
    }

    public struct UnknowTexHeader : ICompressTextureData
    {
        public byte[] rawData;
        public byte[] RawData { get { return rawData; } }
        public void Decode(M3ZTrunk trunk, Stream stream)
        {
            this.rawData = new byte[trunk.fileSize];
            IOUtil.ReadToEnd(stream, rawData, 0, rawData.Length);
        }
        public void Encode(Stream stream)
        {
            IOUtil.WriteToEnd(stream, rawData, 0, rawData.Length);
        }

        public Texture2D LoadRawTextureData(M3ZTrunk trunk)
        {
            Texture2D tex = new Texture2D(trunk.pixelW, trunk.pixelH, M3ZTrunk.GetTextureFormat(trunk.type), false, true);
            tex.LoadRawTextureData(rawData);
            tex.Apply(false, true);
            return tex;
        }
    }

    public struct ASTCTexHeader : ICompressTextureData
    {
        public const int ASTC_HEADER_SIZE = 16;

        public byte[] magic; //4
        public byte[] blockdim_x;//1
        public byte[] blockdim_y;//1
        public byte[] blockdim_z;//1
        public byte[] xsize;//3
        public byte[] ysize;//3
        public byte[] zsize;//3

        public byte[] rawData;
        public byte[] RawData { get { return rawData; } }

        public void Decode(M3ZTrunk trunk, Stream stream)
        {
            this.magic = IOUtil.ReadExpect(stream, 4);
            this.blockdim_x = IOUtil.ReadExpect(stream, 1);
            this.blockdim_y = IOUtil.ReadExpect(stream, 1);
            this.blockdim_z = IOUtil.ReadExpect(stream, 1);
            this.xsize = IOUtil.ReadExpect(stream, 3);
            this.ysize = IOUtil.ReadExpect(stream, 3);
            this.zsize = IOUtil.ReadExpect(stream, 3);

            this.rawData = new byte[trunk.fileSize - ASTC_HEADER_SIZE];
            IOUtil.ReadToEnd(stream, rawData, 0, rawData.Length);
        }
        public void Encode(Stream stream)
        {
            IOUtil.WriteToEnd(stream, this.magic, 0, 4);
            IOUtil.WriteToEnd(stream, this.blockdim_x, 0, 1);
            IOUtil.WriteToEnd(stream, this.blockdim_y, 0, 1);
            IOUtil.WriteToEnd(stream, this.blockdim_z, 0, 1);
            IOUtil.WriteToEnd(stream, this.xsize, 0, 1);
            IOUtil.WriteToEnd(stream, this.ysize, 0, 1);
            IOUtil.WriteToEnd(stream, this.zsize, 0, 1);
            IOUtil.WriteToEnd(stream, rawData, 0, rawData.Length);
        }

        public Texture2D LoadRawTextureData(M3ZTrunk trunk)
        {
            Texture2D tex = new Texture2D(trunk.pixelW, trunk.pixelH, M3ZTrunk.GetTextureFormat(trunk.type), false, true);
            tex.LoadRawTextureData(rawData);
            tex.Apply(false, true);
            return tex;
        }

    }

    public struct PVRTexHeader : ICompressTextureData
    {
        public const int PVR_HEADER_SIZE = 52;

        public uint headerLength;
        public uint height;
        public uint width;
        public uint numMipmaps;
        public uint flags;
        public uint dataLength;
        public uint bpp;
        public uint bitmaskRed;
        public uint bitmaskGreen;
        public uint bitmaskBlue;
        public uint bitmaskAlpha;
        public uint pvrTag;
        public uint numSurfs;

        public byte[] rawData;
        public byte[] RawData { get { return rawData; } }

        public void Decode(M3ZTrunk trunk, Stream stream)
        {
            this.headerLength = LittleEdian.GetU32(stream);
            this.height = LittleEdian.GetU32(stream);
            this.width = LittleEdian.GetU32(stream);
            this.numMipmaps = LittleEdian.GetU32(stream);
            this.flags = LittleEdian.GetU32(stream);
            this.dataLength = LittleEdian.GetU32(stream);
            this.bpp = LittleEdian.GetU32(stream);
            this.bitmaskRed = LittleEdian.GetU32(stream);
            this.bitmaskGreen = LittleEdian.GetU32(stream);
            this.bitmaskBlue = LittleEdian.GetU32(stream);
            this.bitmaskAlpha = LittleEdian.GetU32(stream);
            this.pvrTag = LittleEdian.GetU32(stream);
            this.numSurfs = LittleEdian.GetU32(stream);

            this.rawData = new byte[dataLength];
            IOUtil.ReadToEnd(stream, rawData, 0, rawData.Length);
        }
        public void Encode(Stream stream)
        {
            LittleEdian.PutU32(stream, this.headerLength);
            LittleEdian.PutU32(stream, this.height);
            LittleEdian.PutU32(stream, this.width);
            LittleEdian.PutU32(stream, this.numMipmaps);
            LittleEdian.PutU32(stream, this.flags);
            LittleEdian.PutU32(stream, this.dataLength);
            LittleEdian.PutU32(stream, this.bpp);
            LittleEdian.PutU32(stream, this.bitmaskRed);
            LittleEdian.PutU32(stream, this.bitmaskGreen);
            LittleEdian.PutU32(stream, this.bitmaskBlue);
            LittleEdian.PutU32(stream, this.bitmaskAlpha);
            LittleEdian.PutU32(stream, this.pvrTag);
            LittleEdian.PutU32(stream, this.numSurfs);
            IOUtil.WriteToEnd(stream, rawData, 0, rawData.Length);
        }

        public Texture2D LoadRawTextureData(M3ZTrunk trunk)
        {
            Texture2D tex = new Texture2D(trunk.pixelW, trunk.pixelH, M3ZTrunk.GetTextureFormat(trunk.type), false, true);
            tex.LoadRawTextureData(rawData);
            tex.Apply(false, true);
            return tex;
        }

    }

    public struct PKMTexHeader : ICompressTextureData
    {
        public const int PKM_HEADER_SIZE = 16;

        public byte[] MagicNumber;  // 4 byte magic number: "PKM "
        public byte[] Version;      // 2 byte version "10"
        public byte[] DataType;     // 2 byte data type: 0 (ETC1_RGB_NO_MIPMAPS)
        public ushort ExtWidth;     // 16 bit big endian extended width
        public ushort ExtHeight;    // 16 bit big endian extended height
        public ushort SrcWidth;     // 16 bit big endian original width
        public ushort SrcHeight;    // 16 bit big endian original height

        public byte[] rawData;

        public byte[] RawData { get { return rawData; } }
        public void Decode(M3ZTrunk trunk, Stream stream)
        {
            this.MagicNumber = IOUtil.ReadExpect(stream, 4);
            this.Version = IOUtil.ReadExpect(stream, 2);
            this.DataType = IOUtil.ReadExpect(stream, 2);

            this.ExtWidth = BigEdian.GetU16(stream);
            this.ExtHeight = BigEdian.GetU16(stream);
            this.SrcWidth = BigEdian.GetU16(stream);
            this.SrcHeight = BigEdian.GetU16(stream);

            this.rawData = new byte[trunk.fileSize - PKM_HEADER_SIZE];
            IOUtil.ReadToEnd(stream, rawData, 0, rawData.Length);
        }
        public void Encode(Stream stream)
        {
            IOUtil.WriteToEnd(stream, this.MagicNumber, 0, 4);
            IOUtil.WriteToEnd(stream, this.Version, 0, 2);
            IOUtil.WriteToEnd(stream, this.DataType, 0, 2);

            BigEdian.PutU16(stream, this.ExtWidth);
            BigEdian.PutU16(stream, this.ExtHeight);
            BigEdian.PutU16(stream, this.SrcWidth);
            BigEdian.PutU16(stream, this.SrcHeight);

            IOUtil.WriteToEnd(stream, rawData, 0, rawData.Length);
        }

        public Texture2D LoadRawTextureData(M3ZTrunk trunk)
        {
            Texture2D tex = new Texture2D(trunk.pixelW, trunk.pixelH, M3ZTrunk.GetTextureFormat(trunk.type), false, true);
            tex.LoadRawTextureData(rawData);
            tex.Apply(false, true);
            return tex;
        }

    }

    public struct KTXTexHeader : ICompressTextureData
    {
        public const int KTX_HEADER_SIZE = 52 + 12;
        public static byte[] KTX_IDENTIFIER_REF = { 0xAB, 0x4B, 0x54, 0x58, 0x20, 0x31, 0x31, 0xBB, 0x0D, 0x0A, 0x1A, 0x0A };

        public byte[] identifier;
        public uint endianness;
        public uint glType;
        public uint glTypeSize;
        public uint glFormat;
        public uint glInternalFormat;
        public uint glBaseInternalFormat;
        public uint pixelWidth;
        public uint pixelHeight;
        public uint pixelDepth;
        public uint numberOfArrayElements;
        public uint numberOfFaces;
        public uint numberOfMipmapLevels;
        public uint bytesOfKeyValueData;

        public byte[] rawData;

        public byte[] RawData { get { return rawData; } }
        public void Decode(M3ZTrunk trunk, Stream stream)
        {
            this.identifier = IOUtil.ReadExpect(stream, 12);
            this.endianness = LittleEdian.GetU32(stream);
            this.glType = LittleEdian.GetU32(stream);
            this.glTypeSize = LittleEdian.GetU32(stream);
            this.glFormat = LittleEdian.GetU32(stream);
            this.glInternalFormat = LittleEdian.GetU32(stream);
            this.glBaseInternalFormat = LittleEdian.GetU32(stream);
            this.pixelWidth = LittleEdian.GetU32(stream);
            this.pixelHeight = LittleEdian.GetU32(stream);
            this.pixelDepth = LittleEdian.GetU32(stream);
            this.numberOfArrayElements = LittleEdian.GetU32(stream);
            this.numberOfFaces = LittleEdian.GetU32(stream);
            this.numberOfMipmapLevels = LittleEdian.GetU32(stream);
            this.bytesOfKeyValueData = LittleEdian.GetU32(stream);

            stream.Position += this.bytesOfKeyValueData;
            uint imageSize = LittleEdian.GetU32(stream);
            this.rawData = new byte[imageSize];
            IOUtil.ReadToEnd(stream, rawData, 0, rawData.Length);
        }
        public void Encode(Stream stream)
        {
            IOUtil.WriteToEnd(stream, this.identifier, 0, 12);
            LittleEdian.PutU32(stream, endianness);
            LittleEdian.PutU32(stream, glType);
            LittleEdian.PutU32(stream, glTypeSize);
            LittleEdian.PutU32(stream, glFormat);
            LittleEdian.PutU32(stream, glInternalFormat);
            LittleEdian.PutU32(stream, glBaseInternalFormat);
            LittleEdian.PutU32(stream, pixelWidth);
            LittleEdian.PutU32(stream, pixelHeight);
            LittleEdian.PutU32(stream, pixelDepth);
            LittleEdian.PutU32(stream, numberOfArrayElements);
            LittleEdian.PutU32(stream, numberOfFaces);
            LittleEdian.PutU32(stream, numberOfMipmapLevels);
            LittleEdian.PutU32(stream, bytesOfKeyValueData);

            LittleEdian.PutU32(stream, (uint)rawData.Length);
            IOUtil.WriteToEnd(stream, rawData, 0, rawData.Length);
        }

        public Texture2D LoadRawTextureData(M3ZTrunk trunk)
        {
            Texture2D tex = new Texture2D(trunk.pixelW, trunk.pixelH, M3ZTrunk.GetTextureFormat(trunk.type), false, true);
            tex.LoadRawTextureData(rawData);
            tex.Apply(false, true);
            return tex;
        }
    }

    public static class G3ZStream
    {

        public static Stream AllocDecompressToStream(byte[] data)
        {
            int pos = 0;
            int Head = (int)LittleEdian.GetU32(data, ref pos);
            int SrcSize = (int)LittleEdian.GetU32(data, ref pos);
            byte[] dst = new byte[SrcSize];
            UnityDriver.Platform.NativeDecompressMemory(
                new ArraySegment<byte>(data, pos, data.Length - pos),
                new ArraySegment<byte>(dst, 0, dst.Length));
            return MemoryStreamObjectPool.AllocAutoRelease(dst);
        }

    }
}
