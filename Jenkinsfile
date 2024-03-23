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
                    sh "docker build -t plataforma:latest -f Plataforma.Isolar/Dockerfile Plataforma.Isolar/"
                }
            }
        }
        stage('Remover Container Anterior') {
            steps {
                script {
                    // Verifica se o container existe e remove se existir
                    sh "docker ps -a | grep -q ${CONTAINER_NAME} && docker rm -f ${CONTAINER_NAME} || true"
                }
            }
        }
        stage('Subir Novo Container') {
            steps {
                script {
                    // Executa um novo container com a nova imagem, fazendo bind das portas 4000 e 4001
                    sh "docker run --name ${CONTAINER_NAME} -d -p 4000:4000 -p 4001:4001 ${IMAGE_NAME}:${IMAGE_TAG}"
                }
            }
        }
        stage('Limpeza') {
            steps {
                script {
                    // Limpa imagens antigas não utilizadas
                    sh "docker image prune -a -f --filter 'until=24h'"
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
