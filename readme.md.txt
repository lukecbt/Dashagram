# Dashagram API

## TODOS

Deployment Checklist

Push the project to GitHub. Keep .env and real credentials out of the repository.

Create a Neon Postgres project on the free plan. Save its host, database, username, and password. Neon’s free plan is a better fit than Render’s free Postgres for data you want to keep; Render’s free database expires after 30 days.

Create the hosted database schema. Apply the migrations already in the repo to Neon with dotnet ef database update. Future migrations should be created locally, committed, then applied during deployment.

Create a Render Web Service connected to the GitHub repository. Use Docker, the repository root as the build context, and Dockerfile as the Dockerfile path.

Configure Render’s environment variables:

ASPNETCORE_ENVIRONMENT=Production
ConnectionStrings__DefaultConnection with Neon’s database details and SSL enabled
Jwt__Key, Jwt__Issuer, and Jwt__Audience
Your Dockerfile currently listens on port 8080 and its health check also uses 8080. Configure Render’s PORT as 8080 to match, or update the Dockerfile and health check together.

Add a GitHub Actions workflow. On pull requests, set up .NET 10, restore, build, and run dotnet test. On pushes to your deploy branch, run the same checks, apply migrations to Neon, then deploy the API.

Add GitHub repository secrets for the Neon connection string and Render deploy-hook URL. Disable Render’s automatic deploy-on-push so it deploys only after Actions succeeds.

Verify the deployment using the API’s /health endpoint and a posts endpoint. Expect the free API service to sleep when idle and take a little time to wake up.