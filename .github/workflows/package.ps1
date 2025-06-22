mkdir .dist | Out-Null

Copy-Item ".\${env:_RELEASE_NAME}\bin\${env:_RELEASE_CONFIGURATION}\*.nupkg" -Destination ".\.dist\"
