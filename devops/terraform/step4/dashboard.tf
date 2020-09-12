resource "azurerm_dashboard" "theboard" {
  name                = "thedashboard"
  resource_group_name = azurerm_resource_group.rg.name
  location            = azurerm_resource_group.rg.location
  tags = {
    source = "terraform"
  }
  dashboard_properties = templatefile("dashboard.tpl",
  {
      vmid = azurerm_virtual_machine.vm.id,
      vmname = azurerm_virtual_machine.vm.name
  })
}

