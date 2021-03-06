
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
EXPOSE 5000

WORKDIR /app

COPY ./*.csproj ./
WORKDIR /app/
RUN dotnet restore 

COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./ 
ENTRYPOINT [ "dotnet", "API.dll" ]
