pipeline {
    agent any   
    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }
        stage('Construir e Subir Serviços') {
            steps {
                script {
                    // Utiliza docker-compose para construir e subir os serviços
                   sh "docker-compose up -d --build --no-cache"
                }
            }
        }          
    }
    post {
        always {
            cleanWs()
        }
    }
}
