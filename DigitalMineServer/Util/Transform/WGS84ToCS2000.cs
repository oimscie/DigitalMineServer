using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalMineServer.Util.Transform
{
    public class WGS84ToCS2000
    {
        /// <summary>
        /// WGS-84转2000坐标
        /// </summary>
        /// <param name="B">纬度</param>
        /// <param name="L">经度</param>
        /// <param name="degree">3度带、6度带</param>
        /// <returns></returns>
        public static List<double> GpsToXY(double B, double L, double degree)
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
    }
}
