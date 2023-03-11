$DEFAULT_MIGRATION_NAME = "NEW_MIGRATION";
$DEFAULT_MIGRATIONS_DIRECTORY = "./GrpcTodo.Server/Infra/Data/Migrations";

if ($args.Length -ge 1 -and $args[0])
{
    $DEFAULT_MIGRATION_NAME = $args[0];
}

if ($args.Length -ge 2 -and $args[1])
{
    $DEFAULT_MIGRATIONS_DIRECTORY = $args[1];
}

$DEFAULT_MIGRATION_NAME = "_" + $DEFAULT_MIGRATION_NAME;

$files_in_directory = Get-ChildItem $DEFAULT_MIGRATIONS_DIRECTORY | Select-Object Name;

$max_migration_version = 0;

for ($i = 0; $i -le $files_in_directory.Length; $i++)
{
    $file = $files_in_directory[$i].Name;

    if ($file -and $file.StartsWith("_"))
    {
        $file_version = $file -replace '^_(\d+).*', '$1';

        if ($file_version)
        {
            $version = [System.Int32]::Parse($file_version);

            if ($version -gt $max_migration_version)
            {
                $max_migration_version = $version;
            }
        }
    }
}

$new_migration_version = $max_migration_version + 1;
$new_migration_name = [System.String]::Join($new_migration_version, "_", $DEFAULT_MIGRATION_NAME);

$full_path = $DEFAULT_MIGRATIONS_DIRECTORY + "/" + $new_migration_name + ".cs";

[void](New-Item -Path $full_path);

Add-Content $full_path "using FluentMigrator;

namespace Core.Infra.Data.Migrations;

[Migration($new_migration_version)]
public sealed class $new_migration_name : Migration
{
    public override void Down()
    {
    }

    public override void Up()
    {
    }
}"

Write-Host 
Write-Host "Migration $full_path created successfully";
Write-Host 

