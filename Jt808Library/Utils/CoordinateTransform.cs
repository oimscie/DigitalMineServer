using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JtLibrary.Utils
{
    public class CTransform
    {
        /// <summary>
        /// WGS-84转2000坐标
        /// </summary>
        /// <param name="B">纬度</param>
        /// <param name="L">经度</param>
        /// <param name="degree">3度带、6度带</param>
        /// <returns></returns>
        public static List<double> WGS84ToXY(double B, double L, double degree)
        {
            List<double> xy = new List<double>
            {
                0,
                0,
                0
            };
            double a = 6378137; //椭球长半轴
            double b = 6356752.3142451795; //椭球短半轴
            double e = 0.081819190842621; //第一偏心率
            double eC = 0.0820944379496957; //第二偏心率
            double L0 = 0; //中央子午线经度
            int n = 0; //带号
            if (degree == 6)
            {
                //6度
                n = Convert.ToInt32(Math.Round((L + degree / 2) / degree));
                L0 = degree * n - degree / 2;
                xy[2] = L0;
            }
            else
            {
                //3度
                n = Convert.ToInt32(Math.Round(L / degree));
                L0 = degree * n;
                xy[2] = L0;
            }
            //开始计算
            double radB = B * Math.PI / 180; //纬度(弧度)
            double radL = L * Math.PI / 180; //经度(弧度)
            double deltaL = (L - L0) * Math.PI / 180; //经度差(弧度)
            double N = a * a / b / Math.Sqrt(1 + eC * eC * Math.Cos(radB) * Math.Cos(radB));
            double C1 = 1.0 +
                3.0 / 4 * e * e +
                45.0 / 64 * Math.Pow(e, 4) +
                175.0 / 256 * Math.Pow(e, 6) +
                11025.0 / 16384 * Math.Pow(e, 8);
            double C2 = 3.0 / 4 * e * e +
                15.0 / 16 * Math.Pow(e, 4) +
                525.0 / 512 * Math.Pow(e, 6) +
                2205.0 / 2048 * Math.Pow(e, 8);
            double C3 = 15.0 / 64 * Math.Pow(e, 4) +
                105.0 / 256 * Math.Pow(e, 6) +
                2205.0 / 4096 * Math.Pow(e, 8);
            double C4 = 35.0 / 512 * Math.Pow(e, 6) + 315.0 / 2048 * Math.Pow(e, 8);
            double C5 = 315.0 / 131072 * Math.Pow(e, 8);
            double t = Math.Tan(radB);
            double eta = eC * Math.Cos(radB);
            double X = a *
                (1 - e * e) *
                (C1 * radB -
                    C2 * Math.Sin(2 * radB) / 2 +
                    C3 * Math.Sin(4 * radB) / 4 -
                    C4 * Math.Sin(6 * radB) / 6 +
                    C5 * Math.Sin(8 * radB));
            xy[0] = X +
                    N *
                        Math.Sin(radB) *
                        Math.Cos(radB) *
                        Math.Pow(deltaL, 2) *
                        (1 +
                            Math.Pow(deltaL * Math.Cos(radB), 2) *
                                (5 - t * t + 9 * eta * eta + 4 * Math.Pow(eta, 4)) /
                                12 +
                             Math.Pow(deltaL * Math.Cos(radB), 4) *
                                (61 - 58 * t * t + Math.Pow(t, 4)) /
                                360) /
                        2;
            xy[1] = N *
                    deltaL *
                    Math.Cos(radB) *
                    (1 +
                         Math.Pow(deltaL * Math.Cos(radB), 2) * (1 - t * t + eta * eta) / 6 +
                         Math.Pow(deltaL * Math.Cos(radB), 4) *
                            (5 -
                                18 * t * t +
                                 Math.Pow(t, 4) -
                                14 * eta * eta -
                                58 * eta * eta * t * t) /
                            120) +
                500000 + n * 1000000;
            return xy;
        }

        /// <summary>
        /// 将大地2000转为WGS84
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="y">通过经纬度来获取中央带所在带的角度</param>
        /// <param name="L0"></param>
        /// <returns>B纬度 , L经度</returns>
        public static List<double> XYToWGS84(double x, double y, double L0)
        {

            List<double> xy = new List<double>
            {
                0,
                0
            };
            //中央子午线经度
            //WGS-84   椭球体参数
            double a = 6378137.0; //major semi axis
            double efang = 0.0066943799901413; //square of e
            double e2fang = 0.0067394967422764; //suqre of e2
                                                //  y = y - 500000;
                                                //主曲率计算
            double m0, m2, m4, m6, m8;
            m0 = a * (1 - efang);
            m2 = 3.0 / 2.0 * efang * m0;
            m4 = efang * m2 * 5.0 / 4.0;
            m6 = efang * m4 * 7.0 / 6.0;
            m8 = efang * m6 * 9.0 / 8.0;
            //子午线曲率计算
            double a0, a2, a4, a6, a8;
            a0 = m0 + m2 / 2.0 + m4 * 3.0 / 8.0 + m6 * 5.0 / 16.0 + m8 * 35.0 / 128.0;
            a2 = m2 / 2.0 + m4 / 2.0 + m6 * 15.0 / 32.0 + m8 * 7.0 / 16.0;
            a4 = m4 / 8.0 + m6 * 3.0 / 16.0 + m8 * 7.0 / 32.0;
            a6 = m6 / 32.0 + m8 / 16.0;
            a8 = m8 / 128.0;
            double X = x;
            double FBf = 0;
            double Bf0 = X / a0, Bf1 = 0;
            //计算Bf的值，直到满足条件
            while ((Bf0 - Bf1) >= 0.0001)
            {
                Bf1 = Bf0;
                FBf = -a2 * Math.Sin(2 * Bf0) / 2 + a4 * Math.Sin(4 * Bf0) / 4 - a6 * Math.Sin(6 * Bf0) / 6 + a8 * Math.Sin(8 * Bf0) / 8;
                Bf0 = (X - FBf) / a0;
            }
            double Bf = Bf0;
            //计算公式中参数
            double Wf = Math.Sqrt(1 - efang * Math.Sin(Bf) * Math.Sin(Bf));
            double Nf = a / Wf;
            double Mf = a * (1 - efang) / Math.Pow(Wf, 3);
            double nffang = e2fang * Math.Cos(Bf) * Math.Cos(Bf);
            double tf = Math.Tan(Bf);
            double B = Bf - tf * y * y / (2 * Mf * Nf) + tf * (5 + 3 * tf * tf + nffang - 9 * nffang * tf * tf) * Math.Pow(y, 4) / (24 * Mf * Math.Pow(Nf, 3)) - tf * (61 + 90 * tf * tf + 45 * Math.Pow(tf, 4)) * Math.Pow(y, 6) / (720 * Mf * Math.Pow(Nf, 5));
            double l = y / (Nf * Math.Cos(Bf)) - (1 + 2 * tf * tf + nffang) * Math.Pow(y, 3) / (6 * Math.Pow(Nf, 3) * Math.Cos(Bf)) + (5 + 28 * tf * tf + 24 * Math.Pow(tf, 4)) * Math.Pow(y, 5) / (120 * Math.Pow(Nf, 5) * Math.Cos(Bf));
            double L = l + L0;
            //转化成为十进制经纬度格式
            var array_B = rad2dms(B);
            var array_L = rad2dms(L);
            double Bdec = dms2dec(array_B);
            double Ldec = dms2dec(array_L);
            xy[0] = Bdec;
            xy[1] = Ldec;
            return xy;
        }

        private static readonly double p = 180.0 / Math.PI * 3600;
        /// <summary>
        /// 通过经纬度来获取中央带所在带的角度
        /// </summary>
        /// <param name="B">纬度</param>
        /// <param name="L">经度</param>
        /// <param name="N">N 带[3,6带度]</param>
        /// <returns></returns>
        public static double gaussLongToDegreen(double B, double L, int N)
        {
            //计算该地区的中央子午线经度
            double L00 = Convert.ToDouble(Math.Round(L / 3) * 3);
            double degreen = L00 / 180 * 3.1415926;
            return degreen;
        }
        //将弧度转化为度分
        private static dynamic rad2dms(double rad)
        {
            List<int> a = new List<int>
            {
                0,
                0,
                0
            };
            double dms = rad * p;
            a[0] = (int)Math.Floor(dms / 3600.0);
            a[1] = (int)Math.Floor((dms - a[0] * 3600) / 60.0);
            a[2] = (Convert.ToInt32(dms - a[0] * 3600) - a[1] * 60);
            return a;
        }
        //将度分秒转化为十进制坐标
        private static double dms2dec(dynamic dms)
        {
            double dec = 0.0;
            dec = dms[0] + dms[1] / 60.0 + dms[2] / 3600.0;
            return dec;
        }
    }
}
