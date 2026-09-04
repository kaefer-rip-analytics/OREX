### Setting the connection string to secret manager
```bash
$bd_pw = "[BD PASSWORD HERE]"
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=10.221.2.102,14004;Database=OREX_TESTE;User Id=usrorex;Password=$db_pw;TrustServerCertificate=True;Encrypt=False;"
```