# Database
variable "neon_org_id" {
  type        = string
  description = "Neon organization ID the project is created with"
}

# R2
variable "cloudflare_account_id" {
  type        = string
  description = "Cloudflare account ID used for R2 bucket creation"
}

variable "cloudflare_api_token" {
  type        = string
  description = "Cloudflare API token used for R2 bucket creation"
  sensitive   = true
}

# App
variable "db_connection_string" {
  type        = string
  description = "PostgreSQL connection string used by the API service"
  sensitive   = true
}

variable "jwt_key" {
  type        = string
  description = "JWT key used for signing and verifying JWT tokens"
  sensitive   = true
}

variable "s3_access_key_id" {
  type        = string
  description = "S3 access key ID used for connecting to the R2 bucket"
  sensitive   = true
}

variable "s3_secret_access_key" {
  type        = string
  description = "S3 secret access key used for connecting to the R2 bucket"
  sensitive   = true
}