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
            // Verifica se o container existe
            sh(script: "docker ps -a | grep ${CONTAINER_NAME}", returnStatus: true)
            if (currentBuild.result == 'SUCCESS') {
                // Se o container existe, ele é removido
                sh "docker rm -f ${CONTAINER_NAME}"
            }
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
