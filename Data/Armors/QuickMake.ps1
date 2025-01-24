[CmdletBinding()]
param (
    # Template to base file off of
    [Parameter()]
    [string]
    $Template = '.\Templates\Template.json.ignore',

    # Armor Name
    [Parameter(Mandatory)]
    [string]
    $Name,

    # Start ID of the armor
    [Parameter(Mandatory)]
    [int]
    $StartID,

    # Armor Type
    [Parameter(Mandatory)]
    [ValidateSet('Cloth', 'Light', 'Heavy', 'Universal', 'None')]
    $Type,

    # Armor item count
    [Parameter()]
    [int]
    $Count = 3
)

$content = Get-Content -Path $Template

for ($i = $0; $i -lt $Count; $i++) {
    $placeholder = $Count - $i
    $id = $StartID + $i

    $content = $content.Replace("-00$placeholder", $id)
}

$Type = $Type.ToLower()
$content = $content.Replace('armor_type', $Type)

$makeName = [System.IO.Path]::GetFileNameWithoutExtension($Name)
$content = $content.Replace('make_name', $makeName)

$dst = "$Name.json"

Write-Output "Outputting to $dst"
Set-Content -Path $dst -Value $content