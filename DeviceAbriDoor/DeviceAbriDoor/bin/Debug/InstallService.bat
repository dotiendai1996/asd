@echo off
color 0A
title Conditional ShutDown
 
:start
set /p choice="Are your sure want to deploy Abrivision Device Service as a windows service? (Y/N): "
if "%choice%"=="Y" goto install
if "%choice%"=="y" goto install
if "%choice%"=="N" exit
if "%choice%"=="n" exit

echo Invalid choice: %choice%
echo.
pause
cls
goto start
 
:install
cls
"%~dp0InstallUtil.exe"  "%~dp0DeviceAbriDoor.exe"
echo.
pause
exit