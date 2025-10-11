targetScope = 'subscription'

var resourceGroupName = 'FullStackApp-Angular-Dotnet-Azure'
var resourceGroupLocation = 'brazilsouth'

resource resourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: resourceGroupName
  location: resourceGroupLocation
}
