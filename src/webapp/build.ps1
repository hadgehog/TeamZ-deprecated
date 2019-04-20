rm -r ./dist
npm run build
docker build -t apparatus/webapp:latest .