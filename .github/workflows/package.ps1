mkdir .dist | Out-Null

Copy-Item ".\${env:_RELEASE_NAME}\bin\${env:_RELEASE_CONFIGURATION}\*.nupkg" -Destination ".\.dist\${env:_RELEASE_NAME}-${env:_RELEASE_VERSION}_${env:_RELEASE_CONFIGURATION}.nupkg"
