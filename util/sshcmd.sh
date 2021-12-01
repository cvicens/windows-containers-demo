#!/bin/bash
#
## Vars
windowshost=${1}
adminuser=${2}
sshkey=/tmp/ssh/private-key.pem
sshargs="-o StrictHostKeyChecking=no -o UserKnownHostsFile=/dev/null"

#
## Check if they provided $1
[[ ${#windowshost} -eq 0 ]] && echo "Please provide a Windows Hostname or IP Address" && exit 253

#
## Check if key is present
[[ ! -f ${sshkey} ]] && echo "FATAL: ${sshkey} not found. This container was deployed wrong." && exit 253

#
## SSH into the windows host
/usr/bin/ssh ${sshargs} -i ${sshkey} ${adminuser:=administrator}@${windowshost} powershell

##
##