using System;
using System.Diagnostics;
using System.Threading;
using static Alert.Alert;

namespace THelper
{
    public class Utilites
    {
        /// <summary>
        ///     Простой рандом
        /// </summary>
        /// <param name="MinValue">Нижняя граница рандома</param>
        /// <param name="MaxValue">Верхняя граница рандома</param>
        /// <returns>Рандомное число</returns>
        public static int Randomer(int MinValue, int MaxValue)
        {
            return new Random().Next(MinValue, MaxValue);
        }

        /// <summary>
        ///     Вызывает popup-сообщение
        /// </summary>
        /// <param name="Message">Текст сообщения</param>
        /// <param name="Type">Тип сообщения</param>
        public static void AlertShow(string Message, AlertType Type)
        {
            new Alert.Alert().ShowAlert(Message, Type);
        }

        /// <summary>
        ///     Вызывает popup-сообщение
        /// </summary>
        /// <param name="Message">Текст сообщения</param>
        /// <param name="Type">Тип сообщения</param>
        /// <param name="MoreText">Дополнительная информация по клику</param>
        public static void AlertShow(string Message, AlertType Type, string MoreText)
        {
            new Alert.Alert().ShowAlert(Message, Type, MoreText);
        }

        /// <summary>
        ///     Запускает код в новом потоке. Вместо Action можно использовать лямбда-выражение, например, так - InNewThread(()=>{
        ///     тут код});
        /// </summary>
        /// <param name="Code">Код</param>
        public static void InNewThread(Action Code)
        {
            new Thread(() => Code()).Start();
        }

        /// <summary>
        ///     Простой цикл. Вместо Action можно использовать лямбда-выражение, например, так - InNewThread(()=>{ тут код});
        /// </summary>
        /// <param name="Code">Код</param>
        /// <param name="count">Кол-во повторов</param>
        public static void Repeater(Action Code, int count)
        {
            for (var i = 0; i < count; i++) Code();
        }

        /// <summary>
        ///     Убивает процесс по имени
        /// </summary>
        /// <param name="name">Имя процесса для Убиения</param>
        public static void KillProcess(string name)
        {
            var etc = Process.GetProcesses();
            foreach (var anti in etc)
                if (anti.ProcessName.ToLower().Contains(name.ToLower()))
                    anti.Kill();
        }
    }
}