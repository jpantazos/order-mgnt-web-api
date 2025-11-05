# Enable TCP/IP and Named Pipes for SQL Server
[System.Reflection.Assembly]::LoadWithPartialName('Microsoft.SqlServer.SqlWmiManagement') | Out-Null

$wmi = New-Object ('Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer') localhost

# Enable TCP/IP
$tcp = $wmi.ServerInstances['MSSQLSERVER'].ServerProtocols['Tcp']
$tcp.IsEnabled = $true
$tcp.Alter()
Write-Host "TCP/IP protocol enabled"

# Enable Named Pipes
$np = $wmi.ServerInstances['MSSQLSERVER'].ServerProtocols['Np']
$np.IsEnabled = $true
$np.Alter()
Write-Host "Named Pipes protocol enabled"

Write-Host "`nProtocols enabled successfully. Please restart SQL Server service."
