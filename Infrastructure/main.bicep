var location = resourceGroup().location



resource appServicePlan 'Microsoft.Web/serverfarms@2020-12-01' = {
  name: 'app-plan-1'
  location: location
  sku: {
    name: 'F1'
    capacity: 1
  }
}


resource webApplication 'Microsoft.Web/sites@2021-01-15' = {
  name: 'WebApp-1'
  location: location
  tags: {
    'hidden-related:${resourceGroup().id}/providers/Microsoft.Web/serverfarms/appServicePlan': 'Resource'
  }
  properties: {
    serverFarmId: appServicePlan.id
  }
}

resource apiManagementInstance 'Microsoft.ApiManagement/service@2020-12-01' = {
  name: 'apim-1'
  location: location
  sku:{
    capacity: 1
    name: 'Developer'
  }
  properties:{
    virtualNetworkType: 'None'
    publisherEmail: 'arthurfontenelle2017@gmail.com'
    publisherName: 'Arthur Fontenelle'
  }
}

