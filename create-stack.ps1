#aws cloudformation create-stack --stack-name test --template-body file://book-chest.stack.json --capabilities CAPABILITY_IAM

aws cloudformation deploy --stack-name test --template-file ./book-chest.stack.json --capabilities CAPABILITY_IAM
