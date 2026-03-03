FROM node:22-alpine AS build
WORKDIR /app

COPY package.json .
COPY package-lock.json .
RUN npm ci

COPY . .
RUN npm run build

FROM node:22-alpine AS final
WORKDIR /app

ENV NODE_ENV=production
ENV PORT=8433
EXPOSE 8433

COPY --from=build /app/.next/standalone .
COPY --from=build /app/.next/static ./.next/static
COPY --from=build /app/public ./public

RUN addgroup -g 10000 -S transparity_client_group \
   && adduser -S transparity_client_user -u 10000 -G transparity_client_group

USER transparity_client_user

CMD ["node", "server.js"]
