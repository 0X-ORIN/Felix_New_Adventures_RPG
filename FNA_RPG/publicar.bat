@echo off
cd /d "%~dp0"
set TEMP_OUT=%~dp0bin\publish_out

dotnet publish -c Release -r win-x64 --self-contained true -o "%TEMP_OUT%"
if errorlevel 1 (
    echo.
    echo ERRO na publicacao.
    pause
    exit /b 1
)

if exist "%~dp0..\Content" rmdir /s /q "%~dp0..\Content"
xcopy "%TEMP_OUT%\*" "%~dp0.." /E /Y /I
rmdir /s /q "%TEMP_OUT%"

echo.
echo Pronto! O jogo foi atualizado na pasta de cima.
pause