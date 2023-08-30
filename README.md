# rundll34
Утилита командной строки для вызова функций из DLL

## Использование
`rundll34 <DLL> <имя функции> <тип возврата> [аргументы в формате <тип>:<значение>]`
- `DLL`: Путь к DLL-файлу (если DLL-файл находится в любой из папок, указанных в `%PATH%`, допустимо указать только имя).
- `имя функции`: Полное имя функции (включая окончание A или W для функций WinAPI) или ее порядковый номер (с префиксом `#`).
- `тип возврата`: Тип возврата функции. Допустимы любые типы .NET или их сокращения, представленные ниже.
- `аргументы`: Список аргументов функции в формате `<тип>:<значение>`. Аргументы разделяются пробелом. Допустимы любые типы .NET или их сокращения, представленные ниже.

Если функция найдена и выполнена, код выхода устанавливается в 0, а в StdOut выводится результат выполнения функции.  
В случае ошибки код выхода устанавливается в 1, а в StdOut выводится сообщение об ошибке.

Допустимые сокращения типов:
- `int`: `System.Int32`
- `uint`: `System.UInt32`
- `long`: `System.Int64`
- `ulong`: `System.UInt64`
- `double`: `System.Double`
- `byte`: `System.Byte`
- `intptr`: `System.IntPtr`
- `char`: `System.Char`
- `string`: `System.String`

Также вместо типа можно указать:
- `null`: нулевой указатель
- `alloc:<тип>`: указатель на фрагмент памяти размером с указанный тип

В качестве типа возврата также допустимо значение `void`.

## Примеры
`rundll34 user32.dll MessageBoxA int null string:"Hello World" string:"Dialog" int:64`  
Выводит окно сообщения с текстом "Hello World", заголовком "Dialog", кнопкой "ОК" и значком информации.  
Подробнее: [MessageBoxA](https://learn.microsoft.com/ru-ru/windows/win32/api/winuser/nf-winuser-messageboxa)

`rundll34 shell32.dll ShellAboutA void null string:"Test" string:"Why not?"`  
Создает окно "О программе" с заголовком "Test" и текстом "Why not?".  
Подробнее: [ShellAboutA](https://learn.microsoft.com/ru-ru/windows/win32/api/shellapi/nf-shellapi-shellabouta)
