for /f %%i in ('dir /a:d /s /b bin') do rd /s /q %%i
for /f %%i in ('dir /a:d /s /b obj') do rd /s /q %%i