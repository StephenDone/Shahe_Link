using System.Text;
using Windows.Security.Cryptography;
using Windows.Storage.Streams;

namespace Shahe_Link
{

    public static class BtByteArrayExtensions
    {
        public static UInt16 GetUInt16(this byte[] data, int index)
        {
            return (ushort)(
                data[index]
                | (data[index + 1] << 8)
            );
        }
        public static UInt32 GetUInt32(this byte[] data, int index)
        {
            return (ushort)(
                data[index]
                | (data[index + 1] << 8)
                | (data[index + 2] << 16)
                | (data[index + 3] << 24)
            );
        }

        public static Int16 GetInt16(this byte[] data, int index)
        {
            return (Int16)GetUInt16(data, index);
        }

        public static Single GetSingle(this byte[] data, int index)
        {
            return System.BitConverter.ToSingle(data, index);
        }

        public static void SetUInt16(this byte[] data, UInt16 value, int index)
        {
            data[index] = (byte)(value & 0xFF);
            data[index + 1] = (byte)(value >> 8);
        }

        public static void SetSInt16(this byte[] data, Int16 value, int index)
        {
            data[index] = (byte)(value & 0xFF);
            data[index + 1] = (byte)(value >> 8);
        }

        public static IBuffer ToIBuffer(this byte[] data)
        {
            return CryptographicBuffer.CreateFromByteArray(data);
        }
        public static string ToHex(this byte[] data) => BitConverter.ToString(data);
        public static string ToUTF8String(this byte[] data) => Encoding.UTF8.GetString(data, 0, data.Length);
    }

    public static class IBufferExtensions
    {
        public static byte[] ToByteArray(this IBuffer buffer)
        {
            CryptographicBuffer.CopyToByteArray(buffer, out byte[] data);
            return data;
        }

        public static string ToHex(this IBuffer Buffer)
        {
            return Buffer.ToByteArray().ToHex();
        }
    }


}