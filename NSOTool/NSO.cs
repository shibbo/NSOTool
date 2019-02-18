using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NSOTool
{
    class NSO
    {
        public NSO(string path)
        {
            BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open));
            mMagic = Encoding.ASCII.GetString(reader.ReadBytes(4));
            mVersion = reader.ReadInt32();
            reader.ReadInt32();
            mFlags = reader.ReadInt32();

            SegmentHeader textSeg = new SegmentHeader
            {
                fileOffset = reader.ReadInt32(),
                memoryOffset = reader.ReadInt32(),
                decompressedSize = reader.ReadInt32()
            };

            mTextSeg = textSeg;
            mModuleOffset = reader.ReadInt32();

            SegmentHeader rodataSeg = new SegmentHeader
            {
                fileOffset = reader.ReadInt32(),
                memoryOffset = reader.ReadInt32(),
                decompressedSize = reader.ReadInt32()
            };

            mRODataSeg = rodataSeg;
            mModuleFileSize = reader.ReadInt32();

            SegmentHeader dataSeg = new SegmentHeader
            {
                fileOffset = reader.ReadInt32(),
                memoryOffset = reader.ReadInt32(),
                decompressedSize = reader.ReadInt32()
            };

            mDataSeg = dataSeg;
            mBSSSize = reader.ReadInt32();
            mBuildID = reader.ReadBytes(0x20);
            mTextSize = reader.ReadInt32();
            mRODataSize = reader.ReadInt32();
            mDataSize = reader.ReadInt32();
            reader.ReadBytes(0x1C);

            RODataExtent apiExtent = new RODataExtent
            {
                regionRODataOffset = reader.ReadInt32(),
                regionSize = reader.ReadInt32()
            };

            mAPIInfoExtent = apiExtent;

            RODataExtent dynStrExtent = new RODataExtent
            {
                regionRODataOffset = reader.ReadInt32(),
                regionSize = reader.ReadInt32()
            };

            mDynStrExtent = dynStrExtent;

            RODataExtent dynSymExtent = new RODataExtent
            {
                regionRODataOffset = reader.ReadInt32(),
                regionSize = reader.ReadInt32()
            };

            mDynSymExtent = dynSymExtent;

            mTextSHA = reader.ReadBytes(0x20);
            mRODataSHA = reader.ReadBytes(0x20);
            mDataSHA = reader.ReadBytes(0x20);

            mText = reader.ReadBytes(mTextSize);
            mROData = reader.ReadBytes(mRODataSize);
            mData = reader.ReadBytes(mDataSize);
        }
  
        public struct SegmentHeader
        {
            public int fileOffset;
            public int memoryOffset;
            public int decompressedSize;
        }

        struct RODataExtent
        {
            public int regionRODataOffset;
            public int regionSize;
        }

        string mMagic;
        int mVersion;
        int mReserved;
        public int mFlags;
        public SegmentHeader mTextSeg;
        int mModuleOffset;
        public SegmentHeader mRODataSeg;
        int mModuleFileSize;
        public SegmentHeader mDataSeg;
        int mBSSSize;
        byte[] mBuildID;
        int mTextSize;
        int mRODataSize;
        int mDataSize;
        byte[] mReserved_1;
        RODataExtent mAPIInfoExtent;
        RODataExtent mDynStrExtent;
        RODataExtent mDynSymExtent;
        public byte[] mTextSHA;
        public byte[] mRODataSHA;
        public byte[] mDataSHA;

        byte[] mText;
        byte[] mROData;
        byte[] mData;
    }
}
