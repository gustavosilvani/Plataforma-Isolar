pipeline {
    agent any
    triggers {
        pollSCM('* * * * *')
    }
    stages{
        stage('Build'){
            steps {
                sh "mvn clean package"
                sh "docker build . -t dockerjenkinshelloworld:${env.BUILD_ID}"
            }
            post {
                success {
                    echo "Now Archiving..."
                    archiveArtifacts artifacts: "target/*.war"
                }
            }
        }
    }
}