# Cloudflare R2 bucket for Dashagram. This is used for image and object storage.
resource "cloudflare_r2_bucket" "uploads" {
  account_id = var.cloudflare_account_id
  name       = "dashagram-uploads"
  location   = "weur"

  lifecycle {
    prevent_destroy = true
  }
}