
"C:\Program Files (x86)\Microsoft Visual Studio 14.0\Common7\IDE\tf.exe" get *.* /recursive /overwrite | find "All files are up to date" && exit 0

"C:\SonarQube\If.CsHtmlParser.exe"

for /f %%a in ('powershell -Command "Get-Date -format yy.MM.dd"') do set datetime=%%a

"C:\SonarQube\sonar-scanner-msbuild-4.0.2.892\SonarQube.Scanner.MSBuild.exe" begin ^
	/k:"SafetyFund" /n:"Safety fund" /v:"%datetime%" ^
	/d:sonar.cs.xunit.reportsPaths="%CD%\XUnitResults.xml" ^
	/d:sonar.cs.opencover.reportsPaths="%CD%\opencover.xml"

dotnet build --configuration Debug


set test1="SafetyFund.Data.Tests\bin\Debug\netcoreapp2.0\SafetyFund.Data.Tests"
set test2="SafetyFund.Web.Tests\bin\Debug\netcoreapp2.0\SafetyFund.Web.Tests"
set test3="SafetyFund.Business.Tests\bin\Debug\netcoreapp2.0\SafetyFund.Business.Tests"

"C:\SonarQube\Tools\OpenCover.4.6.519\tools\OpenCover.Console.exe" ^
	-output:"%CD%\opencover.xml" ^
	-register:user ^
	-target:"C:\Program Files\dotnet\dotnet.exe" ^
	-targetargs:"C:\SonarQube\Tools\xunit.runner.console.2.3.1\tools\netcoreapp2.0\xunit.console.dll %test1%.dll %test1%.deps.json %test2%.dll %test2%.deps.json %test3%.dll %test3%.deps.json -xml XUnitResults.xml" ^
	-filter:"+[SafetyFund.*]*" ^
	-coverbytest:*.Tests.dll ^
	-searchdirs:"SafetyFund.Data.Tests\bin\Debug\netcoreapp2.0";"SafetyFund.Web.Tests\bin\Debug\netcoreapp2.0";"SafetyFund.Business.Tests\bin\Debug\netcoreapp2.0" ^
	-oldStyle


"C:\SonarQube\sonar-scanner-msbuild-4.0.2.892\SonarQube.Scanner.MSBuild.exe" end