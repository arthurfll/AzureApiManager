targetScope = 'resourceGroup'

var webappName = uniqueString(resourceGroup().id)



resource appServicePlan 'Microsoft.Web/serverfarms@2020-12-01' = {
  name: 'backend-service-plan'
  location: resourceGroup().location
  sku: {
    name: 'F1'
    capacity: 1
  }
}

resource backendAppService 'Microsoft.Web/sites@2021-01-15' = {
  name: webappName
  location: resourceGroup().location
  properties: {
    serverFarmId: appServicePlan.id
  }
}
