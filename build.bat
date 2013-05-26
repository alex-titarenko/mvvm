@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild TAlex.WPF.Mvvm.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

mkdir Build
mkdir Build\net45
.nuget\nuget.exe pack "TAlex.WPF.Mvvm\TAlex.WPF.Mvvm.csproj" -symbols -o Build\net45 -p SolutionDir=%cd% -Prop Configuration=%config%
copy TAlex.WPF.Mvvm\bin\%config%\*.dll Build\net45
copy TAlex.WPF.Mvvm\bin\%config%\*.pdb Build\net45