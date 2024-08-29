# Step 1: Build the Angular application
FROM node:18-alpine as build
WORKDIR /app/src

# Copy the package.json and package-lock.json files
COPY IdeoPrivateRoom.AngularApp/ideo-private-room/package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application code
COPY IdeoPrivateRoom.AngularApp/ideo-private-room ./

# Build the Angular application (non-SSR)
RUN npm run build

# Step 2: Serve the built Angular application using NGINX
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Copy the built Angular application from the build stage
COPY --from=build /app/src/dist/ideo-private-room .

# Copy the custom Nginx configuration
COPY IdeoPrivateRoom.AngularApp/ideo-private-room/nginx.conf /etc/nginx/nginx.conf

# Expose port 80 for the NGINX server
EXPOSE 80

# Start NGINX server
CMD ["nginx", "-g", "daemon off;"]
