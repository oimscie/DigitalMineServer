
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary
{
    public static class BitConvert
    {
        public static bool islittleEndian = true;

        /// <summary>
        /// 获取或设置字节序
        /// </summary>
        public static bool IsBigEndian
        {
            get { return islittleEndian; }
            set { islittleEndian = value; }
        }
        /// <summary>
        /// 检查系统运行的字节序,true为小端,否则为大端
        /// </summary>
        /// <returns></returns>
        public static bool CheckSysIsBigEndian()
        {
            UInt16 flag = 0x4321;
            if ((byte)(flag >> 8) == 0x43)
                return true;
            else return false;
        }

        public static byte[] ToBytes(this UInt16 value)
        {
            if (islittleEndian)
            {
                return new byte[]{
                     (byte)(value >> 8),
                     (byte)value
                };
            }

            return new byte[] {
                (byte)value,
                (byte)(value>>8)
            };
        }

        public static byte[] ToBytes(this UInt32 value)
        {
            if (islittleEndian)
            {
                return new byte[] {
                 (byte)(value >> 24),
                 (byte)(value >> 16),
                 (byte)(value >> 8),
                 (byte)value
                 };
            }

            return new byte[] {
                (byte)value,
                (byte)(value >> 8),
                (byte)(value >> 16),
                (byte)(value >> 24)
            };
        }

        public static byte[] ToBytes(this UInt64 value)
        {
            if (islittleEndian)
            {
                return new byte[]{
                  (byte)(value >> 56),
                  (byte)(value >> 48),
                  (byte)(value >> 40),
                  (byte)(value >> 32),
                  (byte)(value >> 24),
                  (byte)(value >> 16),
                  (byte)(value >> 8),
                  (byte)value
             };
            }

            return new byte[] {
            (byte)(byte)value,
                  (byte)(value >> 8),
                  (byte)(value >> 16),
                  (byte)(value >> 24),
                  (byte)(value >> 32),
                  (byte)(value >> 40),
                  (byte)(value >> 48),
                  (byte)(value >> 56)
            };
        }

        public static UInt16 ToUInt16(this byte[] value, int offset)
        {
            if (islittleEndian)
            {
                return (UInt16)((value[offset] << 8) | value[offset + 1]);
            }

            return (UInt16)(value[offset] | (value[offset + 1] << 8));
        }

        public static UInt32 ToUInt32(this byte[] value, int offset)
        {
            if (islittleEndian)
            {
                return (((UInt32)value[offset] << 24)
                   | ((UInt32)value[offset + 1] << 16)
                   | ((UInt32)value[offset + 2] << 8)
                   | value[offset + 3]);
            }

            return value[offset]
                   | ((UInt32)value[offset + 1] << 8)
                   | ((UInt32)value[offset + 2] << 16)
                   | ((UInt32)value[offset + 3] << 24);
        }

        public static UInt64 ToUInt64(this byte[] value, int offset)
        {
            if (islittleEndian)
            {
                return (((UInt64)value[offset] << 56)
                  | ((UInt64)value[offset + 1] << 48)
                  | ((UInt64)value[offset + 2] << 40)
                  | ((UInt64)value[offset + 3] << 32)
                  | ((UInt64)value[offset + 4] << 24)
                  | ((UInt64)value[offset + 5] << 16)
                  | ((UInt64)value[offset + 6] << 8)
                  | value[offset + 7]);
            }

            return value[offset]
                 | ((UInt64)value[offset + 1] << 8)
                 | ((UInt64)value[offset + 2] << 16)
                 | ((UInt64)value[offset + 3] << 24)
                 | ((UInt64)value[offset + 4] << 32)
                 | ((UInt64)value[offset + 5] << 40)
                 | ((UInt64)value[offset + 6] << 48)
                 | ((UInt64)value[offset + 7] << 56);
        }

        /**
         * Byte转Bit
         */
        public static string ByteToBit(byte b)
        {
            if (islittleEndian)
            {
                return "" + (byte)((b >> 7) & 0x1) +
                            (byte)((b >> 6) & 0x1) +
                            (byte)((b >> 5) & 0x1) +
                            (byte)((b >> 4) & 0x1) +
                            (byte)((b >> 3) & 0x1) +
                            (byte)((b >> 2) & 0x1) +
                            (byte)((b >> 1) & 0x1) +
                            (byte)((b >> 0) & 0x1);
            }
            else
            {
                return "" + (byte)((b >> 0) & 0x1) +
                            (byte)((b >> 1) & 0x1) +
                            (byte)((b >> 2) & 0x1) +
                            (byte)((b >> 3) & 0x1) +
                            (byte)((b >> 4) & 0x1) +
                            (byte)((b >> 5) & 0x1) +
                            (byte)((b >> 6) & 0x1) +
                            (byte)((b >> 7) & 0x1);
            }
        }

        public static byte[] UInt32ToBit(UInt32 b)
        {
            if (islittleEndian)
            {
                byte[] result = new byte[32];
                for (int i = 0; i < 32; i++)
                {
                    result[i] = (byte)((b >> i) & 0x1);
                }
                return result;
            }
            else
            {
                byte[] result = new byte[32];
                for (int i = 31; i >= 0; i--)
                {
                    result[i] = (byte)((b >> i) & 0x1);
                }
                return result;
            }
        }
    }
}
