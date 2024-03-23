pipeline {
    agent any
    environment {
        // Defina aqui o nome da sua imagem e tag
        IMAGE_NAME = 'plataforma'
        IMAGE_TAG = 'latest'
        CONTAINER_NAME = 'plataforma'
    }
    stages {
        stage('Checkout') {
            steps {
                // Faz o checkout do código fonte
                checkout scm
            }
        }
        stage('Construir e Publicar Imagem') {
            steps {
                script {
                    // Constrói a nova imagem Docker usando o Dockerfile
                    docker.build("${IMAGE_NAME}:${IMAGE_TAG}")
                }
            }
        }
        stage('Remover Container Anterior') {
            steps {
                script {
                    // Remove o container anterior, se ele existir
                    sh "docker rm -f ${CONTAINER_NAME} || true"
                }
            }
        }
        stage('Subir Novo Container') {
            steps {
                script {
                    // Executa um novo container com a nova imagem
                    docker.run("--name ${CONTAINER_NAME} -d ${IMAGE_NAME}:${IMAGE_TAG}")
                }
            }
        }
        stage('Limpeza') {
            steps {
                script {
                    // Limpa imagens antigas não utilizadas
                    sh "docker image prune -a -f"
                }
            }
        }
    }
    post {
        always {
            // Limpa o workspace após a execução da pipeline
            cleanWs()
        }
    }
}
