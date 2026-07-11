# Local Development with Docker

## Prerequisites

- Docker Desktop installed
- Docker Desktop running

Verify the installation:

```bash
docker version
docker compose version
```

---

## Start the Application

From the repository root:

```bash
docker compose -f deploy/docker-compose.yml up --build
```

Or run it in the background:

```bash
docker compose -f deploy/docker-compose.yml up --build -d
```

---

## Verify the Containers

```bash
docker compose -f deploy/docker-compose.yml ps
```

Expected output:

- `biometric-platform-postgres` → **healthy**
- `biometric-platform-api` → **Up**

---

## View Container Logs

API:

```bash
docker compose -f deploy/docker-compose.yml logs -f api
```

PostgreSQL:

```bash
docker compose -f deploy/docker-compose.yml logs -f postgres
```

---

## Verify the API

Open Swagger UI:

```
http://localhost:8080/swagger
```

Or verify the endpoint with:

```bash
curl http://localhost:8080/swagger
```

---

## Verify the Database

Connect to PostgreSQL:

```bash
docker exec -it biometric-platform-postgres psql -U postgres
```

List all databases:

```sql
\l
```

Exit PostgreSQL:

```sql
\q
```

---

## Stop the Application

```bash
docker compose -f deploy/docker-compose.yml down
```

---

## Reset the Environment

Removes containers and volumes.

> **Warning:** This will permanently delete the local PostgreSQL database and uploaded files.

```bash
docker compose -f deploy/docker-compose.yml down -v
docker compose -f deploy/docker-compose.yml up --build
```

---

## Verify the Image Architecture (Apple Silicon)

If you're using an Apple Silicon Mac (M1/M2/M3/M4/M5), verify that the image was built for ARM64:

```bash
docker image inspect deploy-api --format '{{.Architecture}}'
```

Expected output:

```
arm64
```