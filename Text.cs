using System.Windows.Forms;

namespace THelper
{
    public class Text
    {
        /// <summary>
        ///     Заменяет текст(букву) контрола на другой
        /// </summary>
        /// <param name="control">Кнопка, лейбл, текстбокс - что угодно, но через object!</param>
        /// <param name="OldValue">Текст ДЛЯ замены</param>
        /// <param name="NewValue">Текст, КОТОРЫМ это будет заменяться</param>
        public static void ReplaceText(object control, string OldValue, string NewValue)
        {
            var RTcontrol = control as Control;
            RTcontrol.Text = RTcontrol.Text.Replace(OldValue, NewValue);
        }

        /// <summary>
        ///     Заменяет текст(букву) контрола на другой
        /// </summary>
        /// <param name="control">Кнопка, лейбл, текстбокс - что угодно, но через Control!</param>
        /// <param name="OldValue">Текст ДЛЯ замены</param>
        /// <param name="NewValue">Текст, КОТОРЫМ это будет заменяться</param>
        public static void ReplaceText(Control control, string OldValue, string NewValue)
        {
            control.Text = control.Text.Replace(OldValue, NewValue);
        }

        /// <summary>
        ///     Заменяет текст(букву) в строке на другую
        /// </summary>
        /// <param name="text">Сам текст</param>
        /// <param name="OldValue">Текст ДЛЯ замены</param>
        /// <param name="NewValue">Текст, КОТОРЫМ это будет заменяться</param>
        /// <returns>Замененный текст</returns>
        public static string ReplaceText(string text, string OldValue, string NewValue)
        {
            return text.Replace(OldValue, NewValue);
        }
    }
}