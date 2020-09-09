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
  name     = "rglahodatf"
  location = "westeurope"
  tags = {
    tag1 = "my-tag1"
    tag2 = "tutorial"
  }
}
