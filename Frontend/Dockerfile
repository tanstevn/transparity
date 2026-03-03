FROM node:20-alpine AS build
WORKDIR /app

COPY package.json .
COPY package-lock.json .

RUN npm ci --prefer-online

COPY . .

RUN npm run build

FROM node:20-alpine AS final
WORKDIR /app

ENV NODE_ENV=production
ENV PORT=8433
EXPOSE 8433

COPY --from=build /app/package.json .
COPY --from=build /app/package-lock.json .
COPY --from=build /app/node_modules ./node_modules
COPY --from=build /app/.next ./.next
COPY --from=build /app/public ./public

RUN addgroup -g 10000 -S transparity_client_group \
   && adduser -S transparity_client_user -u 10000 -G transparity_client_group

USER transparity_client_user

CMD ["npm", "run", "start"]
