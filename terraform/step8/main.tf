
locals {
    locations = { germanywestcentral = "germanywestcentral", canadaeast = "canadaeast"}
}

terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"      
    }
  }
}
provider "azurerm" {
  features {}
}
resource "azurerm_resource_group" "default" {
  name     = "rglahodatf-canadaeast"
  location = "canadaeast"
}



resource "azurerm_cosmosdb_account" "default" {
  name                      =  "cosmos-lahodatf"
  location                  =  azurerm_resource_group.default.location
  resource_group_name       =  azurerm_resource_group.default.name
  offer_type                = "Standard"
  kind                      = "GlobalDocumentDB"
  enable_automatic_failover = true
    
  capabilities {
    name = "EnableTable"
  }

  enable_multiple_write_locations = true
  consistency_policy {
    consistency_level       = "Session"
  }

  geo_location {
    location          = azurerm_resource_group.default.location
    failover_priority = 0
  }
}

resource "azurerm_cosmosdb_table" "default" {
  name                = "cosmos-db-table"
  resource_group_name = azurerm_cosmosdb_account.default.resource_group_name
  account_name        = azurerm_cosmosdb_account.default.name
  throughput          = 400
}

output "cosmosdb_connection_string0" {
    value = azurerm_cosmosdb_account.default.connection_strings[0]
}

output "cosmosdb_connection_string1" {
    value = azurerm_cosmosdb_account.default.connection_strings[1]
}

output "cosmosdb_connection_string2" {
    value = azurerm_cosmosdb_account.default.connection_strings[2]
}

output "cosmosdb_connection_string3" {
    value = azurerm_cosmosdb_account.default.connection_strings[3]
}

output "cosmosdb_connection_string" {
    value = azurerm_cosmosdb_account.default.endpoint
}


output "cosmosdb_connection_primary_key" {
    value = azurerm_cosmosdb_account.default.primary_key
}




output "cosmosdb_ep1" {
    value = azurerm_cosmosdb_account.default.write_endpoints
}



output "cosmosdb_ep2" {
    value = azurerm_cosmosdb_account.default.endpoint
}