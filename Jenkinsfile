pipeline {
    agent any

    environment {
        DOTNET_ROOT = "${HOME}/.dotnet"
        PATH = "${env.PATH}:${HOME}/.dotnet:${HOME}/.dotnet/tools"
    }

    stages {
        stage('Clone Repository') {
            steps {
                git url: 'https://github.com/ciprianhrt/e_commerce_api.git'
            }
        }

        stage('Restore Packages') {
            steps {
                sh 'dotnet restore ECommerceApi/ECommerceApi.sln'
            }
        }

        stage('Run Tests') {
            steps {
                sh 'dotnet test ECommerceApi/ECommerceApiTests/ECommerceApiTests.csproj --no-restore --verbosity normal'
            }
        }
    }
}

