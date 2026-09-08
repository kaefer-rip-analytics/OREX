### Setting the connection string to secret manager
```bash
$bd_pw = "[BD PASSWORD HERE]"
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=10.221.2.102,14004;Database=OREX_TESTE;User Id=usrorex;Password=$db_pw;TrustServerCertificate=True;Encrypt=False;"

openssl rand -base64 64

dotnet user-secrets set "Jwt:Key" "KEY_GENERATED"

dotnet user-secrets set "Jwt-Issuer" "OrexApp"
dotnet user-secrets set "Jwt-Audience" "OrexFront"

dotnet user-secrets set "OpenTelemetry:OtplEndpoint" "http://localhost:5121"
```