pipeline {
    agent any
    environment {
        // Mantenha as definições de ambiente caso precise usar em comandos específicos
        IMAGE_NAME = 'plataforma'
        IMAGE_TAG = 'latest'
        CONTAINER_NAME = 'plataforma'
    }
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
                    sh "docker-compose up -d --build"
                }
            }
        }
        stage('Remover Serviços Anteriores') {
            steps {
                script {
                    // Utiliza docker-compose para parar e remover os serviços anteriores
                    sh "docker-compose down"
                }
            }
        }
        stage('Limpeza') {
            steps {
                script {
                    sh "docker image prune -a -f --filter 'until=24h'"
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
