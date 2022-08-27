# DotNet Integration Testing

## Pre-requisites

### Docker (Windows)
1. Enable the Linux Subsystem feature (powershell, admin, may require restart)
    ```powershell
    Enable-WindowsOptionalFeature -Online -FeatureName Microsoft-Windows-Subsystem-Linux
    ```
1. Enable Hyper-V (powershell, admin, may require restart)
    ```powershell
    DISM /Online /Enable-Feature /All /FeatureName:Microsoft-Hyper-V
    bcdedit /set hypervisorlaunchtype auto
    ```
1. Download and install the latest Linux kernel update (https://wslstorestorage.blob.core.windows.net/wslblob/wsl_update_x64.msi)
1. Install WSL2 (powershell, admin, may require restart)
    ```powershell
    wsl --set-default-version 2
    wsl --install -d Ubuntu
    ```
1.  Ensure your installed Ubunto is using WSL2, if not update it (powershell, admin)
    ```powershell
    wsl -l -v
    wsl --set-version Ubuntu 2
    ```
1.  Install docker on your linux subsystem, either by following the steps on https://docs.docker.com/engine/install/ubuntu/. or by running the following commands on your ubunto console
    ```powershell
    wsl
    ```
    ```bash
    #/bin/bash 

    # 1. Required dependencies 
    sudo apt-get update 
    sudo apt-get -y install apt-transport-https ca-certificates curl gnupg lsb-release 

    # 2. GPG key 
    curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg

    # 3. Use stable repository for Docker 
    echo "deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu bionic stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null

    # 4. Install Docker 
    sudo apt-get update
    sudo apt-get -y install docker-ce docker-ce-cli containerd.io 

    # 5. Add user to docker group 
    sudo groupadd docker 
    sudo usermod -aG docker $USER

    # 6. Configure daemon to expose itself via a tcp socker
    sudo touch /etc/docker/daemon.json
    echo '{ "hosts": ["unix:///var/run/docker.sock", "tcp://0.0.0.0:2375"] }' | sudo tee -a /etc/docker/daemon.json
    ```
1.  Start the docker service (todo - make docker auto-start)
    ```powershell
    wsl
    ```
    ```bash
    sudo service docker start
    ```
1.  Open a new command window and verify that your instillation was successful by running the following (powershell)
    ```powershell
    wsl docker ps
    ```
1.  **Important** - ensure the `DOCKER_HOST` environment variable is set to your mapped TCP port, which will be `tcp://localhost:2375/`
