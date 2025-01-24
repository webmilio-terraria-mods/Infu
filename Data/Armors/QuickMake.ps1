[CmdletBinding()]
param (
    # Template to base file off of
    [Parameter()]
    [string]
    $Template = './Template.json.ignore',

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
    [ValidateSet('Cloth', 'Light', 'Heavy')]
    $Type
)

$content = Get-Content -Path $Template

for ($i = $0; $i -le 2; $i++) {
    $placeholder = 3 - $i
    $id = $StartID + $i

    $content = $content.Replace("-00$placeholder", $id)
}

$Type = $Type.ToLower()
$content = $content.Replace('armor_type', $Type)

$root = [System.IO.Path]::GetDirectoryName($Template)
$dst = [System.IO.Path]::Combine($root, "$Name.json")

Write-Output "Outputting to $dst"
Set-Content -Path $dst -Value $content