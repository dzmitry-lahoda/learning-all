resource "azurerm_dashboard" "tfboard" {
  name                = "tfboard"
  resource_group_name = var.rgname
  location            = var.location
  tags = {
    source = "terraform",
    test   = "good"
  }
  dashboard_properties = templatefile("/modules/dashboard/dashboard.template.json", {
      dashboarded =  {
        one = { number = 0, vmid = var.vmid, vmname = var.vmname },
        two = { number = 1, vmid = var.vmid, vmname = var.vmname }
      }
  })
}

