resource "azurerm_dashboard" "theboard" {
  name                = "thedashboard"
  resource_group_name = var.rgname
  location            = var.location
  tags = {
    source = "terraform"
  }
  dashboard_properties = templatefile("dashboard.tpl",
  {
      vmid = var.vmid,
      vmname = var.vmname
  })
}

