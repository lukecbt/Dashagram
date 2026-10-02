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
      context         = "."

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
      value = local.npgsql_connection_string
    }
    Jwt__Key = {
      value = var.jwt_key
    }
  }

  lifecycle {
    prevent_destroy = true
  }
}