FROM node:16 AS builder

# Create app directory
WORKDIR /app

# Install app dependencies
COPY package*.json .
COPY ./ /app/

RUN npm install
RUN npm run build
EXPOSE 80
ENTRYPOINT ["npm", "start"]