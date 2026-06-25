rm -rf ./pub-linux
rm -rf ./pub-windows
rm -rf ./pub-mac
rm -rf ./pub-html
dotnet publish ./src/MTBP.Spectre/MTBP.Spectre.vbproj -o ./pub-linux -c Release --sc -p:PublishSingleFile=true -r linux-x64
dotnet publish ./src/MTBP.Spectre/MTBP.Spectre.vbproj -o ./pub-windows -c Release --sc -p:PublishSingleFile=true -r win-x64
dotnet publish ./src/MTBP.Spectre/MTBP.Spectre.vbproj -o ./pub-mac -c Release --sc -p:PublishSingleFile=true -r osx-x64
dotnet publish ./src/MTBP.Blazor/MTBP.Blazor.csproj -o ./pub-html -c Release 
rm -f ./pub-linux/*.pdb
rm -f ./pub-windows/*.pdb
rm -f ./pub-mac/*.pdb
rm -f ./pub-html/*.pdb
butler push pub-windows thegrumpygamedev/toxic-city-of-splorr:windows
butler push pub-linux thegrumpygamedev/toxic-city-of-splorr:linux
butler push pub-mac thegrumpygamedev/toxic-city-of-splorr:mac
butler push pub-html/wwwroot thegrumpygamedev/toxic-city-of-splorr:html
