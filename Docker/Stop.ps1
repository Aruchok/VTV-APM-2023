# Stopping containers.
$CurentDir = Split-Path -Parent $MyInvocation.MyCommand.Path

docker-compose down --volume
