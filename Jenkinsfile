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
