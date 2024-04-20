pipeline {
    agent any 

    stages {
        stage('Checkout') {
            steps {
                checkout scm 
            }
        }
        stage('Parar Serviços') {
            steps {
                script {                   
                    sh "docker-compose down"
                }
            }
        }
        stage('Construir e Subir Serviços') { 
            steps {
                script {                    
                    sh "docker-compose up -d --build"
                }
            }
        }
        stage('Limpar Imagens Docker') {
            steps {
                script {
                    sh "docker image prune -f"
                }
            }
        }
        stage('Limpar Recursos Docker') {
            steps {
                script {                   
                    sh "docker network prune -f"                   
                    sh "docker volume prune -f"
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
