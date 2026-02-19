@echo off
chcp 65001 >nul
echo Проверка .NET SDK...
dotnet --list-sdks
if errorlevel 1 ( echo Установите .NET SDK: https://dotnet.microsoft.com/download & pause & exit /b 1 )

echo.
echo Очистка...
dotnet clean 14.csproj -v minimal

echo.
echo Восстановление пакетов...
dotnet restore 14.csproj

echo.
echo Сборка для Windows...
dotnet build 14.csproj -f net10.0-windows10.0.19041.0 -c Debug -v minimal

if errorlevel 1 (
    echo.
    echo Сборка завершилась с ошибками. Попробуйте в Visual Studio: Сборка -^> Перестроить решение
    pause
    exit /b 1
)

echo.
echo Сборка успешна. Запуск приложения...
start "" "bin\Debug\net10.0-windows10.0.19041.0\win10-x64\14.exe"
echo Готово.
pause
