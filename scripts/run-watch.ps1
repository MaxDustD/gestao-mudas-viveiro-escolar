param(
    [int]$HttpPort = 5000,
    [int]$HttpsPort = 5001
)

Write-Host "Starting run-watch (http:$HttpPort, https:$HttpsPort)" -ForegroundColor Cyan

# find PID(s) listening on the HTTP port (IPv4/IPv6)
$lines = netstat -ano | Select-String ":$HttpPort " -SimpleMatch
if ($lines) {
    $pids = $lines | ForEach-Object { ($_ -split '\s+')[-1] } | Sort-Object -Unique
    foreach ($pid in $pids) {
        try {
            $proc = Get-Process -Id $pid -ErrorAction Stop
            Write-Host "Killing process $($proc.Id) $($proc.ProcessName) listening on port $HttpPort" -ForegroundColor Yellow
            Stop-Process -Id $pid -Force
        } catch {
            Write-Host "Could not stop PID $pid: $_" -ForegroundColor Red
        }
    }
} else {
    Write-Host "No process found listening on port $HttpPort" -ForegroundColor Green
}

# set URLs and start dotnet watch
$urls = "http://localhost:$HttpPort;https://localhost:$HttpsPort"
Write-Host "Starting dotnet watch with URLs: $urls" -ForegroundColor Cyan
$env:ASPNETCORE_URLS = $urls
dotnet watch --project src/ViveiroEscolar.Web
