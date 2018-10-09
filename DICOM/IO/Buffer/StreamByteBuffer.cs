﻿// Copyright (c) 2012-2018 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

using System.IO;

namespace Dicom.IO.Buffer
{
    public sealed class StreamByteBuffer : IByteBuffer
    {

        public StreamByteBuffer(Stream stream, long position, uint length)
        {
            Stream = stream;
            Position = position;
            Size = length;
        }

        public bool IsMemory => false;

        public Stream Stream { get; private set; }

        public long Position { get; private set; }

        public uint Size { get; private set; }

        public byte[] Data
        {
            get
            {
                if (!Stream.CanRead) throw new DicomIoException("cannot read from stream - maybe closed");
                byte[] data = new byte[Size];
                Stream.Position = Position;
                Stream.Read(data, 0, (int)Size);
                return data;
            }
        }

        public byte[] GetByteRange(int offset, int count)
        {
            if (!Stream.CanRead) throw new DicomIoException("cannot read from stream - maybe closed");
            byte[] buffer = new byte[count];
            Stream.Position = Position + offset;
            Stream.Read(buffer, 0, count);
            return buffer;
        }
    }
}
