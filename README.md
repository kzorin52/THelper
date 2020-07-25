# THelper
Помошник-мултитул для программиста!
## Как использовать?
### Класс Crypto

#### Метод Encode
Использование:
``` C#
string text = THelper.Crypto.Encode("Какой-то текст","Абсолютно любой пароль");
```
  Шифрует строку с помощью пароля и некоторых алгоритмов шифрования. Возвращает строку в кодировке Base64.

#### Метод Decode
Использование:
``` C#
string text = THelper.Crypto.Decode("Зашифрованный текст","Пароль");
```
  Расшифровывает строку в кодировке Base64, возвращает string

#### Метод EncodeFile
Использование:
``` C#
THelper.Crypto.EncodeFile("Путь-к-файлу","Пароль");
```
  Шифрует файл с помощью пароля и некоторых алгоритмов шифрования.

#### Метод DecodeFile
Использование:
``` C#
THelper.Crypto.DecodeFile("Путь-к-зашифрованному-файлу","Пароль");
```
  Дешифрует файл с помощью пароля, заранее зашифрованный способом THelper.Crypto.EncryptFile.

### Класс Utilites

#### Метод Randomer
Использование:
``` C#
int rnd = THelper.Utilites.Randomer(0, 100); //0 - Нижняя граница рандома, 100 - Верхняя граница рандома
```
  Простой рандом, в замену new Random().Next(0,100);

#### Метод InNewThread
Использование:
``` C#
THelper.Utilites.InNewThread(()=>
{
  ...   //Какой-то код, который будет выполняться в новом потоке
}); 
```
  Запускает код в новом потоке.

#### Метод Repeater
Использование:
``` C#
THelper.Utilites.Repeater(()=>
{
  ...   //Какой-то код, который будет выполняться
}, 5); //5 - количество повторений
```
  Простой цикл.

#### Метод KillProcess
Использование:
``` C#
THelper.Utilites.KillProcess("explorer.exe"); //5 - количество повторений
```
  Убивает процесс по имени или кусочку имени(!)

#### Метод AlertShow
Использование:
``` C#
THelper.Utilites.AlertShow("Текст для уведомления", AlertType.<Есть Success, Error, Info, Warning>, ["Дополнительная инфа на одну строку по клику"]);
```
  Вызывает popup-сообщение

### Класс WinAPI

#### Метод SetForegroundWindow
Использование:
``` C#
THelper.WinAPI.SetForegroundWindow("explorer.exe"); // - По названию окна
// ИЛИ
THelper.WinAPI.SetForegroundWindow(123); // - hWnd
```
  "Поднимает" окно на передний план (делает Focus).

#### Метод IsWindowOpen
Использование:
``` C#
bool status = THelper.WinAPI.IsWindowOpen("Калькулятор");
```
  Проверка на существование открытого окна

#### Метод MouseClick
Использование:
``` C#
THelper.WinAPI.MouseClick();
```
  Выполняет клик мышкой в текущих координатах

### Класс URL

#### Метод GetFileName
Использование:
``` C#
string filename = THelper.WinAPI.GetFileName("https://host.ru/files/123.txt");
// ИЛИ
Uri uri = new Uri("https://host.ru/files/123.txt");
string filename = THelper.WinAPI.GetFileName(uri);
```
  Получает имя файла из URL
  
## Пока на этом всё, но будут частые обновления

### Кто хочет помочь автору материально
...И тем самым ускорить процесс :)
Вам [сюда](https://qiwi.com/p/79618800938)

### Кто хочет помочь автору Идеями
Писать [сюда](http://vk.com/temnij52)

### Также есть PRO версия за 100 рублей, в ней +10 методов
