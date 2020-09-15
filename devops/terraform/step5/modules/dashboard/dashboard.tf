resource "azurerm_dashboard" "theboard" {
  name                = "thedashboard"
  resource_group_name = var.rgname
  location            = var.location
  tags = {
    source = "terraform"
  }
  dashboard_properties = templatefile("/modules/dashboard/dashboard.template.json",
  {
      vmid = var.vmid,
      vmname = var.vmname
  })
}

