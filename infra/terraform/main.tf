terraform {
  required_version = ">= 1.6"
  required_providers {
    neon = {
      source  = "kislerdm/neon"
      version = "~> 0.15"
    }
  }
}

provider "neon" {}

resource "neon_project" "this" {
  name      = "dashagram"
  region_id = "aws-eu-central-1"
  org_id    = var.neon_org_id
  history_retention_seconds = 21600
}