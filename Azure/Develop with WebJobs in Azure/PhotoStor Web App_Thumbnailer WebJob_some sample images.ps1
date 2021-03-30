Add-Type -AssemblyName System.IO.Compression.FileSystem

$url = "https://github.com/linuxacademy/content-az-300-lab-repos/blob/master/create-an-azure-web-app/LA_PhotoStor.zip?raw=true"
$url2 = "https://github.com/linuxacademy/content-az-300-lab-repos/blob/master/create-an-azure-web-app/images.zip?raw=true"
$url3 = "https://github.com/linuxacademy/content-az-300-lab-repos/blob/master/develop-with-webjobs-in-azure/Thumbnailer.zip?raw=true"

$zipfile = "C:\Users\azureuser\Desktop\LA_PhotoStor.zip"
$zipfile2 = "C:\Users\azureuser\Desktop\images.zip"
$zipfile3 = "C:\Users\azureuser\Desktop\Thumbnailer.zip"

$folder = "C:\Users\azureuser\Documents\VS"
$folder2 = "C:\Users\azureuser\Pictures"

[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
Invoke-WebRequest -UseBasicParsing -OutFile $zipfile $url

[System.IO.Compression.ZipFile]::ExtractToDirectory($zipfile, $folder)

[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
Invoke-WebRequest -UseBasicParsing -OutFile $zipfile2 $url2

[System.IO.Compression.ZipFile]::ExtractToDirectory($zipfile2, $folder2)

[Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12
Invoke-WebRequest -UseBasicParsing -OutFile $zipfile3 $url3

[System.IO.Compression.ZipFile]::ExtractToDirectory($zipfile3, $folder)

Remove-Item -Path $zipfile
Remove-Item -Path $zipfile2
Remove-Item -Path $zipfile3
#
