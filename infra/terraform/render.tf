# Render web service resource for Dashagram. This is a managed application hosted on Render.
resource "render_web_service" "api" {
  name              = "Dashagram"
  plan              = "starter"
  region            = "frankfurt"
  health_check_path = "/health"

  runtime_source = {
    docker = {
      repo_url        = "https://github.com/lukecbt/Dashagram"
      branch          = "main"
      dockerfile_path = "./src/Dashagram.Api/Dockerfile"
      context         = "./src"

      auto_deploy_trigger = "checksPass"

      build_filter = {
        ignored_paths = ["infra/terraform/**"]
      }
    }
  }

  # Set environment variables for the Render web service container.
  env_vars = {
    PORT = {
      value = "8080"
    }
    ConnectionStrings__DefaultConnection = {
      value = var.db_connection_string
    }
    Jwt__Key = {
      value = var.jwt_key
    }
    S3__ServiceUrl = {
      value = "https://${var.cloudflare_account_id}.r2.cloudflarestorage.com"
    }
    S3__Region = {
      value = "auto"
    }
    S3__AccessKeyId = {
      value = var.s3_access_key_id
    }
    S3__SecretAccessKey = {
      value = var.s3_secret_access_key
    }
    S3__BucketName = {
      value = cloudflare_r2_bucket.uploads.name
    }
    S3__ForcePathStyle = {
      value = "true"
    }
    S3__PublicBaseUrl = {
      value = var.s3_public_base_url
    }
  }

  lifecycle {
    prevent_destroy = true
  }
}