variable "neon_org_id" {
  type        = string
  description = "Neon organization ID the project is created with"
}

variable "jwt_key" {
  type        = string
  description = "JWT key used for signing and verifying JWT tokens"
  sensitive   = true
}

variable "db_connection_string" {
  type        = string
  description = "PostgreSQL connection string used by the API service"
  sensitive   = true
}