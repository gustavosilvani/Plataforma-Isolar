pipeline {
  agent any
  stages {
    stage('Preparação') {
      steps {
        // Limpa arquivos não rastreados e diretórios não rastreados pelo Git.
        sh "git clean -fdx"
      }
    }
    stage('Build') {
      steps {
        sh "docker build . -t plataforma:last"
      }
    }
    stage('Run') {
      steps {
        // Executa a imagem Docker construída no estágio anterior mapeando a porta 4000 do container para a porta 4000 do host.
        sh "docker run -d --name app_plataforma -p 4000:8081 plataforma:last"
      }
    }
  }
  triggers {
    // Configura o Jenkins para pesquisar mudanças no repositório de controle de versão conforme necessário.
    // Ajuste esta configuração conforme a frequência que deseja que os builds sejam disparados automaticamente.
    pollSCM('H * * * *')
  }  
}
