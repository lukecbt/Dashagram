terraform {
  required_version = ">= 1.6"

  # Configure the backend to use Terraform Cloud for state management
  cloud {
    organization = "longdog-labs"

    workspaces {
      name = "dashagram"
    }
  }

  required_providers {
    neon = {
      source  = "kislerdm/neon"
      version = "~> 0.15"
    }
    render = {
      source  = "render-oss/render"
      version = "~> 1.9"
    }
  }
}

# Database project for Dashagram. This is a managed Postgres database hosted on Neon.
provider "neon" {}

# Render project for Dashagram. This is a managed application hosted on Render.
provider "render" {}

resource "neon_project" "this" {
  name                      = "dashagram"
  region_id                 = "aws-eu-central-1"
  org_id                    = var.neon_org_id
  history_retention_seconds = 21600

  lifecycle {
    prevent_destroy = true
  }
}