# Deploying containers.
$CurentDir = Split-Path -Parent $MyInvocation.MyCommand.Path

docker-compose up -d solution unit-tests