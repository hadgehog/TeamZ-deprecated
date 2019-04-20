docker stop apparatus-webapp
docker rm apparatus-webapp
docker run -p 80:80 -d --name apparatus-webapp apparatus/webapp:latest