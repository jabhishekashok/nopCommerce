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
        stage('Example') {
            steps {
                echo 'Hello World'
            }
        }
    }
}