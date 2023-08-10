pipeline {
    agent { 
        label 'DotnetNode1' 
    }
    options {
        // Timeout counter starts AFTER agent is allocated
        timeout(time: 1, unit: 'SECONDS')
    }
    triggers { 
        pollSCM('* * * * *') 
    }
    stages {
        stage('VCS') {
            steps {
                git url: 'https://github.com/jabhishekashok/nopCommerce.git'
                    branch: 'develop'
            }
        }
        stage('Build'){
            steps{
                sh 'ls -al'
                //sh 'docker image build -t theabhij/nopimg:2.0'
            }

        }
        
    }

}