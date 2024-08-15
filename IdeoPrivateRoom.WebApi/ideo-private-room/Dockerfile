# Angular setup
FROM node:18-alpine as build
WORKDIR /app/src
COPY IdeoPrivateRoom.WebApi/ideo-private-room/package*.json ./
RUN npm install
COPY IdeoPrivateRoom.WebApi/ideo-private-room ./
RUN npm run build

FROM node:18-alpine
WORKDIR /usr/app
COPY --from=build /app/src/dist/ideo-private-room ./
CMD node server/server.mjs
EXPOSE 4000
