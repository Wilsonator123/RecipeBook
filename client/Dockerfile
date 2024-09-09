# Use Node.js LTS version as base image
FROM node:latest

WORKDIR /app/client
# Copy package.json and package-lock.json files
COPY package*.json ./

# Install dependencies
RUN npm install -f

# Copy the rest of the application
COPY . .

EXPOSE 3000:3000

# Start the application
CMD npm run dev