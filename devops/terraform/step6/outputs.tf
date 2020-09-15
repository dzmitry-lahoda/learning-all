

output "publicip" {
    value = azurerm_public_ip.publicip.ip_address
}

output "resourcegroup" {
    value = azurerm_resource_group.rg.id
}

output "username" {
    value = [for p in azurerm_virtual_machine.vm.os_profile: p.admin_username]
}


output "password" {
    value = [for p in azurerm_virtual_machine.vm.os_profile: p.admin_password]
}