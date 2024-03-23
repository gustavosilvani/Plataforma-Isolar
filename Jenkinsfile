pipeline {
  agent any
  stages {
    stage('Build') {
      post {
        success {
          echo 'Now Archiving...'
          archiveArtifacts 'target/*.war'
        }

      }
      steps {
        sh "docker build . -t dockerjenkinshelloworld:${env.BUILD_ID}"
      }
    }

  }
  triggers {
    pollSCM('* * * * *')
  }
}
