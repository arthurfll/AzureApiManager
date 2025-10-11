targetScope = 'subscription'

var resourceGroupName = 'FullStackApp-Angular-Dotnet-Azure'
var resourceGroupLocation = 'brazilsouth'
var webappName = uniqueString(resourceGroup.id)

resource resourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: resourceGroupName
  location: resourceGroupLocation
}

resource appServicePlan 'Microsoft.Web/serverfarms@2020-12-01' = {
  name: 'backend-service-plan'
  location: resourceGroup.location
  scope:resourceGroup.id
  sku: {
    name: 'F1'
    capacity: 1
  }
}

resource backendAppService 'Microsoft.Web/sites@2021-01-15' = {
  name: webappName
  location: resourceGroup.location
  scope:resourceGroup.id
  properties: {
    serverFarmId: appServicePlan.id
  }
}



