


variable "location" {}

variable "admin_password" {
    type = string
}

variable "admin_username" {
    default = "dzmitry"
}
variable "prefix" {
    default = "lahoda"
}

variable "tags" {
    type = map
    default = {
        env = "test"
        fun = "half"
    }
}

variable "skus" {
    default = {
        westeurope = "2019-Datacenter"
    }
}



