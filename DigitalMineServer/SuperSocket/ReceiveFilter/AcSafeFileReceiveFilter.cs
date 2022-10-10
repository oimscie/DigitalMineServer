using DigitalMineServer.Utils;
using JtLibrary;
using MySqlX.XDevAPI.Common;
using SuperSocket.Common;
using SuperSocket.Facility.Protocol;
using SuperSocket.SocketBase.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.SuperSocket.ReceiveFilter
{
    public class AcSafeFileReceiveFilter : IReceiveFilter<BinaryRequestInfo>
    {
        public int LeftBufferSize { get; }

        public IReceiveFilter<BinaryRequestInfo> NextReceiveFilter { get; }

        public FilterState State { get; private set; }

        private bool HasOrderHead = false;

        private byte[] residue = new byte[0];

        private bool needContant = false;

        /// <summary>
        /// 该方法将会在 SuperSocket 收到一块二进制数据时被执行，接收到的数据在 readBuffer 中从 offset 开始， 长度为 length 的部分。
        /// </summary>
        /// <param name="readBuffer">接收缓冲区, 接收到的数据存放在此数组里</param>
        /// <param name="offset">接收到的数据在接收缓冲区的起始位置</param>
        /// <param name="length">本轮接收到的数据的长度</param>
        /// <param name="toBeCopied">表示当你想缓存接收到的数据时，是否需要为接收到的数据重新创建一个备份而不是直接使用接收缓冲区</param>
        /// <param name="rest">这是一个输出参数, 它应该被设置为当解析到一个为政的请求后，接收缓冲区还剩余多少数据未被解析</param>
        /// <returns></returns>
        /// 当你在接收缓冲区中找到一条完整的请求时，你必须返回一个你的请求类型的实例.
        /// 当你在接收缓冲区中没有找到一个完整的请求时, 你需要返回 NULL.
        /// 当你在接收缓冲区中找到一条完整的请求, 但接收到的数据并不仅仅包含一个请求时，设置剩余数据的长度到输出变量 "rest". SuperSocket 将会检查这个输出参数 "rest", 如果它大于 0, 此 Filter 方法 将会被再次执行, 参数 "offset" 和 "length" 会被调整为合适的值.
        public BinaryRequestInfo Filter(byte[] readBuffer, int offset, int length, bool toBeCopied, out int rest)
        {
            rest = 0;
            if (length < 10)//没有数据
            {
                return null;
            }
            byte[] data = new byte[length];
            Buffer.BlockCopy(readBuffer, offset, data, 0, length);
            if (residue.Length > 0 && needContant)
            {
                data = residue.Concat(data).ToArray();
                length += residue.Length;
            }
            int index = 0;

            while (index < length)
            {
                if (data[index] == 0x7e)
                {
                    HasOrderHead = true;
                    int temp = index + 1;
                    while (temp < length)
                    {
                        if (data[temp] == 0x7e)
                        {
                            byte[] result = new byte[temp + 1];
                            Buffer.BlockCopy(data, index, result, 0, temp + 1);
                            HasOrderHead = false;
                            needContant = false;
                            rest = length - temp - 1;
                            return new BinaryRequestInfo("AcSafeFileCommand", result);
                        }
                        temp++;
                    }
                    HasOrderHead = false;
                    rest = 0;
                    residue = new byte[length - index];
                    needContant = true;
                    Buffer.BlockCopy(data, index + 1, residue, 0, length - index);
                    return null;
                }
                else if (data[index] == 0x30 && data[index + 1] == 0x31 && data[index + 2] == 0x63 && data[index + 3] == 0x64 && !HasOrderHead)
                {
                    int dataLength = (int)data.ToUInt32(index + 58);
                    if (data.Length - index - 62 < dataLength)
                    {
                        rest = 0;
                        residue = new byte[length - index];
                        needContant = true;
                        Buffer.BlockCopy(data, index, residue, 0, length - index);
                        return null;
                    }
                    byte[] result = new byte[dataLength + 62];
                    Buffer.BlockCopy(data, index, result, 0, dataLength + 62);
                    if (data.Length - index - 62 - dataLength > 0)
                    {
                        rest = length - index - 62 - dataLength;
                        residue = new byte[rest];
                        Buffer.BlockCopy(data, index + 62 + dataLength, residue, 0, rest);
                        needContant = false;
                    }
                    return new BinaryRequestInfo("AcSafeFileCommand", result);
                }
                else
                {
                    index++;
                }
            }
            return null;
        }

        public void Reset()
        {
        }
    }
}