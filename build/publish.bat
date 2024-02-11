del ..\publish\beef /F /Q
del ..\publish\beef.types /F /Q

dotnet pack ..\Beef.Types\Beef.Types.csproj -c Release --output "../publish/beef.types"
dotnet pack ..\Beef\Beef.csproj -c Release --output "../publish/beef"

nuget init ..\publish C:\LocalNuget\
