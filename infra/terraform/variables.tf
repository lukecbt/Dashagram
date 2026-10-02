variable "neon_org_id" {
  type        = string
  description = "Neon organization ID the project is created with"
}

variable "jwt_key" {
  type        = string
  description = "JWT key used for signing and verifying JWT tokens"
  sensitive   = true
}