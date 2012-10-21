function Get-SurveySignature {
	param([String]$path)
	
	process {
		$include = "*.cs"
		Get-ChildItem -Path $path -Include $include -Recurse | Get-Signature | Sort -Property length -Descending
	}
}

function Monitor-SurveySignature {
	[CmdletBinding()]
	param([String]$path)
	
	while(-1) {
		$output = Get-SurveySignature $path
		if($output) { 
			Clear-Host
			Write-Host ([string]::join("`n", $output))
			sleep 5
		}
		else {
			break 
			}
	}
}

function GrabRelevantChars([String]$content) {
	$chars = "{", "}", ";"
	[char[]]$content | % { if($chars -contains $_) { $_ } }
}

function Get-Signature() {
	if($input) {
		$input | % { 
			"$($_.Name) in $($_.Directory.Name) " +
			"[$((cat $_ | Measure-Object -Line).Lines)]: " +
			[string]::Join('', (GrabRelevantChars((cat $_))))
		}
	}
}

Monitor-SurveySignature "D:\Code\PathToProject"