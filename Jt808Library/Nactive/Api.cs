/*-------------------------------------------------------------
 *   auth: bouyei
 *   date: 2017/6/21 12:59:31
 *contact: 453840293@qq.com
 *profile: www.openthinking.cn
 *    Ltd: 
 *   guid: 615f6b93-af1a-4287-813c-1a12196d58f8
---------------------------------------------------------------*/
using System;
using System.Runtime.InteropServices;

namespace JtLibrary.Nactive
{
    public class Api
    { 
        [DllImport("ntdll.dll", CallingConvention = CallingConvention.Cdecl)]
        public static unsafe extern byte* memcpy(
            byte* dst,
            byte* src,
            int count);

        [DllImport("msvcrt.dll")]
        public static extern unsafe int memcmp(void* src, void* dst, int count);
    }
}
