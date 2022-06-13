using DigitalMineServer.Mysql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DigitalMineServer.implement
{
    public class FormUtil
    {
        delegate void lableShowDelegate(Label lable, string strshow);

        delegate void TextBoxShowDelegate(TextBox TextBox, string strshow);

        delegate void UpdataSourceDelegate(DataGridView view, List<vehicleStateEntity> list);
        /// <summary>
        /// 修改界面提示文字
        /// </summary>
        /// <param name="lable"></param>
        /// <param name="strshow"></param>
        public static void ModifyLable(Label lable, string strshow)
        {
            if (lable.InvokeRequired)
            {
                lable.Invoke(new lableShowDelegate(ModifyLable), new object[] { lable, strshow });
            }
            else
            {
                lable.Text = strshow;
            }
        }
        /// <summary>
        /// 添加文本
        /// </summary>
        /// <param name="TextBox"></param>
        /// <param name="strshow"></param>
        public static void AppendText(TextBox TextBox, string strshow)
        {
            if (TextBox.InvokeRequired)
            {
                TextBox.Invoke(new TextBoxShowDelegate(AppendText), new object[] { TextBox, strshow });
            }
            else
            {
                TextBox.AppendText(strshow + "\r\n");
            }
        }

        public static void UpdataSource(DataGridView view,List<vehicleStateEntity> list) {
            if (view.InvokeRequired)
            {
                view.Invoke(new UpdataSourceDelegate(UpdataSource), new object[] { view, list });
            }
            else
            {
                view.DataSource = list;
            }
        }
    }
}
