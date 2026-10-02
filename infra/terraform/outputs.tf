locals {
  npgsql_connection_string = join(";", [
    "Host=${neon_project.this.database_host}",
    "Database=${neon_project.this.database_name}",
    "Username=${neon_project.this.database_user}",
    "Password=${neon_project.this.database_password}",
    "SSL Mode=Require",
  ])
}

output "npgsql_connection_string" {
  value     = local.npgsql_connection_string
  sensitive = true
}