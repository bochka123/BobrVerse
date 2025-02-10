FROM node:20-alpine3.20 AS build
WORKDIR /usr/src/app
COPY package*.json ./
RUN npm ci
COPY ./ ./
RUN npm run build

FROM nginx:stable-alpine AS production
COPY --from=build /usr/src/app/nginx.conf /etc/nginx/conf.d
COPY --from=build /usr/src/app/dist /usr/share/nginx/html
EXPOSE 80 443
ENTRYPOINT ["nginx", "-g", "daemon off;"] 