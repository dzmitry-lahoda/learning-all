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
resource "azurerm_resource_group" "rg" {
  for_each = local.locations
  name     = "rglahodatf-containers"
  location = each.value
  tags = {
    tag1 = "my-tag1"
    tag2 = "tutorial"
  }
}

