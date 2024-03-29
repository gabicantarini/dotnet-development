param($installPath, $toolsPath, $package, $project)

Import-Module (Join-Path $toolsPath common.psm1) -Force

try {

    # Indicates if the current project is a VB project
    if ([string]::IsNullOrEmpty($project.CodeModel.Language)) {
        $IsVbProject = $project.FullName -like "*.vbproj"
    }
    else {
        $IsVbProject = ($project.CodeModel.Language -eq [EnvDTE.CodeModelLanguageConstants]::vsCMLanguageVB)
    }

    # Indicates if the current project is an MVC project
    $IsMvcProject = ($project.Object.References | Where-Object { $_.Identity -eq "System.Web.Mvc" }) -ne $null

    # The filters folder.
    $FiltersProjectItem = $project.ProjectItems.Item("Filters");

    if ($IsVbProject) {
        # For VB project, delete TokenHelper.cs, SharePointContext.cs and SharePointContextFilterAttribute.cs
        $project.ProjectItems | Where-Object { ($_.Name -eq "TokenHelper.cs") -or ($_.Name -eq "SharePointContext.cs") } | ForEach-Object { $_.Delete() }
        $FiltersProjectItem.ProjectItems | Where-Object { ($_.Name -eq "SharePointContextFilterAttribute.cs") } | ForEach-Object { $_.Delete() }

        # Delete SharePointContextFilterAttribute.vb if the web project is not MVC.
        if (!$IsMvcProject) {
            $FiltersProjectItem.ProjectItems | Where-Object { $_.Name -eq "SharePointContextFilterAttribute.vb" } | ForEach-Object { $_.Delete() }
        }

        # Add Imports for VB project
        $VbImports | ForEach-Object {
            if (!($project.Object.Imports -contains $_)) {
                $project.Object.Imports.Add($_)
            }
        }
    }
    else {
        # For CSharp project, delete TokenHelper.vb, SharePointContext.vb and SharePointContextFilterAttribute.vb
        $project.ProjectItems | Where-Object { ($_.Name -eq "TokenHelper.vb") -or ($_.Name -eq "SharePointContext.vb") } | ForEach-Object { $_.Delete() }
        $FiltersProjectItem.ProjectItems | Where-Object { ($_.Name -eq "SharePointContextFilterAttribute.vb") } | ForEach-Object { $_.Delete() }

        # Delete SharePointContextFilterAttribute.cs if the web project is not MVC.
        if (!$IsMvcProject) {
            $FiltersProjectItem.ProjectItems | Where-Object { $_.Name -eq "SharePointContextFilterAttribute.cs" } | ForEach-Object { $_.Delete() }
        }
    }

    # Delete the Filters folder if there is no item in it.
    if ($FiltersProjectItem.ProjectItems.Count -eq 0) {
        try {
            $FiltersProjectItem.Delete()
        }
        catch {
            Write-Host "Error while deleting the Filters folder: " + $_.Exception -ForegroundColor Yellow
        }
    }

    # Set CopyLocal = True as needed
    Foreach ($spRef in $CopyLocalReferences) {
        $project.Object.References | Where-Object { $_.Identity -eq $spRef } | ForEach-Object { $_.CopyLocal = $True }
    }

} catch {

    Write-Host "Error while installing package: " + $_.Exception -ForegroundColor Red
    exit
}
# SIG # Begin signature block
# MIIlkwYJKoZIhvcNAQcCoIIlhDCCJYACAQExDzANBglghkgBZQMEAgEFADB5Bgor
# BgEEAYI3AgEEoGswaTA0BgorBgEEAYI3AgEeMCYCAwEAAAQQH8w7YFlLCE63JNLG
# KX7zUQIBAAIBAAIBAAIBAAIBADAxMA0GCWCGSAFlAwQCAQUABCCD6qhoLGq68dNF
# mJh/bjSuZUHhdY1z/Y5sxCS/QO/zuKCCC3YwggT+MIID5qADAgECAhMzAAAE/lnK
# t+YqpSLBAAAAAAT+MA0GCSqGSIb3DQEBCwUAMH4xCzAJBgNVBAYTAlVTMRMwEQYD
# VQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNy
# b3NvZnQgQ29ycG9yYXRpb24xKDAmBgNVBAMTH01pY3Jvc29mdCBDb2RlIFNpZ25p
# bmcgUENBIDIwMTAwHhcNMjMwMjE2MjAxMTA5WhcNMjQwMTMxMjAxMTA5WjB0MQsw
# CQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9u
# ZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMR4wHAYDVQQDExVNaWNy
# b3NvZnQgQ29ycG9yYXRpb24wggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIB
# AQDPd/vELKzTiB3Rg4I1XSd7lwXSUG//YkcFj22rdNxMUPS+Eptq80TlhH03xtIE
# yrbYV+6S5s7CCY6Cjhda2foJOcyVayPKOrQyYZow8KbIKDL/QEafZr+sbXy34c8n
# GweKgqXjVfCyJFuR7wtRkaTV+miiLzsTcByTHU+Lzdb4Y+5BEYtdMJwCIkIY9kXT
# yCAHm3/1x9GALWpuUPRHyOBsDQcChOGnvetKBVYhORn8Y3Lb/2BnwQT+WIVdQlqs
# F8yhVwPgex7qEfHoianrXhC+WH+1AbaD0bqicteeRJYRGxGdB5YB1Bmw21W31iXp
# /hrzCnK8mlHVCUDC74lSNNIvAgMBAAGjggF9MIIBeTAfBgNVHSUEGDAWBgorBgEE
# AYI3PQYBBggrBgEFBQcDAzAdBgNVHQ4EFgQUvQbFseL64nzW8ZzXqQfwhLHuoDgw
# VAYDVR0RBE0wS6RJMEcxLTArBgNVBAsTJE1pY3Jvc29mdCBJcmVsYW5kIE9wZXJh
# dGlvbnMgTGltaXRlZDEWMBQGA1UEBRMNMjMwODY1KzUwMDIzMzAfBgNVHSMEGDAW
# gBTm/F97uyIAWORyTrX0IXQjMubvrDBWBgNVHR8ETzBNMEugSaBHhkVodHRwOi8v
# Y3JsLm1pY3Jvc29mdC5jb20vcGtpL2NybC9wcm9kdWN0cy9NaWNDb2RTaWdQQ0Ff
# MjAxMC0wNy0wNi5jcmwwWgYIKwYBBQUHAQEETjBMMEoGCCsGAQUFBzAChj5odHRw
# Oi8vd3d3Lm1pY3Jvc29mdC5jb20vcGtpL2NlcnRzL01pY0NvZFNpZ1BDQV8yMDEw
# LTA3LTA2LmNydDAMBgNVHRMBAf8EAjAAMA0GCSqGSIb3DQEBCwUAA4IBAQACKoJ8
# TKfm2TtDfauJ7Z3/D8VUjcpK4J23zTKglb9L6QaDeKr+He9pD7Yg74PDmWhZKcNc
# eQSe5BFcDKjKWqmFu85qjIkDb5znqAtVyq2yxBS4yGyMh3eY8TpbqBRaXEcI9zBK
# Mm/js+TvOSev24Ek5y7rrlZOIPXqZjPScx6PzqW9tC3GFnzJeE6TaTInDKxlMzRC
# MRxFgSSU1rIXj9Neu3AeyQKemdhQ/7w5bW+etkaBjTdr7vyInmPlYmzfsT5oOzAP
# R3UWTfuLk5WIM/vAjeWtyUTsNq4x5sX/mzhfu2/qWLrZPSIc9zKfHBtTPjw4gt8a
# aU6AzhdcXafzWFqeMIIGcDCCBFigAwIBAgIKYQxSTAAAAAAAAzANBgkqhkiG9w0B
# AQsFADCBiDELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNV
# BAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjEyMDAG
# A1UEAxMpTWljcm9zb2Z0IFJvb3QgQ2VydGlmaWNhdGUgQXV0aG9yaXR5IDIwMTAw
# HhcNMTAwNzA2MjA0MDE3WhcNMjUwNzA2MjA1MDE3WjB+MQswCQYDVQQGEwJVUzET
# MBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UEChMV
# TWljcm9zb2Z0IENvcnBvcmF0aW9uMSgwJgYDVQQDEx9NaWNyb3NvZnQgQ29kZSBT
# aWduaW5nIFBDQSAyMDEwMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA
# 6Q5kUHlntcTj/QkATJ6UrPdWaOpE2M/FWE+ppXZ8bUW60zmStKQe+fllguQX0o/9
# RJwI6GWTzixVhL99COMuK6hBKxi3oktuSUxrFQfe0dLCiR5xlM21f0u0rwjYzIjW
# axeUOpPOJj/s5v40mFfVHV1J9rIqLtWFu1k/+JC0K4N0yiuzO0bj8EZJwRdmVMkc
# vR3EVWJXcvhnuSUgNN5dpqWVXqsogM3Vsp7lA7Vj07IUyMHIiiYKWX8H7P8O7YAS
# NUwSpr5SW/Wm2uCLC0h31oVH1RC5xuiq7otqLQVcYMa0KlucIxxfReMaFB5vN8sZ
# M4BqiU2jamZjeJPVMM+VHwIDAQABo4IB4zCCAd8wEAYJKwYBBAGCNxUBBAMCAQAw
# HQYDVR0OBBYEFOb8X3u7IgBY5HJOtfQhdCMy5u+sMBkGCSsGAQQBgjcUAgQMHgoA
# UwB1AGIAQwBBMAsGA1UdDwQEAwIBhjAPBgNVHRMBAf8EBTADAQH/MB8GA1UdIwQY
# MBaAFNX2VsuP6KJcYmjRPZSQW9fOmhjEMFYGA1UdHwRPME0wS6BJoEeGRWh0dHA6
# Ly9jcmwubWljcm9zb2Z0LmNvbS9wa2kvY3JsL3Byb2R1Y3RzL01pY1Jvb0NlckF1
# dF8yMDEwLTA2LTIzLmNybDBaBggrBgEFBQcBAQROMEwwSgYIKwYBBQUHMAKGPmh0
# dHA6Ly93d3cubWljcm9zb2Z0LmNvbS9wa2kvY2VydHMvTWljUm9vQ2VyQXV0XzIw
# MTAtMDYtMjMuY3J0MIGdBgNVHSAEgZUwgZIwgY8GCSsGAQQBgjcuAzCBgTA9Bggr
# BgEFBQcCARYxaHR0cDovL3d3dy5taWNyb3NvZnQuY29tL1BLSS9kb2NzL0NQUy9k
# ZWZhdWx0Lmh0bTBABggrBgEFBQcCAjA0HjIgHQBMAGUAZwBhAGwAXwBQAG8AbABp
# AGMAeQBfAFMAdABhAHQAZQBtAGUAbgB0AC4gHTANBgkqhkiG9w0BAQsFAAOCAgEA
# GnTvV08pe8QWhXi4UNMi/AmdrIKX+DT/KiyXlRLl5L/Pv5PI4zSp24G43B4AvtI1
# b6/lf3mVd+UC1PHr2M1OHhthosJaIxrwjKhiUUVnCOM/PB6T+DCFF8g5QKbXDrMh
# KeWloWmMIpPMdJjnoUdD8lOswA8waX/+0iUgbW9h098H1dlyACxphnY9UdumOUjJ
# N2FtB91TGcun1mHCv+KDqw/ga5uV1n0oUbCJSlGkmmzItx9KGg5pqdfcwX7RSXCq
# tq27ckdjF/qm1qKmhuyoEESbY7ayaYkGx0aGehg/6MUdIdV7+QIjLcVBy78dTMgW
# 77Gcf/wiS0mKbhXjpn92W9FTeZGFndXS2z1zNfM8rlSyUkdqwKoTldKOEdqZZ14y
# jPs3hdHcdYWch8ZaV4XCv90Nj4ybLeu07s8n07VeafqkFgQBpyRnc89NT7beBVaX
# evfpUk30dwVPhcbYC/GO7UIJ0Q124yNWeCImNr7KsYxuqh3khdpHM2KPpMmRM19x
# HkCvmGXJIuhCISWKHC1g2TeJQYkqFg/XYTyUaGBS79ZHmaCAQO4VgXc+nOBTGBpQ
# HTiVmx5mMxMnORd4hzbOTsNfsvU9R1O24OXbC2E9KteSLM43Wj5AQjGkHxAIwlac
# vyRdUQKdannSF9PawZSOB3slcUSrBmrm1MbfI5qWdcUxghlzMIIZbwIBATCBlTB+
# MQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVk
# bW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMSgwJgYDVQQDEx9N
# aWNyb3NvZnQgQ29kZSBTaWduaW5nIFBDQSAyMDEwAhMzAAAE/lnKt+YqpSLBAAAA
# AAT+MA0GCWCGSAFlAwQCAQUAoIGuMBkGCSqGSIb3DQEJAzEMBgorBgEEAYI3AgEE
# MBwGCisGAQQBgjcCAQsxDjAMBgorBgEEAYI3AgEVMC8GCSqGSIb3DQEJBDEiBCAd
# hpszzPJKK7pPhpccCHLAk+VRpuh1kCeHu0MvceMh8DBCBgorBgEEAYI3AgEMMTQw
# MqAUgBIATQBpAGMAcgBvAHMAbwBmAHShGoAYaHR0cDovL3d3dy5taWNyb3NvZnQu
# Y29tMA0GCSqGSIb3DQEBAQUABIIBAFr0KzobB07RL9ofrrjyvhXfJHFU2TsI0DFI
# lZiuPCnZ+0Ub0WrALLZLkGvvqv15yQrSg5tM2AfolVkXUPxpdxAH8ADsGBd1LnCj
# rNlmY85KuBA38a0LYSxAGSppz+XhEQ31J0r+W+f5sLSWoDx9qtwYZvCgFCe9PPbK
# 3CJu4mTWPek+R9IueLocwwljsnlmyjNQYkXhjZLs7WVJYbKLaGCk+FMgBBlmTDrn
# +BB20zj1xBpAU9zp26MjsCwnGSx4S/zT+dor2g19S15Q6zsM2Rp3gIsE8seRksHf
# SZwr2DQ/iJJvJD79knOsN9ZnZrUlxuYk3MJMl5KYMCcBl7VKKIOhghb9MIIW+QYK
# KwYBBAGCNwMDATGCFukwghblBgkqhkiG9w0BBwKgghbWMIIW0gIBAzEPMA0GCWCG
# SAFlAwQCAQUAMIIBUQYLKoZIhvcNAQkQAQSgggFABIIBPDCCATgCAQEGCisGAQQB
# hFkKAwEwMTANBglghkgBZQMEAgEFAAQgVk6S4XnaQUSMEn2bh6bUZlxzJeZY8qDl
# rWhZwf1WoogCBmSwHQvZxhgTMjAyMzA3MjExODQyMDQuMjU0WjAEgAIB9KCB0KSB
# zTCByjELMAkGA1UEBhMCVVMxEzARBgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcT
# B1JlZG1vbmQxHjAcBgNVBAoTFU1pY3Jvc29mdCBDb3Jwb3JhdGlvbjElMCMGA1UE
# CxMcTWljcm9zb2Z0IEFtZXJpY2EgT3BlcmF0aW9uczEmMCQGA1UECxMdVGhhbGVz
# IFRTUyBFU046MTJCQy1FM0FFLTc0RUIxJTAjBgNVBAMTHE1pY3Jvc29mdCBUaW1l
# LVN0YW1wIFNlcnZpY2WgghFUMIIHDDCCBPSgAwIBAgITMwAAAcpPwrPtAw0YbAAB
# AAAByjANBgkqhkiG9w0BAQsFADB8MQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2Fz
# aGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENv
# cnBvcmF0aW9uMSYwJAYDVQQDEx1NaWNyb3NvZnQgVGltZS1TdGFtcCBQQ0EgMjAx
# MDAeFw0yMjExMDQxOTAxNDBaFw0yNDAyMDIxOTAxNDBaMIHKMQswCQYDVQQGEwJV
# UzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UEBxMHUmVkbW9uZDEeMBwGA1UE
# ChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMSUwIwYDVQQLExxNaWNyb3NvZnQgQW1l
# cmljYSBPcGVyYXRpb25zMSYwJAYDVQQLEx1UaGFsZXMgVFNTIEVTTjoxMkJDLUUz
# QUUtNzRFQjElMCMGA1UEAxMcTWljcm9zb2Z0IFRpbWUtU3RhbXAgU2VydmljZTCC
# AiIwDQYJKoZIhvcNAQEBBQADggIPADCCAgoCggIBAMMBnKvY+dK1hJHC0pRmS8Hl
# FX8V0WlI4j7MmjVcT3TEbNwlXufLIrvxJGlbe7ezzUz5KP7PJTbbrAHixJ85IuOM
# sZNnPXpJvBNUkNmFfW/PyzsXymqWfLVZT8scOg5hczRUI4n1ZvBIme5RUaGOOPL6
# XGwQ9fKo2DRf9md0kHIXLHVdTfWOldlhpeeVZi6hUV+fnabXY63gV84t1lvU7KAh
# FnGAgSucGIEvbU9kkkk82nt45ncONIPiziMq1Txdg6M5Erb+iQQz78GoV1qShqTu
# 6x6yhfqOxAjf1YkBTGcqf78Xj4lAAQzasxFCPLO5JWJnh1743kDqvwjNwv2PG493
# yTm52R+3gF70Q58U/Eelv7g9ZhlCb7/TPQTLt54SqSpksc8zuS7XDBIdrTF8YTBh
# jTFD9wzhCt1/tvyw9WdN7rCU5OaRzaB/4AyL26e/OwsONLbAKYgL7ax2MLm9E6iL
# /GcutpfJ/LPzL/z++uAk6q8XF82pQR4fl8uFz45mnZz5GScnKdynM25IPUG7yadB
# /9BbMB0vnnxaH6QzScC4dYbP2jItSv6MxL+/1iyD7A4Cten1P2scm6jCpNDTsYHp
# VIwGCpeMFNGOkOKvTf/T8AEtHLIn0IWNaKEHwCNIgwgkhAE7JQ/G0ztLUbanXCHW
# rzkXqs6D3bwL9w69V10PAgMBAAGjggE2MIIBMjAdBgNVHQ4EFgQU+sf+pUuBEkPC
# mYXdEYFO0WFzPH8wHwYDVR0jBBgwFoAUn6cVXQBeYl2D9OXSZacbUzUZ6XIwXwYD
# VR0fBFgwVjBUoFKgUIZOaHR0cDovL3d3dy5taWNyb3NvZnQuY29tL3BraW9wcy9j
# cmwvTWljcm9zb2Z0JTIwVGltZS1TdGFtcCUyMFBDQSUyMDIwMTAoMSkuY3JsMGwG
# CCsGAQUFBwEBBGAwXjBcBggrBgEFBQcwAoZQaHR0cDovL3d3dy5taWNyb3NvZnQu
# Y29tL3BraW9wcy9jZXJ0cy9NaWNyb3NvZnQlMjBUaW1lLVN0YW1wJTIwUENBJTIw
# MjAxMCgxKS5jcnQwDAYDVR0TAQH/BAIwADATBgNVHSUEDDAKBggrBgEFBQcDCDAN
# BgkqhkiG9w0BAQsFAAOCAgEA2NB5KgktyrinnxV/P65v3bQ/dOunShJXjaq4sGar
# DOGJvBMOEIqjqju5lqGGGOCucvHl0jhyjVzn6TukW2mI/IwjnCQAebai5eYYjIGj
# GwFS65dYZYsfbJaDuOSzjdCaYSr2tw+gFSgOz+/JgPYkR2WFFa0Ysn70I1sZlJ8Y
# YrPH6Jvdvv0R3BGdQ4efqeIM3ni/OGJdNFsqvRHIASin4KtJQo1jUqtQFBtbBC5Y
# FzkrymeP5V1x/jH4XB3vdViyGmZEvx7vcHU0+iNeAQve8oQ7GVlwJdCCfNNSOlLR
# BaQrE3jsmQSewhdK3+ZcSfLRPaIzbMqlySrsFZ9HsAjJGas/3BML/RRMfEJrGdGT
# aHU7bJflfXWZvnlKzCmZOjyrCgK7UPox5H1bB4Cjg5aHpaTph6oF+1GztDVZyJaT
# +eYEE6le+r1O8EV6VH8+yoRNwWVzYMBtOXUZwlw8K4YWM/iLW87NStUfCOGDAyY1
# vdAEgoPSX/scoESmRih46NgbNl8mirjdT+hRQiFVzeyoB5f22YmpBqu5OU0ODlyu
# cZIQh4Y3YB7sY94SuQGjsvhl8Tv428qhkanksX/k0z7loQ1rqyKN90TAxnuQ2yJo
# 3G3I+nVYUHr6hwpUdTh0n6/vJtYVkSRpQtWrXDlimowK8DlEaiUlZU4cFYCMsxjZ
# o84wggdxMIIFWaADAgECAhMzAAAAFcXna54Cm0mZAAAAAAAVMA0GCSqGSIb3DQEB
# CwUAMIGIMQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UE
# BxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMTIwMAYD
# VQQDEylNaWNyb3NvZnQgUm9vdCBDZXJ0aWZpY2F0ZSBBdXRob3JpdHkgMjAxMDAe
# Fw0yMTA5MzAxODIyMjVaFw0zMDA5MzAxODMyMjVaMHwxCzAJBgNVBAYTAlVTMRMw
# EQYDVQQIEwpXYXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVN
# aWNyb3NvZnQgQ29ycG9yYXRpb24xJjAkBgNVBAMTHU1pY3Jvc29mdCBUaW1lLVN0
# YW1wIFBDQSAyMDEwMIICIjANBgkqhkiG9w0BAQEFAAOCAg8AMIICCgKCAgEA5OGm
# TOe0ciELeaLL1yR5vQ7VgtP97pwHB9KpbE51yMo1V/YBf2xK4OK9uT4XYDP/XE/H
# ZveVU3Fa4n5KWv64NmeFRiMMtY0Tz3cywBAY6GB9alKDRLemjkZrBxTzxXb1hlDc
# wUTIcVxRMTegCjhuje3XD9gmU3w5YQJ6xKr9cmmvHaus9ja+NSZk2pg7uhp7M62A
# W36MEBydUv626GIl3GoPz130/o5Tz9bshVZN7928jaTjkY+yOSxRnOlwaQ3KNi1w
# jjHINSi947SHJMPgyY9+tVSP3PoFVZhtaDuaRr3tpK56KTesy+uDRedGbsoy1cCG
# MFxPLOJiss254o2I5JasAUq7vnGpF1tnYN74kpEeHT39IM9zfUGaRnXNxF803RKJ
# 1v2lIH1+/NmeRd+2ci/bfV+AutuqfjbsNkz2K26oElHovwUDo9Fzpk03dJQcNIIP
# 8BDyt0cY7afomXw/TNuvXsLz1dhzPUNOwTM5TI4CvEJoLhDqhFFG4tG9ahhaYQFz
# ymeiXtcodgLiMxhy16cg8ML6EgrXY28MyTZki1ugpoMhXV8wdJGUlNi5UPkLiWHz
# NgY1GIRH29wb0f2y1BzFa/ZcUlFdEtsluq9QBXpsxREdcu+N+VLEhReTwDwV2xo3
# xwgVGD94q0W29R6HXtqPnhZyacaue7e3PmriLq0CAwEAAaOCAd0wggHZMBIGCSsG
# AQQBgjcVAQQFAgMBAAEwIwYJKwYBBAGCNxUCBBYEFCqnUv5kxJq+gpE8RjUpzxD/
# LwTuMB0GA1UdDgQWBBSfpxVdAF5iXYP05dJlpxtTNRnpcjBcBgNVHSAEVTBTMFEG
# DCsGAQQBgjdMg30BATBBMD8GCCsGAQUFBwIBFjNodHRwOi8vd3d3Lm1pY3Jvc29m
# dC5jb20vcGtpb3BzL0RvY3MvUmVwb3NpdG9yeS5odG0wEwYDVR0lBAwwCgYIKwYB
# BQUHAwgwGQYJKwYBBAGCNxQCBAweCgBTAHUAYgBDAEEwCwYDVR0PBAQDAgGGMA8G
# A1UdEwEB/wQFMAMBAf8wHwYDVR0jBBgwFoAU1fZWy4/oolxiaNE9lJBb186aGMQw
# VgYDVR0fBE8wTTBLoEmgR4ZFaHR0cDovL2NybC5taWNyb3NvZnQuY29tL3BraS9j
# cmwvcHJvZHVjdHMvTWljUm9vQ2VyQXV0XzIwMTAtMDYtMjMuY3JsMFoGCCsGAQUF
# BwEBBE4wTDBKBggrBgEFBQcwAoY+aHR0cDovL3d3dy5taWNyb3NvZnQuY29tL3Br
# aS9jZXJ0cy9NaWNSb29DZXJBdXRfMjAxMC0wNi0yMy5jcnQwDQYJKoZIhvcNAQEL
# BQADggIBAJ1VffwqreEsH2cBMSRb4Z5yS/ypb+pcFLY+TkdkeLEGk5c9MTO1OdfC
# cTY/2mRsfNB1OW27DzHkwo/7bNGhlBgi7ulmZzpTTd2YurYeeNg2LpypglYAA7AF
# vonoaeC6Ce5732pvvinLbtg/SHUB2RjebYIM9W0jVOR4U3UkV7ndn/OOPcbzaN9l
# 9qRWqveVtihVJ9AkvUCgvxm2EhIRXT0n4ECWOKz3+SmJw7wXsFSFQrP8DJ6LGYnn
# 8AtqgcKBGUIZUnWKNsIdw2FzLixre24/LAl4FOmRsqlb30mjdAy87JGA0j3mSj5m
# O0+7hvoyGtmW9I/2kQH2zsZ0/fZMcm8Qq3UwxTSwethQ/gpY3UA8x1RtnWN0SCyx
# TkctwRQEcb9k+SS+c23Kjgm9swFXSVRk2XPXfx5bRAGOWhmRaw2fpCjcZxkoJLo4
# S5pu+yFUa2pFEUep8beuyOiJXk+d0tBMdrVXVAmxaQFEfnyhYWxz/gq77EFmPWn9
# y8FBSX5+k77L+DvktxW/tM4+pTFRhLy/AsGConsXHRWJjXD+57XQKBqJC4822rpM
# +Zv/Cuk0+CQ1ZyvgDbjmjJnW4SLq8CdCPSWU5nR0W2rRnj7tfqAxM328y+l7vzhw
# RNGQ8cirOoo6CGJ/2XBjU02N7oJtpQUQwXEGahC0HVUzWLOhcGbyoYICyzCCAjQC
# AQEwgfihgdCkgc0wgcoxCzAJBgNVBAYTAlVTMRMwEQYDVQQIEwpXYXNoaW5ndG9u
# MRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3NvZnQgQ29ycG9yYXRp
# b24xJTAjBgNVBAsTHE1pY3Jvc29mdCBBbWVyaWNhIE9wZXJhdGlvbnMxJjAkBgNV
# BAsTHVRoYWxlcyBUU1MgRVNOOjEyQkMtRTNBRS03NEVCMSUwIwYDVQQDExxNaWNy
# b3NvZnQgVGltZS1TdGFtcCBTZXJ2aWNloiMKAQEwBwYFKw4DAhoDFQCjjueXDE+L
# Mjz+pwpYNiFn2ozKpKCBgzCBgKR+MHwxCzAJBgNVBAYTAlVTMRMwEQYDVQQIEwpX
# YXNoaW5ndG9uMRAwDgYDVQQHEwdSZWRtb25kMR4wHAYDVQQKExVNaWNyb3NvZnQg
# Q29ycG9yYXRpb24xJjAkBgNVBAMTHU1pY3Jvc29mdCBUaW1lLVN0YW1wIFBDQSAy
# MDEwMA0GCSqGSIb3DQEBBQUAAgUA6GUm9DAiGA8yMDIzMDcyMTIzNDcwMFoYDzIw
# MjMwNzIyMjM0NzAwWjB0MDoGCisGAQQBhFkKBAExLDAqMAoCBQDoZSb0AgEAMAcC
# AQACAhlZMAcCAQACAhKpMAoCBQDoZnh0AgEAMDYGCisGAQQBhFkKBAIxKDAmMAwG
# CisGAQQBhFkKAwKgCjAIAgEAAgMHoSChCjAIAgEAAgMBhqAwDQYJKoZIhvcNAQEF
# BQADgYEAhFG4UvyRgf4d2irgdm1fSO1Qz78a0kw5k+g34ONm2kKFsZO9LqKyelm4
# XfM9DihxWfsJAYVxQWhpArH3zDwv+cwJkU8/EyF6riOWQLE4sOCK16iX0vfScgXR
# YonRl9SFk1Zu5exmk6PPFCMKjfDlT+n4NHI9KeixWgkPgk3JhBwxggQNMIIECQIB
# ATCBkzB8MQswCQYDVQQGEwJVUzETMBEGA1UECBMKV2FzaGluZ3RvbjEQMA4GA1UE
# BxMHUmVkbW9uZDEeMBwGA1UEChMVTWljcm9zb2Z0IENvcnBvcmF0aW9uMSYwJAYD
# VQQDEx1NaWNyb3NvZnQgVGltZS1TdGFtcCBQQ0EgMjAxMAITMwAAAcpPwrPtAw0Y
# bAABAAAByjANBglghkgBZQMEAgEFAKCCAUowGgYJKoZIhvcNAQkDMQ0GCyqGSIb3
# DQEJEAEEMC8GCSqGSIb3DQEJBDEiBCBESvq4DWtqZUDeARGCqJF3R39zkl530N3c
# fwoSmtXmbzCB+gYLKoZIhvcNAQkQAi8xgeowgecwgeQwgb0EIBM9G/Ob61VNrJWQ
# JOI7dUgxJOiM2QMFQqFsBdys3BrgMIGYMIGApH4wfDELMAkGA1UEBhMCVVMxEzAR
# BgNVBAgTCldhc2hpbmd0b24xEDAOBgNVBAcTB1JlZG1vbmQxHjAcBgNVBAoTFU1p
# Y3Jvc29mdCBDb3Jwb3JhdGlvbjEmMCQGA1UEAxMdTWljcm9zb2Z0IFRpbWUtU3Rh
# bXAgUENBIDIwMTACEzMAAAHKT8Kz7QMNGGwAAQAAAcowIgQgmYVwgbAs+E02sY3t
# WIddyN65Zvhi/c0eraMAfr9dSJswDQYJKoZIhvcNAQELBQAEggIAUgfguLYpt5j1
# AGm3OizcF0gKch0buFkpUp86e0crXi8tLX833s0UsvYGxsO4pJql4feX4/NJ5ajd
# kP1tIk7oEgq0pdMnAApp6vgJPrxkS2gpxw/GGSLMFo30Jsr1i+B+VKH908gNIMLC
# W+e4eHo7jjyLhty3gagh7p7WI0Rp0u6qln5nQj0+YHKxf6O0FIzYozQX3B52tCXB
# 3voAghsVQ1u+nxTH4wNsb1LEJkUHqNSzvRaYu6gNXl+DeRPObZFPFXnaXultRjam
# /wKE41hm5HrW1d1bNX0OTda28qjZZ817fq/ew+IJ0EB8hdTcVpjKWN9yhZvl2dXr
# J9Og/58F32YxIgnmZpMWPq8cwfm3ZRAUeiZAQg+cTR3MBHL222MuxEc50WJcwY77
# /HrVXMfWKOTcVuWHG7o46oByDS7TkhKQ1yORc0S/e45DS1IAmSUNxzzz+jQLdMm3
# qMm+M1m90xfEkK3JaN285MRJX5ENllxhpXTWigmQ876DLxDih7Qpt456NQAvjTbS
# knP+b08s9Xt27uZy9ymexGeYVpS8j8AJWRfYTIqXkCwD9XORvhmdAPf7nhAcCanD
# iPFyiET7u++i/IuXYa7Ele37QFJqIPRGklkoozHvGLio0Hn7R/PO2x/KoHtGe6yP
# sUKhwqYeJmvEQvZwRqDqi1evK93dX3A=
# SIG # End signature block
