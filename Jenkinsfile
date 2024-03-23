pipeline {
  agent any
  stages {
    stage('Preparação') {
      steps {
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
          sh "docker run -d --name app_plataforma -p 4000:8080 -p 4001:8081 plataforma:last"
      }
    }
  }
  triggers {
    
    pollSCM('H * * * *')
  }  
}
