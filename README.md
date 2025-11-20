
## Docker
A multi-stage Dockerfile is provided at the repo root.

Build image:
```bash
docker build -t patient-management-api .
```

Run container (basic):
```bash
docker run -d -p 6001:8080 --name patient-api patient-management-api:latest
```

Then browse:
- Swagger UI: http://localhost:8080/