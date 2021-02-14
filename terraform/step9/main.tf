
locals {
   sql_allowed_ips = {
    bp_office  = { start = "62.77.220.46", end = "62.77.220.46" }
    gzp_office = { start = "84.3.117.224", end = "84.3.117.224" }
  }

}

output "name" {
    value = join(",", toset(concat(flatten([for x in local.sql_allowed_ips: [x.start, x.end]]), ["0.0.0.0"])))
}