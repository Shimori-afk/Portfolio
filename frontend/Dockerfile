FROM node:20-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY . .
RUN npm run build -- --configuration production

FROM nginx:alpine AS runtime
COPY --from=build /app/dist/portfolio-frontend/browser /usr/share/nginx/html
EXPOSE 80
